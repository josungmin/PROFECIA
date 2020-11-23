using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour {

    Image m_image;
    [SerializeField] Sprite[] sprites;
    [SerializeField] SwitchingWeapons weapons;

	// Use this for initialization
	void Start () {
        m_image = gameObject.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        m_image.sprite = sprites[weapons.selectedWeapon];
	}
}
