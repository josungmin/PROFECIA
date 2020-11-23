using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D myRigidbody;

    private PlayerStat targetAttribute;
    [SerializeField] private door doorCheck;
    public GameObject Target;

    public int HP;
    public int CurrentHP;
    public int previousHP;
    public int AttackPower;
    public int Skill_AttackPower1;
    public int Skill_AttackPower2;
    public int Skill_AttackPower3;
    public float MoveSpeed;
    public float Speed;
    public float width;

    public bool hitRange;
    public bool AttackPater1;
    public bool AttackPater2;
    public bool AttackPater3;

    public int AttackCount;
    public int AttackPater;
    public bool facingRight = true;
    public bool find;

    AudioSource audioSource;
    AudioSource aud;
    public AudioClip attack; // 기본
    public AudioClip attack1; //화염방사
    public AudioClip attack2; //점프
    public AudioClip attack3; //차징
    public AudioClip damage; // 피격

    public enum State
    {
        IDLE,
        MOVE,
        ATTACK,
        ATTACK1,
        ATTACK2,
        ATTACK3,
        DAMAGED,
        DEAD,
    };
    private State currentState_ = State.IDLE;

    public void SetState(State newState)
    {
        //공격중 다시 공격 못하도록
        if (newState == currentState_)
            return;

        currentState_ = newState;

        switch (newState)
        {
            case State.MOVE:
                break;
            case State.ATTACK:
                {
                    Attack();
                }
                break;
            case State.ATTACK1:
                {
                    Attack1();
                }
                break;
            case State.ATTACK2:
                {
                    Attack2();
                }
                break;
            case State.ATTACK3:
                {
                    Attack3();
                }
                break;
            case State.IDLE:
                {
                }
                break;
            case State.DAMAGED:
                {
                    Damaged();
                }
                break;
            case State.DEAD:
                {
                    Dead();
                }
                break;
        }
    }

    void Awake()
    {
        SetState(State.IDLE);
        hitRange = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        myRigidbody = GetComponent<Rigidbody2D>();
        targetAttribute = Target.GetComponent<PlayerStat>();

        find = false;

        AttackPater = 0;
        AttackCount = 0;
        CurrentHP = HP;
        previousHP = CurrentHP;

        audioSource = GetComponent<AudioSource>();
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (find == true)
        {
            if (CurrentHP == 0 || CurrentHP < 0)
            {
                SetState(State.DEAD);
            }

            float key = Input.GetAxis("Horizontal");

            Vector3 dir = Target.transform.position - transform.position;
            dir.Normalize();

            float distance = Vector3.Distance(Target.transform.position, transform.position);

            //플레이어에게 접근
            if (distance > targetAttribute.basicStat.width + width)
            {
                if (dir.x > 0 && !facingRight)
                {
                    facingRight = !facingRight;
                    spriteRenderer.flipX = true;
                }
                else if (dir.x < 0 && facingRight)
                {
                    facingRight = !facingRight;
                    spriteRenderer.flipX = false;
                }

                if (currentState_ == State.ATTACK)
                {
                    Speed = 0.0f;
                    animator.SetFloat("Speed", Speed);
                }
                else
                {
                    SetState(State.MOVE);
                    Speed = MoveSpeed;
                    animator.SetFloat("Speed", Speed);
                    animator.SetBool("isWalk", true);
                    transform.position += (dir * MoveSpeed * Time.deltaTime);
                }

                //SetState(State.MOVE);
            }

            //플레이어에게 근접 시 공격 시전
            else if (distance <= targetAttribute.basicStat.width + width)
            {
                hitRange = true;
                animator.SetBool("isWalk", false);

                SetState(State.ATTACK);

                StartCoroutine(AttackDelay());
            }

            if (CurrentHP < previousHP)
            {
                if (CurrentHP >= 1)
                {
                    //knockBack();
                }
                else if (CurrentHP <= 0)
                {
                    SetState(State.DEAD);
                }
            }
        }
    }

    void Speed_zero()
    {
        Speed = 0.0f;
        animator.SetFloat("Speed", Speed);
    }

    void Attack()//기본공격
    {
        animator.SetBool("isBasicAttack", false);
        hitRange = false;
    }

    void Attack1()
    {
        animator.SetBool("isAttack1", false);
        hitRange = false;
        //AttackCount = 0;
        AttackPater = 0;
    }

    void Attack2()
    {
        animator.SetBool("isAttack2", false);
        hitRange = false;
        //AttackCount = 0;
        AttackPater = 0;
    }

    void Attack3()
    {
        animator.SetBool("isAttack3", false);
        hitRange = false;
        //AttackCount = 0;
        AttackPater = 0;
    }

    void Damaged()
    {
        Debug.Log("Damaged");
        //animator.SetBool("isDamaged", true);
        previousHP = CurrentHP;
        if (!aud.isPlaying)
            PlayerSound(damage);
    }

    void Idle()
    {
        if (find == true)
        {
            SetState(State.MOVE);
        }
    }

    void Dead()
    {
        doorCheck.doorOpen = true;
        Destroy(GameObject.Find("BossRoom"));
        Destroy(this.gameObject);
    }

    void knockBack()
    {
        SetState(State.DAMAGED);
        var enem = GameObject.FindGameObjectWithTag("Player");
        // 넉백 정도 (Vector3(x,y,z)값으로 조정)
        Vector3 attackedVelocity = Vector3.zero;

        if (this.transform.position.x < enem.transform.position.x)
            attackedVelocity = new Vector3(-30.0f, 20.0f);
        else
            attackedVelocity = new Vector3(30.0f, 20.0f);

        myRigidbody.AddForce(attackedVelocity, ForceMode2D.Impulse);

        //myAttribute.currentHP -= targetAttribute.atk;

        previousHP -= targetAttribute.currentPower;

        animator.SetBool("isDamaged", false);
    }

    void AttackCountUp()
    {
        AttackCount++;
    }

    void AttackCountInitialization()
    {
        AttackCount = 0;
        AttackPater = 0;
;    }

    IEnumerator AttackDelay()
    {
        if(AttackCount < 3)
        {
            animator.SetBool("isBasicAttack", true);
            if (!aud.isPlaying)
                PlayerSound(attack);
            SetState(State.ATTACK);
            Debug.Log(AttackCount);
        }
        else
        {
            if(AttackPater == 0)
            {
                AttackPater = Random.Range(1, 4);
            }
            else
            {

            }
            
            Debug.Log("AttackPater");

            if (AttackPater == 1)
            {
                animator.SetBool("isAttack1", true);
                if (!aud.isPlaying)
                    PlayerSound(attack1);
                yield return new WaitForSeconds(1.3f);
                SetState(State.ATTACK1);
                Debug.Log("isAttack1");
            }

            if (AttackPater == 2)
            {
                animator.SetBool("isAttack2", true);
                if (!aud.isPlaying)
                {
                    audioSource.volume = 0.7f; //0.0f ~ 1.0f사이의 숫자로 볼륨을 조절
                    PlayerSound(attack2);
                }
                yield return new WaitForSeconds(0.4f);
                SetState(State.ATTACK2); 
                Debug.Log("isAttack2");
            }

            if (AttackPater == 3)
            {
                animator.SetBool("isAttack3", true);
                if (!aud.isPlaying)
                {
                    PlayerSound(attack3);
                }
                yield return new WaitForSeconds(1.1f);
                SetState(State.ATTACK3);
                Debug.Log("isAttack3");
            }
        }

        //yield return null;
    }

    // 효과음 재생 메소드    // 기본 : 1회재생(반복X)
    void PlayerSound(AudioClip clip)
    {
        aud.PlayOneShot(clip);
    }
}