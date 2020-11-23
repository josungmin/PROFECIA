using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public static PlayerStat instance;
    public ObjectMove hitCounts;
    public SwitchingWeapons weapons;
    //필요 경험치, 최대체력, 공격력, 공격속도와 범위, 이속, 최대 에너지, 충전 시간, 플레이타임
    public PlayerBasicStat basicStat;   
    public StatPointData statPoint;     //레벨, 스텟포인트와 사용 내용
    
    //public int characterLevel;
    //public int[] needExp;
    public int currentExp;

    public int currentHP;       // 현재HP
    //public int maxHP;       // 최대HP

    public int currentPower;        // 공격력
    //public int atkSpeed;        // 공격속도
    //public float attackRadius;      // 공격범위

    //public float movementSpeed;     //이동속도

    public float swordEnergy = 10;     // 현재 소드에너지      // 임시지정
    public float gauntletEnergy;     // 현재 건틀릿에너지      // 임시지정
    //public float maxEnergy = 15;  // 최대에너지    //임시지정
    //public float minEnergy = 0;
    float changePerSecond;    
    //public float timeToChange = 450;  // 최소에너지(0)부터 최대에너지(15)까지 회복하는데 걸리는 최소시간(초)

    public int hits;        // 몬스터에게 피해를 입힌 횟수
    /// <히트카운트시스템>
    /// 
    /// 히트카운트가 Stage 1에서는 5의배수, Stage 2에서는 7의 배수,
    /// Stage 3에서는 10의 배수, Stage 4에서는 14의 배수 등...
    /// 스테이지별로 특정 배수가 될때마다 에너지 한칸 리젠
    /// 
    /// </히트카운트시스템>


    //public float width;
    // Start is called before the first frame update
    void Start()
    {
        currentPower = basicStat.attackPower + (statPoint.attack * 5);

        // 처음 게임실행 시 소드, 건틀릿 에너지 최대치
        swordEnergy = PlayerPrefs.GetFloat("SwordEnergy", 0); //basicStat.maxEnergy + (statPoint.energy * 10);
        gauntletEnergy = PlayerPrefs.GetFloat("GauntletEnergy", 0);

        currentExp = PlayerPrefs.GetInt("CurrentEXP", 0);

        //weapons = GetComponent<SwitchingWeapons>();

        instance = this;

        hits = 0;
        //hitCounts = gameObject.GetComponent<ObjectMove>();
        changePerSecond = (basicStat.maxEnergy - 0) / basicStat.timeToCharge;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentExp >= basicStat.needEXP[statPoint.level])
        {
            statPoint.exp += currentExp;
            statPoint.level++;
            statPoint.statPoint++;
            currentExp = 0;
        }

        ///<기본에너지시스템>
        ///
        ///현재에너지가 최대에너지 미만이면, 일정시간마다 에너지 회복
        ///
        /// </기본에너지시스템>
        swordEnergy = Mathf.Clamp(swordEnergy + changePerSecond * Time.deltaTime, 0, basicStat.maxEnergy + statPoint.energy * 10);
        gauntletEnergy = Mathf.Clamp(gauntletEnergy + changePerSecond * Time.deltaTime, 0, basicStat.maxEnergy + statPoint.energy * 10);

        currentHP = Mathf.Clamp(currentHP, 0, basicStat.maxHP + statPoint.HP * 10);

        /* 몬스터에게 데미지가 들어감
        if (hitCounts.checkHittedCount > hits)
        {
            // 적을 공격하면 히트가 올라감
            hits += 1;
            Debug.Log(hits);

            if (hits % 5 == 0)
            {
                // 현재 소드를 장착했을 때
                if (weapons.selectedWeapon == 0)
                {
                    HitCountSystem(weapons.selectedWeapon);
                }
                // 현재 건틀릿을 장착했을 때
                else if (weapons.selectedWeapon == 1)
                {
                    HitCountSystem(weapons.selectedWeapon);
                }
            }
            else
                return;
        }
        */
    }

    
    public void HitCountSystem(int weaponNum)
    {
        // 장착무기가 소드일 때 소드에너지 충전됨
        if (weaponNum == 0)
        {
            swordEnergy += 1;
            Debug.Log("Regain Energy");

            // 합쳤을 때 스테이지 받아와서 스테이지마다 비율 다르게하기
            /*
            if (stageCount > 15)
            {
                if (hits % 19 == 0)
                {
                    Debug.Log("Got Energy");
                    swordEnergy += 1;
                }
            
                else if(stageCount > 10)
                {
                    if (hits % 14 == 0)
                    {
                        swordEnergy += 1;
                    }
                }
                else if(stageCount > 5)
                 {
                    if (hits % 10 == 0)
                    {
                        swordEnergy += 1;
                    }
                }
                else
                 {
                    if (hits % 7 == 0)
                    {
                        swordEnergy += 1;
                    }
                }
                else if(16 <= stageCount <= 20)
                 {
                    if (hits % 5 == 0)
                    {
                        Debug.Log("Got Energy");
                        swordEnergy += 1;
                    }
                }
            }
            */
        }

        // 장착무기가 건틀릿일 때 건틀릿에너지 충전됨
        else if (weaponNum == 1)
        {
            gauntletEnergy += 1;
            Debug.Log("Regain Energy");

            // 합쳤을 때 스테이지 받아와서 스테이지마다 비율 다르게하기
            /*
            if (stageCount > 15)
            {
                if (hits % 19 == 0)
                {
                    Debug.Log("Got Energy");
                    gauntletEnergy += 1;
                }
            
                else if(stageCount > 10)
                {
                    if (hits % 14 == 0)
                    {
                        gauntletEnergy += 1;
                    }
                }
                else if(stageCount > 5)
                 {
                    if (hits % 10 == 0)
                    {
                        gauntletEnergy += 1;
                    }
                }
                else
                 {
                    if (hits % 7 == 0)
                    {
                        gauntletEnergy += 1;
                    }
                }
                else if(16 <= stageCount <= 20)
                 {
                    if (hits % 5 == 0)
                    {
                        Debug.Log("Got Energy");
                        gauntletEnergy += 1;
                    }
                }
            }
            */
        }
    }
}
