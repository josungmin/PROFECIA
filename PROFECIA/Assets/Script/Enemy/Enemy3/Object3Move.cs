using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object3Move : MonoBehaviour
{

    private PlayerStat targetAttribute;
    public EnemyStat myAttribute;
    private SpriteRenderer spriteRenderer;

    public int enemyID;

    //public float currentHP;
    [SerializeField] public int previousHP;
    public bool facingRight = true;

    public GameObject Target;
    [SerializeField] public int tmp;
    [SerializeField] public bool find;
    [SerializeField] public bool hit;
    [SerializeField] public bool hitRange;
    [SerializeField] public bool direction;
    [SerializeField] public bool nuck;
    [SerializeField] public int AttackCount;

    //public BoxCollider2D AttackCol;

    public float Speed;
    [SerializeField] private Animator animator;
    //[SerializeField] private PlayerMovement playerMovement;
    //[SerializeField] private EnemyHit enemyHit;
    private Rigidbody2D myRigidbody;

    AudioSource audioSource;
    AudioSource aud;
    public AudioClip attack; // 기본
    public AudioClip damage; // 피격


    public enum State
    {
        IDLE,
        MOVE,
        ATTACK,
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
                    if (direction == true) //왼쪽
                    {
                        AttackR();
                        //animator.SetBool("isAttack", true);
                    }
                    else if (direction == false)//오른쪽
                    {
                        Attack();
                        //animator.SetBool("isAttackL", true);
                    }
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
        find = false;
        hit = false;
        hitRange = false;
        nuck = false;
        AttackCount = 0;
        SetState(State.IDLE);
    }

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetAttribute = Target.GetComponent<PlayerStat>();
        myAttribute = gameObject.GetComponent<EnemyStat>();
        myRigidbody = GetComponent<Rigidbody2D>();

        myAttribute.currentHP = myAttribute.myStat.enemyList[enemyID - 1].maxHP;
        tmp = myAttribute.currentHP;
        previousHP = myAttribute.currentHP;

        audioSource = GetComponent<AudioSource>();
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (find == true)
        {
            if (myAttribute.currentHP == 0 || myAttribute.currentHP < 0)
            {
                SetState(State.DEAD);
            }

            float key = Input.GetAxis("Horizontal");

            Vector3 dir = Target.transform.position - transform.position;
            dir.Normalize();

            float distance = Vector3.Distance(Target.transform.position, transform.position);

            //플레이어에게 접근
            if (distance > targetAttribute.basicStat.width + myAttribute.width)
            {
                if (dir.x > 0 && !facingRight)
                {
                    direction = true;
                    facingRight = !facingRight;
                    spriteRenderer.flipX = false;
                }
                else if (dir.x < 0 && facingRight)
                {
                    direction = false;
                    facingRight = !facingRight;
                    spriteRenderer.flipX = true;
                }

                if (currentState_ == State.ATTACK)
                {
                    Speed = 0.0f;
                    animator.SetFloat("Speed", Speed);
                }
                else
                {
                    SetState(State.MOVE);
                    Speed = myAttribute.myStat.enemyList[enemyID - 1].movementSpeed;
                    animator.SetFloat("Speed", Speed);
                    animator.SetBool("isWalk", true);
                    transform.position += (dir * myAttribute.myStat.enemyList[enemyID - 1].movementSpeed * Time.deltaTime);
                }

                //SetState(State.MOVE);
            }

            //플레이어에게 근접 시 공격 시전
            else if (distance <= targetAttribute.basicStat.width + myAttribute.width)
            {
                hitRange = true;
                animator.SetBool("isWalk", false);

                SetState(State.ATTACK);

                StartCoroutine(AttackDelay());

                /*
                if (direction == true) //왼쪽
                {
                    animator.SetBool("isAttack", true);
                    SetState(State.ATTACK);
                }
                else if (direction == false)//오른쪽
                {
                    animator.SetBool("isAttackL", true);
                    SetState(State.ATTACK);
                }
                */
            }

            if (myAttribute.currentHP < previousHP)
            {
                if (myAttribute.currentHP >= 1)
                {
                    knockBack();
                }
                else if (myAttribute.currentHP <= 0)
                {
                    SetState(State.DEAD);
                }
            }

            /*
            if (myAttribute.currentHP < previousHP)
            {
                
                //SetState(State.DAMAGED);
            }
            */

        }
    }

    void Speed_zero()
    {
        Speed = 0.0f;
        animator.SetFloat("Speed", Speed);
    }

    void AttackR()
    {
        //animator.SetBool("isWalk", false);
        animator.SetBool("isAttackR", false);
        hitRange = false;
    }

    void Attack()
    {
        //animator.SetBool("isWalk", false);
        animator.SetBool("isAttack", false);
        hitRange = false;
    }

    void Damaged()
    {
        Debug.Log("Damaged");
        //animator.SetBool("isDamaged", true);
        previousHP = myAttribute.currentHP;
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
        targetAttribute.currentExp += myAttribute.myStat.enemyList[enemyID - 1].droppedEXP;
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

    IEnumerator AttackDelay()
    {
        if(AttackCount >= 2)
        {
            animator.SetBool("isAttackR", false);
            animator.SetBool("isAttack", false);
            animator.SetBool("isWalk", false);
            animator.SetBool("isIdle", true);

            yield return new WaitForSeconds(3.0f);
            AttackCount = 0;
            animator.SetBool("isIdle", false);
            animator.SetBool("isWalk", true);
        }
        yield return new WaitForSeconds(1.0f);

        if (direction == true) //왼쪽
        {
            animator.SetBool("isAttackR", true);
            if (!aud.isPlaying)
                PlayerSound(attack);
            //Attack();
            SetState(State.ATTACK);
            AttackCount += 1;
        }
        else if (direction == false)//오른쪽
        {
            animator.SetBool("isAttack", true);
            if (!aud.isPlaying)
                PlayerSound(attack);
            //AttackL();
            SetState(State.ATTACK);
            AttackCount += 1;
        }

        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (myAttribute.currentHP >= 1)
        {
            if (col.tag == "Effect")
                return;
            else if (col.tag == "Weapon" && myAttribute.currentHP < previousHP)
            {
                Debug.Log("KnockBack");
                knockBack();
            }
        }
        else if (myAttribute.currentHP <= 0)
            SetState(State.DEAD);
    }

    // 효과음 재생 메소드    // 기본 : 1회재생(반복X)
    void PlayerSound(AudioClip clip)
    {
        aud.PlayOneShot(clip);
    }
}