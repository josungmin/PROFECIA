using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS2Move : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D myRigidbody;

    private PlayerStat targetAttribute;
    public GameObject Target;
    [SerializeField] private door doorCheck;

    public int HP;
    public int CurrentHP;
    public int previousHP;
    public int AttackPower;
    public int Skill_AttackPower1;
    public int Skill_AttackPower2;
    public float MoveSpeed;
    public float Speed;
    public float width;

    public bool hitRange;
    public bool AttackPater1;
    public bool AttackPater2;

    public int AttackCount;
    public int AttackPater;
    public bool facingRight = true;
    public bool find;

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
                    spriteRenderer.flipX = false;
                }
                else if (dir.x < 0 && facingRight)
                {
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

    void Damaged()
    {
        Debug.Log("Damaged");
        //animator.SetBool("isDamaged", true);
        previousHP = CurrentHP;
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
    }

    IEnumerator AttackDelay()
    {
        if (AttackCount < 3)
        {
            animator.SetBool("isBasicAttack", true);
            SetState(State.ATTACK);
            Debug.Log(AttackCount);
        }
        else
        {
            if (AttackPater == 0)
            {
                AttackPater = Random.Range(1, 3);
            }
            else
            {

            }

            Debug.Log("AttackPater");

            if (AttackPater == 1)
            {
                animator.SetBool("isAttack1", true);
                yield return new WaitForSeconds(0.5f);
                SetState(State.ATTACK1);
                Debug.Log("isAttack1");
            }

            if (AttackPater == 2)
            {
                animator.SetBool("isAttack2", true);
                yield return new WaitForSeconds(0.7f);
                SetState(State.ATTACK2);
                Debug.Log("isAttack2");
            }
        }
    }
}
