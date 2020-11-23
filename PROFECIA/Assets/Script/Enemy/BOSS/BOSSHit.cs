using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSSHit : MonoBehaviour
{
    public BossMove BosstMove;
    public PlayerStat targetAttribute;
    public GameObject Target;

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.Find("Player");
        targetAttribute = Target.GetComponent<PlayerStat>();
        //BosstMove = gameObject.GetComponent<BossMove>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(BosstMove.hitRange == true)
        {
            if (collision.gameObject.tag == "Player")
            {
                targetAttribute.currentHP -= BosstMove.AttackPower;
            }
        }
    }
}
