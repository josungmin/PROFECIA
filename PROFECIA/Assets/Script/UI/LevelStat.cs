using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class LevelStat
{
    [SerializeField]
    private Level level;

    [SerializeField]
    private float maxVal;

    [SerializeField]
    private float currentVal;

    [SerializeField]
    private float levelNum;

    public float CurrentVal
    {
        get
        {
            return currentVal;
        }

        set
        {
            currentVal = Mathf.Clamp(value, 0, MaxVal);
            level.Value = currentVal;
        }
    }

    public float MaxVal
    {
        get
        {
            return maxVal;
        }

        set
        {
            maxVal = value;
            level.MaxValue = maxVal;
        }
    }

    public float LevelNum
    {
        get
        {
            return levelNum;
        }

        set
        {
            levelNum = value;
            level.LevelNumber = levelNum;
        }
    }

    public void Initialize()
    {
        this.MaxVal = maxVal;
        this.LevelNum = levelNum;
        this.CurrentVal = currentVal;
    }

    public void HandleLevel()
    {
        if(CurrentVal == MaxVal)
        {
            this.CurrentVal = 0;
            this.LevelNum = levelNum + 1;
        }
    }
}
