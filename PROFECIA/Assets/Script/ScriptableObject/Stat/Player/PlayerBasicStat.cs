using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBasicStat", menuName = "Basic Stat/Player Basic Stat", order = 1)]
public class PlayerBasicStat : ScriptableObject
{
    [Header("Level")]
    public int[] needEXP;

    [Header("HP")]
    public int maxHP = 100;

    [Header("Attack")]
    public int attackPower = 10;
    public float attackSpeed = 1.0f;
    public float attackRange = 0.5f;

    [Header("Movement")]
    public float movementSpeed = 5;

    [Header("Energy")]
    public float maxEnergy = 15;
    public float timeToCharge = 450; //단위 : 초

    [Header("Distance to Enemy")]
    public float width = 0.64f;

    [Header("Play Time")]
    public float playTime = 0;
}
