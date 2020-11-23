using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS2Hit : MonoBehaviour
{
    public BOSS2Move BosstMove;
    public PlayerStat targetAttribute;
    public GameObject Target;

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.Find("Player");
        targetAttribute = Target.GetComponent<PlayerStat>();
        //BosstMove = gameObject.GetComponent<BOSS2Move>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (BosstMove.hitRange == true)
        {
            if (collision.gameObject.tag == "Player")
            {
                targetAttribute.currentHP -= BosstMove.AttackPower;
            }
        }
    }
}