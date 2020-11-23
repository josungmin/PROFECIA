using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    //private BoxCollider2D boxColider;
    public LayerMask layerMask;

    public float speed; /// 플레이어 이동속도

    private Vector3 vector;

    public Queue<string> queue; //FIFO, 선입선출 자료구조 queue <- a 이후에 b, 이후에 c
    // a,b,c순서로 빠짐
    
    private bool canMove = true;

    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        //boxColider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    IEnumerator MoveCoroutine()
    {
        while(Input.GetAxisRaw("Horizontal") != 0)
        {
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if (vector.x != 0)
                vector.y = 0;

            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);
            animator.SetBool("Walking", true);

            yield return new WaitForSeconds(0.01f);
        }
        animator.SetBool("Walking", false);
        canMove = true;

    }

    // Update is called once per frame
    void Update()
    {
        queue = new Queue<string>();

        if (canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }
       
    }
}