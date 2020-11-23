using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifier : MonoBehaviour {

    public MyScriptableObject bg;
    public PlayerBasicStat basicStat;
    public StatPointData stat;

    // Use this for initialization
    void Start() {
        stat.exp = 0;
        basicStat.playTime = 0;
        bg.floorNum = 1;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //P키를 입력하면 스텟 포인트 1증가(테스트용)
            stat.statPoint += 1;    
        }
    }

    //타입에 따라 스텟 수치를 1증가
    public void Add(string statType)
    {
        //HP 스텟 증가
        if (statType == "HP")
        {
            if (stat.statPoint > 0)
            {
                stat.HP += 1;
                stat.statPoint -= 1;
            }
            else Debug.Log("StatPoint is Zero");
        }
        //Atk(공격력) 스텟 증가
        else if (statType == "ATK")
        {
            if (stat.statPoint > 0)
            {
                stat.attack += 1;
                stat.statPoint -= 1;
            }
            else Debug.Log("StatPoint is Zero");
        }
        //에너지 스텟 증가
        else if (statType == "ENERGY")
        {
            if (stat.statPoint > 0)
            {
                stat.energy += 1;
                stat.statPoint -= 1;
            }
            else Debug.Log("StatPoint is Zero");
        }
        else Debug.Log("Type Error");   //타입 입력 오류(Canvas > "statType"Button 에서 찾아 수정하면 됨)
        
    }

    //타입에 따라 스텟 수치를 1 감소
    public void Minus(string statType)
    {
        //HP 스텟 감소
        if (statType == "HP")
        {
            if (stat.HP > 0)
            {
                stat.HP -= 1;
                stat.statPoint += 1;
            }
            else Debug.Log("HP Stat is Zero");
        }
        //Atk(공격력) 스텟 감소
        else if (statType == "ATK")
        {
            if (stat.attack > 0)
            {
                stat.attack -= 1;
                stat.statPoint += 1;
            }
            else Debug.Log("Atk Stat is Zero");
        }
        //에너지 스텟 감소
        else if (statType == "ENERGY")
        {
            if (stat.energy > 0)
            {
                stat.energy -= 1;
                stat.statPoint += 1;
            }
            else Debug.Log("EP Stat is Zero");
        }
        else Debug.Log("Type Error");   //타입 입력 오류(Canvas > "statType"Button 에서 찾아 수정하면 됨)
        
    }
}
