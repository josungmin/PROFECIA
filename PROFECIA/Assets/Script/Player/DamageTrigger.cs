using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    public EnemyStat myAttribute;
    public PlayerStat playerStat;
    public int effectDamage = 7; //이펙트고유데미지 + 플레이어공격력으로 스킬공격력결정

    // Start is called before the first frame update
    void Start()
    {
        playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // 펄스건 이펙트에 닿은 경우, 넉백되지 않음.
        if (myAttribute.currentHP >= 1)
        {
            if (col.tag == "Effect")
                myAttribute.currentHP -= (effectDamage + playerStat.basicStat.attackPower);
            else if (col.tag == "mainEffect")
            {
                Debug.Log("G_mainAttack");
                myAttribute.currentHP -= playerStat.basicStat.attackPower;
            }
        }
    }
}
