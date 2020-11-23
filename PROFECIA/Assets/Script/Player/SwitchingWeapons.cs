using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingWeapons : MonoBehaviour
{
    public int selectedWeapon;
    public int WeapNum; // Sword = 0, Gauntlet = 1
    public Transform player;

    public int previousSelectedWeapon;
    //private Player player;
    Animator anim;

    public GameObject sword;
    public GameObject gauntlet;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        
        selectedWeapon = 0; // 소드로 시작
        SelectWeapon(selectedWeapon);
        anim.SetInteger("SelectedWeaponNum", selectedWeapon);
        WeapNum = 0;
    }

    void Update()
    {
        if(selectedWeapon == 0 || Input.GetKeyDown(KeyCode.Q)) // 기본, Q누르기
        {
            previousSelectedWeapon = selectedWeapon;
            selectedWeapon = 0; // 소드
            anim.SetInteger("SelectedWeaponNum", selectedWeapon);
        }
        if(selectedWeapon == 1 || Input.GetKeyDown(KeyCode.W) && transform.childCount >= 2) // W누르기
        {
            previousSelectedWeapon = selectedWeapon;
            selectedWeapon = 1; // 건틀릿
            anim.SetInteger("SelectedWeaponNum", selectedWeapon);
        }
                
        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon(selectedWeapon);
        }
    }

    void SelectWeapon(int weaNum)
    {
        if(weaNum == 0)
        {
            sword.gameObject.SetActive(true);
            gauntlet.gameObject.SetActive(false);
        }
        else if(weaNum == 1)
        {
            gauntlet.gameObject.SetActive(true);
            sword.gameObject.SetActive(false);
        }
        previousSelectedWeapon = selectedWeapon;
    }
}
