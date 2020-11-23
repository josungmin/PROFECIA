using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour {

    private PlayerStat stat;
    
    public int healHP = 10; // 힐팩 먹으면 회복하는 체력의 양

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        // HP회복
        if (other.gameObject.tag == "Player")// 힐 태그 붙은 오브젝트와 충돌
        {
            stat = other.GetComponent<PlayerStat>();

            // 최대HP보다 현재HP가 낮으면 힐
            if (stat.currentHP < stat.basicStat.maxHP + stat.statPoint.HP * 10)
            {
                stat.currentHP += healHP;
                //Debug.Log("regain HP"); // 체력회복 확인
                Destroy(gameObject); // 먹은 힐팩 삭제
            }
            // 풀피면 회복 안함 = 힐팩 안먹음
            else if (stat.currentHP >= stat.basicStat.maxHP + stat.statPoint.HP * 10)
            {
                stat.currentHP = stat.basicStat.maxHP + stat.statPoint.HP * 10;
            }
        }
    }
}
