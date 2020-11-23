using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Hit : MonoBehaviour
{
    [SerializeField] private ObjectMove objectMove;

    private PlayerStat targetAttribute;
    public EnemyStat myAttribute;
    public GameObject Target;

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.Find("Player");
        targetAttribute = Target.GetComponent<PlayerStat>();
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
                targetAttribute.currentHP -= myAttribute.myStat.enemyList[objectMove.enemyID -1].attackPower;
            }
        }
    }
}

