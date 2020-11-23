using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWeaponSpriteController : MonoBehaviour
{
    SwitchingWeapons sWeapons;
    public int currentWeapNum;
    Animator anim;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        anim.SetInteger("SelectedWeaponNum", 0);
        sWeapons = GetComponent<SwitchingWeapons>();
    }

    // Update is called once per frame
    void Update()
    {
        currentWeapNum = sWeapons.selectedWeapon;
        if (currentWeapNum == 0)
        {
            anim.SetInteger("SelectedWeaponNum", 0);
            spriteRenderer.enabled = false;
            currentWeapNum = sWeapons.selectedWeapon;
        }
        else
        {
            anim.SetInteger("SelectedWeaponNum", 1);
            spriteRenderer.enabled = true;
            currentWeapNum = sWeapons.selectedWeapon;
        }

    }
}
