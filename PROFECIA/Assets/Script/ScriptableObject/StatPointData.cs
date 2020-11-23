using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatData", menuName = "Status/Data", order = 1)]
public class StatPointData : ScriptableObject
{
    public int level = 1;
    public float exp = 0;
    public int HP = 0;
    public int attack = 0;
    public int energy = 0;
    public int statPoint = 0;
}
