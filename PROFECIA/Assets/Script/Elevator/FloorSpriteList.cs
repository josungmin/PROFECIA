using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "FloorNum/List", order = 1)]
public class FloorSpriteList : ScriptableObject
{
    public string objectName = "New Floor Sprite List";
    public Sprite[] floorNumSprites;
    //public int floorNum = 1;    //임시 층 수 변수- Background data의 floornum을 불러와 쓸 예정

}
