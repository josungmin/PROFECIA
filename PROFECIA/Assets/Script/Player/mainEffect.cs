using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainEffect : MonoBehaviour
{
    PlayerStat playerStat;

    private void Start()
    {
        playerStat = GameObject.Find("Player").GetComponent<PlayerStat>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            ObjectMove enemy = other.gameObject.GetComponent<ObjectMove>();
            Object3Move enemy3 = other.gameObject.GetComponent<Object3Move>();
            Object4Move enemy4 = other.gameObject.GetComponent<Object4Move>();
            Object5Move enemy5 = other.gameObject.GetComponent<Object5Move>();
            BossMove boss = other.gameObject.GetComponent<BossMove>();
            BOSS2Move boss2 = other.gameObject.GetComponent<BOSS2Move>();
            if (enemy != null)
            {
                enemy.myAttribute.currentHP -= playerStat.currentPower;
            }
            if (enemy3 != null)
            {
                enemy3.myAttribute.currentHP -= playerStat.currentPower;
            }
            if (enemy4 != null)
            {
                enemy4.myAttribute.currentHP -= playerStat.currentPower;
            }
            if (enemy5 != null)
            {
                enemy5.myAttribute.currentHP -= playerStat.currentPower;
            }
            if (boss != null)
            {
                boss.CurrentHP -= playerStat.currentPower;
            }
            if (boss2 != null)
            {
                boss2.CurrentHP -= playerStat.currentPower;
            }
            Destroy(this.gameObject);
        }
    }
}
