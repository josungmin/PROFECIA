using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce = 10f;
    public int maxHealth;

    private Rigidbody2D myRigidbody;

    SpriteRenderer spriteRenderer;

    //Animator animator;

    private bool facingRight = true; // 오른쪽 보고있음

    // 상태감지(점프)
    public bool isGrounded = false; // 바닥에 있는지 없는지
    public bool isJumping = false; // 점프중인지 아닌지

    public int jumpCount = 2; // 점프횟수 2를 3으로 바꾸면 3단 점프
    public Transform groundCheck; // 바닥감지
    public float checkRadius; // 바닥감지 반경
    public LayerMask whatIsGround; //바닥에 레이어 Ground 씌우기

    // 상태감지(죽음)
    public bool isDie = false;

    // 체력    
    public int health = 50; // 현재체력
    public int maxHP = 100; // 최대체력 스탯을 올리면 maxHP가 올라감
    public int healHP = 10; // 힐팩 먹으면 회복하는 체력의 양

    private bool isUnBeatTime = false;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        jumpCount = 0;

        health = maxHP;
    }

    void FixedUpdate()
    {
        // 체력
        if (health == 0)
            return;
        // 이동
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            //animator.SetInteger("Direction", -1);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            //animator.SetInteger("Direction", 1);
        }

        //점프
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpCount > 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpForce);
                isJumping = true;
            }
        }
    }

    // 충돌트리거 감지 
    void OnTriggerEnter2D(Collider2D other)
    {
        // HP회복
        if (other.gameObject.tag == "Heal")// 힐 태그 붙은 오브젝트와 충돌
        {
            // 최대HP보다 현재HP가 낮으면 힐
            if (health < maxHP)
            {
                health += healHP;
                Debug.Log("regain HP"); // 체력회복 확인
                Destroy(other.gameObject); // 먹은 힐팩 삭제
            }
            // 풀피면 회복 안함 = 힐팩 안먹음
            else if (health >= maxHP)
            {
                health = maxHP;
            }
        }
        else if (other.gameObject.tag == "Enemy" && !other.isTrigger && !isUnBeatTime) // eneny 태그 붙은 오브젝트와 충돌
        {
            // 피격시 밀려남
            Vector2 attackedVelocity = Vector2.zero;
            if (other.gameObject.transform.position.x > transform.position.x)
                attackedVelocity = new Vector2(-3f, 2f);
            else
                attackedVelocity = new Vector2(3f, 2f);

            myRigidbody.AddForce(attackedVelocity, ForceMode2D.Impulse);

            // 충돌데미지 설정해주기
            health -= 5;

            // 무적타임
            if (health > 1)
            {
                isUnBeatTime = true;
                StartCoroutine("UnBeatTime");
            }
        }

        else if (other.gameObject.tag == "monster")
        {
            if (health >= 1)
            {
                isUnBeatTime = true;
                StartCoroutine("UnBeatTime");
            }
        }
    }

    // 피격 시 무적
    IEnumerator UnBeatTime()
    {
        int countTime = 0;

        while (countTime < 10)
        {
            if (countTime % 2 == 0)
                spriteRenderer.color = new Color32(255, 255, 255, 90);
            else
                spriteRenderer.color = new Color32(255, 255, 255, 180);

            yield return new WaitForSeconds(0.2f);

            countTime++;
        }

        spriteRenderer.color = new Color32(255, 255, 255, 255);
        isUnBeatTime = false;

        yield return false;
    }

    // 플립 함수(스프라이트 좌우반전)
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    // 죽음(상태) 함수
    void Die()
    {
        isDie = true;
        Debug.Log("GameOver");
    }

    // 충돌감지 함수
    private void OnCollisionEnter(Collision col)
    {
        /// 바닥감지 함수
        if (col.gameObject.tag == "Ground")
        {
            isJumping = false;    //Ground에 닿으면 isJumping은 false
            isGrounded = true;    //Ground에 닿으면 isGround는 true
            jumpCount = 2;  //Ground에 닿으면 점프횟수가 2로 초기화됨
        }
    }

    void Update()
    {
        // 체력
        if (health == 0)
        {
            if (!isDie)
                Die();

            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)//입력키가 스페이스면 실행함
        {
            jumpCount--;//점프할때 마다 점프가능횟수 감소
            isJumping = true;
            myRigidbody.AddForce(new Vector3(0, 1, 0) * speed, ForceMode2D.Impulse); //위방향으로 올라가게함
            //Debug.Log(jumpCount);
        }
        if (isGrounded == true && jumpCount == 0)
        {
            isJumping = false;
            jumpCount = 2;
        }
    }
}