using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    public Rigidbody2D rb;
    PlayerStat playerStat;

    //스킬이펙트 기본 진행방향(좌->우)
    float speed = 15f;
    public Vector2 direction = new Vector2(1, 0);

    public bool facingRight = true;

    private void Start()
    {
        rb.velocity = transform.right * speed;
        playerStat = GameObject.Find("Player").GetComponent<PlayerStat>();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "map" || other.gameObject.tag == "Enemy")
        {
            ObjectMove enemy = other.gameObject.GetComponent<ObjectMove>();
            Object3Move enemy3 = other.gameObject.GetComponent<Object3Move>();
            Object4Move enemy4 = other.gameObject.GetComponent<Object4Move>();
            Object5Move enemy5 = other.gameObject.GetComponent<Object5Move>();
            BossMove boss = other.gameObject.GetComponent<BossMove>();
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
            Destroy(this.gameObject);
        }
    }
}