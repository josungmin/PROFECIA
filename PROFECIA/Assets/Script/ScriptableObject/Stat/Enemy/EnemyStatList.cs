using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatList", menuName = "Basic Stat/Enemy Stat List", order = 1)]
public class EnemyStatList : ScriptableObject
{
    public List<EnemyBasicStat> enemyList;
}
