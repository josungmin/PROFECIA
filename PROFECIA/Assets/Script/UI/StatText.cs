using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatText : MonoBehaviour {
    public Text levelText;
    public Text HPText;
    public Text atkText;
    public Text energyText;
    public Text SPText;

    public StatPointData stat;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        levelText.text = "LV: " + stat.level;
        HPText.text = stat.HP + " / 20";
        atkText.text = stat.attack + " / 20";
        energyText.text = stat.energy + " / 20";
        SPText.text = "Sp: " + stat.statPoint;
    }
}
