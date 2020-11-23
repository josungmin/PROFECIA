using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy1UI : MonoBehaviour
{
    public Slider HPBar;
    public GameObject HeadUpPosition;
    private EnemyStat attribute;

    void Start()
    {
        attribute = gameObject.GetComponent<EnemyStat>();
    }

    void Update()
    {
        HPBar.value = (float)attribute.currentHP / (float)attribute.myStat.enemyList[0].maxHP;
        HPBar.transform.position = HeadUpPosition.transform.position;
    }
}
