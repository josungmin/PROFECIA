using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameMaster : MonoBehaviour
{
    public Text InputText;
    public Text DoorText;

    public PlayerBasicStat basicStat;
    public StatPointData statPoint;

    private void OnApplicationQuit()
    {
        basicStat.playTime = 0;
        statPoint.exp = 0;
    }
}
