using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] PlayerStat stat;
    [SerializeField] private Transform swordEffect;
    SwitchingWeapons weapon;

    public Animator anim;
    public float animSpeed = 1.0f;

    public int weapNum; // 선택한 무기
    public int preWeapNum; // 직전에 선택한 무기
    private int skillNum; // 스킬번호

    int buffATK = 7;
    int originalATK;

    bool isCoolDown = false;
    bool isCanceled = false;
    bool showingEffect = false;
    bool isGauntletCoolDown = false;

    public GameObject gauntletEffect;
    
    [SerializeField] Transform spawnPoint;

    //         사운드 관련          //
    AudioSource aud;
    public AudioClip S_skillSound;  //소드 스킬
    public AudioClip G_skillSound;  //소드 스킬
    public AudioSource audioSource; //출력 관련 제어

    private void Start()
    {
        stat = gameObject.GetComponent<PlayerStat>();
        weapon = gameObject.GetComponent<SwitchingWeapons>();
        originalATK = stat.currentPower;

        aud = GetComponent<AudioSource>();
    }

    public void Update()
    {
        weapNum = weapon.selectedWeapon;

        // 현재무기가 소드일 때 스킬사용
        if (weapNum == 0)
        {
            // 쿨타임이 없는 상태 + 사용스킬에 필요한 에너지 여분이 있는 상태 + 스킬사용 = 스킬사용
            if (isCoolDown == false && stat.swordEnergy >= 3 && Input.GetKeyDown(KeyCode.D))
            {
                preWeapNum = weapNum;
                SwordSkillEffect();
                PlayerSound(S_skillSound);
            }
            // 쿨타임이 남은 상태 + 스킬사용 = 스킬 중복사용 불가
            else if (Input.GetKeyDown(KeyCode.D) && isCoolDown == true)
            {
                return;
            }
        }
        // 현재무기가 건틀릿일 때 스킬사용
        else
        {
            // 사용 스킬에 필요한 에너지 여분이 있는 상태 + 스킬사용 = 스킬사용
            if (Input.GetKeyDown(KeyCode.D) && stat.gauntletEnergy >= 2)
            {
                preWeapNum = weapNum;
                GauntletSkillEffect();
            }
        }

        // 소드 스킬 사용 중, 건틀릿으로 무기변경 시 스킬효과 캔슬
        if (isCoolDown == true && showingEffect == true && preWeapNum == 0)
        {
            // 소드
            if (weapNum == 1)
            {
                SwordEffectDone();
            }
            else
            {
                return;
            }
        }

        // 소드스킬 사용 중 무기교체(건틀릿) 후 다시 무기교체(소드) 시 스킬상태 유지
        if (isCoolDown == true && isCanceled == true && showingEffect == true && weapNum != preWeapNum)
        {
            swordEffect.gameObject.SetActive(true);
            stat.currentPower = originalATK;
            animSpeed = 1.7f; // 공속업
            anim.speed = animSpeed;

            preWeapNum = weapNum;
            isCanceled = false;
            Debug.Log("Maintaining Canceled Effect");
        }
        else if (showingEffect == false && isCoolDown == true)
        {
            swordEffect.gameObject.SetActive(false);
            isCanceled = true;
            stat.currentPower = originalATK;
            animSpeed = stat.basicStat.attackSpeed; // 공속 원상복귀
            anim.speed = animSpeed;

            preWeapNum = weapNum;
        }
    }
    public void SwordSkillEffect()
    {
        if (!isCoolDown)
        {
            // 소드 공업, 공속업
            if (weapNum == 0)
            {
                if(stat.swordEnergy >= 3)
                {
                    swordEffect.gameObject.SetActive(true);
                    stat.swordEnergy -= 3; // 에너지 소모
                    stat.currentPower += buffATK; // 공업 
                    animSpeed = 1.7f; // 공속업
                    anim.SetFloat("attackSpeed", animSpeed);

                    StartCoroutine(IESwordSkillState());

                    Debug.Log("Start Sword Skill Effect");
                }
                else
                {
                    Debug.Log("Not Enough Energy");
                }

                preWeapNum = weapNum;
            }
        }
        else
        {
            swordEffect.gameObject.SetActive(false);
            stat.currentPower = originalATK;
            animSpeed = 1.0f; // 공속업 정상화
            anim.SetFloat("attackSpeed", animSpeed);
            Debug.Log("Skill Effect is done");
        }
    }

    public void SwordEffectDone()
    {
        swordEffect.gameObject.SetActive(false);
        isCanceled = true;
        stat.currentPower = originalATK;
        animSpeed = 1.0f; // 공속업 정상화
        anim.speed = animSpeed;

        preWeapNum = weapNum;
    }

    public void GauntletSkillEffect()
    {
        // 건틀릿 (a.k.a 펄스건 이즈리얼)
        if (weapNum == 1 && !isGauntletCoolDown)
        {
            if (stat.gauntletEnergy >= 2)
            {
                stat.gauntletEnergy -= 2; // 에너지 소모
                StartCoroutine(IEGauntletSkillState());

                Debug.Log("Start Gauntlet Skill Effect");
            }
            else
            {
                Debug.Log("Not Enough Energy");
            }
            preWeapNum = weapNum;
        }
    }

    private IEnumerator IESwordSkillState()
    {
        isCoolDown = true;
        showingEffect = true;
        yield return new WaitForSeconds(7f); // 지속시간

        showingEffect = false; // 이펙트꺼짐
        SwordSkillEffect();
        yield return new WaitForSeconds(15f); // 쿨타임
        isCoolDown = false;
        isCanceled = false;
    }

    private IEnumerator IEGauntletSkillState()
    {
        isGauntletCoolDown = true;
        yield return new WaitForSeconds(0.2f); // 애니메이션 후 이펙트 날라감

        Instantiate(gauntletEffect, spawnPoint.position, spawnPoint.rotation);
        PlayerSound(G_skillSound);

        yield return new WaitForSeconds(3.5f); // 재사용 쿨타임.

        isGauntletCoolDown = false;
    }

    //효과음 재생 메소드
    void PlayerSound(AudioClip clip)
    {
        audioSource.volume = 0.1f; //0.0f ~ 1.0f사이의 숫자로 볼륨을 조절
        aud.PlayOneShot(clip);
    }
}

/*
 * 190201 UPDATE REPORT
 * 1. 소드 이펙트
 * 소드 스킬효과는 소드상태에선 계속 유지.
 * 대신 건틀릿으로 변경하면 건틀릿에는 효과가 유지되지 않으며,
 * 스킬효과가 종료되기 전에 다시 소드로 변경하면 스킬효과 계속 유지.
 * 
 * 2. 건틀릿 이펙트
 * 건틀릿 재사용 쿨타임이 갖춰졌음에도 사방으로 날라가는 문제발생
 * 스폰포인트를 건틀릿으로 조정, 공격 애니메이션이 끝나면 날아가도로고 조정.
 */