using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4Hit : MonoBehaviour
{
    [SerializeField] private Object4Move objectMove;

    public CircleCollider2D Hitcircle;
    public bool hitRangeIn;

    private PlayerStat targetAttribute;
    public EnemyStat myAttribute;
    public GameObject Target;

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.Find("Player");
        targetAttribute = Target.GetComponent<PlayerStat>();
        Hitcircle.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (objectMove.hitRange == true)
        {
            Hitcircle.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            targetAttribute.currentHP -= myAttribute.myStat.enemyList[objectMove.enemyID - 1].attackPower;
            Hitcircle.enabled = false;
        }
    }
}