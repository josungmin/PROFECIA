using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5Hit : MonoBehaviour
{
    [SerializeField] private Object5Move objectMove;

    private PlayerStat targetAttribute;
    public EnemyStat myAttribute;
    public GameObject Target;

    public bool EnemyAttacking;

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.Find("Player");
        targetAttribute = Target.GetComponent<PlayerStat>();
        EnemyAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (objectMove.hitRange == true)
        {
            if (collision.gameObject.tag == "Player")
            {
                EnemyAttacking = true;
                targetAttribute.currentHP -= myAttribute.myStat.enemyList[objectMove.enemyID - 1].attackPower;
            }
        }
    }
}