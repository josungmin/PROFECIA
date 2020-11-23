/*
 *      Script - PlayerController.cs 를 포함한 새로운 플레이어 이동 스크립트
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    #region PUBLIC_VAR
    public bool facingRight = true; //Depends on if your animation is by default facing right or left
    public float jumpForce = 15;
    public bool isGrounded;
    public bool isJumping = false;
    public int jumpCountValue = 1;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    bool isDie = false;
    bool isCoolDown = false;
    bool isCoolDownG = false;
    public int previousHP;
    public bool damaged;
    public Transform player;
    #endregion

    #region PRIVATE_VAR
    [SerializeField] private Transform attackPoint;
    Animator anim;
    SpriteRenderer spriteRenderer;
    bool damageMade;
    private Rigidbody2D myRigidbody;
    private bool isUnBeatTime;
    private int attackedDamage;
    [SerializeField] private PlayerStat playerStat;
    [SerializeField] private SwitchingWeapons weapons;
    private int jumpCount;
    public GameObject gauntletEffect;
    [SerializeField] Transform spawnPoint;
    #endregion

    //         사운드 관련          //
    AudioSource aud;
    public AudioSource audioSource; //출력 관련 
    public AudioClip rollingSound;  //구르기
    public AudioClip walkingSound;  //걷기
    // public AudioClip S_attackSound;  //소드 기본
    public AudioClip G_attackSound; // 펄스건 기본
    // 스킬관련 사운드는 스킬 스크립트에 있음
    bool isWalking;
    public AudioClip healSound;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerStat = GetComponent<PlayerStat>();
        weapons = GetComponent<SwitchingWeapons>();
        player = transform.Find("TirggerCheckP");

        isUnBeatTime = false;
        damaged = false;
        //기본 최대 체력 + HP에 투자한 스텟 * 10
        playerStat.currentHP = PlayerPrefs.GetInt("CurrentHP", 0); //playerStat.basicStat.maxHP + (playerStat.statPoint.HP * 10); 
        previousHP = playerStat.currentHP;

        aud = GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        // Check for axis horizontal
        float movement = Input.GetAxis("Horizontal") * Time.deltaTime * playerStat.basicStat.movementSpeed;

        // 체력
        if (playerStat.currentHP == 0)
            return;


        // if movement is not equal to 0, means player pressed a or either d, so stop idling, else stop running
        if (anim.GetBool("isJumping") || movement != 0 && anim.GetCurrentAnimatorStateInfo(0).IsName("Attacking") == false)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isIdle", false);
            // if movement float is more than 0 means that it moves to right, so turn player to right and move it
            if (movement > 0 && !facingRight) //flip 제어문
            {
                Flip();
            }
            else if (movement < 0 && facingRight)
            {
                Flip();
            }

            if (Input.GetKey(KeyCode.RightArrow))   //이동 제어문
            {
                transform.Translate(transform.right * movement);
                isWalking = true;
                if (!aud.isPlaying)
                    aud.Play();
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(transform.right * movement);
                isWalking = true;
                if (!aud.isPlaying)
                    aud.Play();
            }
        }
        else if (movement != 0)
        {
            if (isJumping == true)
            {
                anim.SetBool("isJumping", true);
            }
        }
        else if (movement == 0 && anim.GetCurrentAnimatorStateInfo(0).IsName("Attacking") == false)
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isIdle", true);
        }

        // 구르기
        if (Input.GetKeyDown(KeyCode.S))    //S키를 눌렀을때
        {
            //Attacking 애니메이션이 플레이되지 않으며
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attacking") == false && !isJumping && !isCoolDown) // !anim.GetCurrentAnimatorStateInfo(0).IsName("Attacking")
            {
                Vector3 attackedVelocity = Vector3.zero;

                if (!facingRight)
                    attackedVelocity = new Vector3(-50.0f, -10f);
                else
                    attackedVelocity = new Vector3(50.0f, -10f);

                myRigidbody.AddForce(attackedVelocity, ForceMode2D.Impulse);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Timer();    //플레이 타임 카운트 시작
        // 체력 0이면 사망
        if (playerStat.currentHP == 0)
        {
            if (!isDie)
                Die();

            return;
        }

        // 플레이어 피격 시 넉백
        if (playerStat.currentHP < previousHP)
        {
            if (playerStat.currentHP >= 1)
            {
                if (isUnBeatTime == false)
                    previousHP = playerStat.currentHP - attackedDamage;
                knockBack();
            }
            /*
            else
            {
                Die();
            }*/
        }
        // 플레이어 회복 시 직전체력 업데이트
        else
        {
            previousHP = playerStat.currentHP;
        }
        
        // 땅에 있으면 점프가능횟수 초기화
        if (isGrounded == true)
        {
            anim.SetBool("isJumping", false);
            jumpCount = jumpCountValue;
            isJumping = false;
        }
        else
        {
            isJumping = true;
            anim.SetBool("isJumping", true);
        }

        //점프 제어
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            isJumping = true;
            myRigidbody.velocity = Vector2.up * jumpForce;
            anim.SetBool("isJumping", true);
            jumpCount--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && jumpCount == 0 && isGrounded == true)
        {
            isJumping = true;
            myRigidbody.velocity = Vector2.up * jumpForce;
            anim.SetBool("isJumping", true);
        }


        // 기본 공격 시 애니메이션 설정
        if (Input.GetKey(KeyCode.A) && weapons.selectedWeapon == 0)
        {
            anim.SetBool("isAttacking", true);
            if (Input.GetKeyUp(KeyCode.W) && weapons.selectedWeapon == 1)
            {
                anim.SetBool("isAttacking", true);
                anim.SetInteger("SelectedWeaponNum", 1);
                anim.SetBool("isAttackingG", true);
            }
        }
        else if (Input.GetKeyUp(KeyCode.A) && weapons.selectedWeapon == 0)
        {
            anim.SetBool("isAttacking", false);
            anim.SetBool("isAttackingG", false);
            anim.SetBool("isIdle", true);
        }

        if (Input.GetKey(KeyCode.D) && playerStat.gauntletEnergy >= 2 || Input.GetKey(KeyCode.A) && weapons.selectedWeapon == 1)
        {
            anim.SetBool("isAttackingG", true);
            if (Input.GetKeyUp(KeyCode.W) && weapons.selectedWeapon == 0)
            {
                anim.SetBool("isAttackingG", true);
                anim.SetInteger("SelectedWeaponNum", 0);
                anim.SetBool("isAttacking", true);
                return;
            }
        }
        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A) && weapons.selectedWeapon == 1)
        {
            anim.SetBool("isAttackingG", false);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isIdle", true);
        }

        if (Input.GetKeyDown(KeyCode.A) && weapons.selectedWeapon == 1)
        {
            StartCoroutine(IEGauntletSkillState());
        }
        
        // 구르기 애니메이션 설정
        if (Input.GetKeyDown(KeyCode.S) && !isCoolDown)
        {
            if (!(anim.GetCurrentAnimatorStateInfo(0).IsName("Attacking") || isJumping))
            {
                anim.SetBool("isRolling", true);
                StartCoroutine(IERollingCoolTime());
                PlayerSound(rollingSound);  // [사운드] 구르기
            }
        }
        else
        {
            anim.SetBool("isRolling", false);
        }

        // check if the attackPoint/damagemaker become active
        if (attackPoint.gameObject.activeSelf == true && damageMade == false)
        {
            damageMade = true;
            damaged = true;

            //hittedObjects에 공격받은 적들이 저장됨
            Collider2D[] hittedObjects = Physics2D.OverlapCircleAll(attackPoint.position, playerStat.basicStat.attackRange);
            if (hittedObjects.Length > 0)
            {
                for (int i = 0; i < hittedObjects.Length; i++)
                {
                    if (hittedObjects[i].gameObject != gameObject)
                    {
                        ObjectMove enemy = hittedObjects[i].gameObject.GetComponent<ObjectMove>();
                        Object3Move enemy3 = hittedObjects[i].gameObject.GetComponent<Object3Move>();
                        Object4Move enemy4 = hittedObjects[i].gameObject.GetComponent<Object4Move>();
                        Object5Move enemy5 = hittedObjects[i].gameObject.GetComponent<Object5Move>();
                        BossMove boss = hittedObjects[i].gameObject.GetComponent<BossMove>();
                        BOSS2Move boss2 = hittedObjects[i].gameObject.GetComponent<BOSS2Move>();
                        if (enemy != null && damaged == true)
                        {
                            enemy.myAttribute.currentHP -= playerStat.currentPower;
                        }
                        if (enemy3 != null && damaged == true)
                        {
                            enemy3.myAttribute.currentHP -= playerStat.currentPower;
                        }
                        if (enemy4 != null && damaged == true)
                        {
                            enemy4.myAttribute.currentHP -= playerStat.currentPower;
                        }
                        if (enemy5 != null && damaged == true)
                        {
                            enemy5.myAttribute.currentHP -= playerStat.currentPower;
                        }
                        if (boss != null && damaged == true)
                        {
                            boss.CurrentHP -= playerStat.currentPower;
                        }
                        if (boss2 != null && damaged == true)
                        {
                            boss2.CurrentHP -= playerStat.currentPower;
                        }
                    }
                }
                damaged = false; //단일 공격의 원인
            }
        }
        else if (attackPoint.gameObject.activeSelf == false && damageMade == true)
        {
            damageMade = false;
        }
    }

    // ▼▼▼▼▼ 기능 함수들 ▼▼▼▼▼

    // 상태함수 (죽음)
    void Die()
    {
        isDie = true;

        myRigidbody.velocity = Vector2.zero;

        Debug.Log("GameOver");

        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);

        //animator.Play("Die");
    }

    // 스프라이트 플립함수
    public void Flip()
    {
        facingRight = !facingRight;
        /*Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;*/
        transform.Rotate(0f, 180f, 0f);
    }

    // 어택포인트 기즈모함수
    void OnDrawGizmos()
    {
        // 소드
        if (weapons.selectedWeapon == 0)
        {
            if (attackPoint.gameObject.activeSelf == false)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(attackPoint.position, playerStat.basicStat.attackRange);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(attackPoint.position, playerStat.basicStat.attackRange);
            }
        }
        // 건틀릿
        else if (weapons.selectedWeapon == 1)
        {
            if (attackPoint.gameObject.activeSelf == false)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(attackPoint.position, playerStat.basicStat.attackRange);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(attackPoint.position, playerStat.basicStat.attackRange);
            }
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        }
    }

    // 충돌 감지함수
    void OnCollisionEnter2D(Collision2D col)
    {
        // 몬스터 감지함수
        if (col.gameObject.tag == "Trigger")
        {
            if (playerStat.currentHP >= 1)
            {
                isUnBeatTime = true;
            }
            else
                Die();
        }
    }

    void OnTriggerEnter2D(Collider2D heal)
    {
        // HP회복
        if (heal.gameObject.tag == "Heal")// 힐 태그 붙은 오브젝트와 충돌
        {
            if (playerStat.currentHP < playerStat.basicStat.maxHP + (playerStat.statPoint.HP * 10))
            {
                if (!aud.isPlaying)
                    PlayerSound(healSound);
            }
        }
    }

    // 넉백함수
    void knockBack()
    {
        var enem = GameObject.FindGameObjectWithTag("Trigger");
        anim.SetBool("isDamaged", true);
        // 넉백 정도 (Vector3(x,y,z)값으로 조정)
        if (isUnBeatTime == false)
        {
            Vector3 attackedVelocity = Vector3.zero;

            if (this.transform.position.x < enem.transform.position.x)
                attackedVelocity = new Vector3(-30.0f, 20.0f);
            else
                attackedVelocity = new Vector3(30.0f, 20.0f);

            myRigidbody.AddForce(attackedVelocity, ForceMode2D.Impulse);

            // 무적타임
            if (playerStat.currentHP > 1)
            {
                isUnBeatTime = true;
                StartCoroutine("UnBeatTime");
            }
            previousHP = previousHP - attackedDamage;
        }
        else
        {
            return;
        }
    }

    void Timer()
    {
        playerStat.basicStat.playTime += Time.deltaTime;
    }

    // 피격 시 무적
    IEnumerator UnBeatTime()
    {
        isUnBeatTime = true;
        int countTime = 0;

        if (true)
        {
            while (countTime < 20)
            {
                if (countTime % 2 == 0)
                    spriteRenderer.color = new Color32(255, 255, 255, 180);
                else
                    spriteRenderer.color = new Color32(255, 255, 255, 255);

                playerStat.currentHP = previousHP;
                yield return new WaitForSeconds(0.05f);

                anim.SetBool("isDamaged", false);

                countTime++;
            }
        }
        spriteRenderer.color = new Color32(255, 255, 255, 255);

        //playerStat.currentHP = previousHP;
        isUnBeatTime = false;

        yield return false;
    }

    // 공격 애니매이션 중 공격 영역 활성화
    private IEnumerator WeaponCollisionActive()
    {
        if (weapons.selectedWeapon == 0)
        {
            //Debug.Log("S Attack");
            attackPoint.gameObject.SetActive(true);

            yield return new WaitForSeconds(0.6f);  // 활성 시간

            attackPoint.gameObject.SetActive(false);
        }
        if (weapons.selectedWeapon == 1)
        {
            attackPoint.gameObject.SetActive(true);
            //Debug.Log("G Attack");

            yield return new WaitForSeconds(0.5f);  // 활성 시간

            attackPoint.gameObject.SetActive(false);
        }

    }

    //구르기 쿨타임
    private IEnumerator IERollingCoolTime()
    {
        StartCoroutine("RollingUnBeatTimeActive");
        yield return new WaitForSeconds(0.6f); //Rolling 애니메이션 플레이 타임
        isCoolDown = true;
        yield return new WaitForSeconds(7f); // 쿨타임
        Debug.Log("Roll CoolDown Complete");
        isCoolDown = false;
    }

    // 구르기 애니메이션 중 무적 활성화
    private IEnumerator RollingUnBeatTimeActive()
    {
        player.gameObject.SetActive(false);
        playerStat.currentHP = previousHP;

        yield return new WaitForSeconds(1.5f);  // 비활성 시간

        player.gameObject.SetActive(true);
    }

    // 기본 펄스건 이펙트
    private IEnumerator IEGauntletSkillState()
    {
        isCoolDownG = true;
        yield return new WaitForSeconds(0.2f); // 애니메이션 후 이펙트 날라감

        Instantiate(gauntletEffect, spawnPoint.position, spawnPoint.rotation);
        audioSource.volume = 1f; //0.0f ~ 1.0f사이의 숫자로 볼륨을 조절
        PlayerSound(G_attackSound);

        yield return new WaitForSeconds(0.3f);
        isCoolDownG = false;
    }

    // 효과음 재생 메소드    // 기본 : 1회재생(반복X)
    void PlayerSound(AudioClip clip)
    {
        aud.PlayOneShot(clip);
    }
}