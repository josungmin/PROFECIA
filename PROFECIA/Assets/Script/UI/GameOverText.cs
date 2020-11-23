using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour
{
    public Text playedTime;
    public Text currentFloor;
    public Text earnedEXP;
    public Text currentLevel;

    public PlayerBasicStat stat;
    public StatPointData statPoint;
    public MyScriptableObject bgl;

    private int minute;
    private float second;

    // Start is called before the first frame update
    void Start()
    {
        minute = (int)stat.playTime / 60;
        second = Mathf.Floor((stat.playTime % 60.0f) * 100) * 0.01f;

        playedTime.text = minute + " : " + second;
        currentFloor.text = bgl.floorNum + " 층";
        earnedEXP.text = " " + statPoint.exp;
        //currentLevel.text = " " + statPoint.level;
    }

    // Update is called once per frame
    void Update()
    {
        currentLevel.text = " " + statPoint.level;
    }
}
