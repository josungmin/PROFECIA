using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    public Slider HPBar;
    public Image energyBar;
    public Text level;
    public Image expBar;
    public GameObject HeadUpPosition;
    private PlayerStat attribute;
    private SwitchingWeapons weap;

    void Start()
    {
        attribute = gameObject.GetComponent<PlayerStat>();
        weap = gameObject.GetComponent<SwitchingWeapons>();
    }

    void Update()
    {
        HPBar.value = (float)attribute.currentHP / (float)attribute.basicStat.maxHP;
        HPBar.transform.position = HeadUpPosition.transform.position;

        if (weap.selectedWeapon == 0)
        {
            energyBar.fillAmount = Map(attribute.swordEnergy, attribute.basicStat.maxEnergy);
        }
        else energyBar.fillAmount = Map(attribute.gauntletEnergy, attribute.basicStat.maxEnergy);

        level.text = "" + attribute.statPoint.level;
        expBar.fillAmount = Map(attribute.currentExp, attribute.basicStat.needEXP[attribute.statPoint.level]);
    }

    private float Map(float current ,float max)
    {
        return current / max;
    }
}
