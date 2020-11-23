using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour {

    [SerializeField] SpriteRenderer render;
    [SerializeField] AudioSource source;

    public MyScriptableObject MyScriptableObject;

    // Use this for initialization
    void Start () {
        /*
        if(MyScriptableObject.isShuffle == false)
        {
            MyScriptableObject.Shuffle(MyScriptableObject.backgroundSprites);
        }
        */
        if(MyScriptableObject.isShuffle == false)
        {
            MyScriptableObject.ShuffleList(MyScriptableObject.backgrounds);
        }
        /*
        if(MyScriptableObject.floorNum > 15)
        {
            render.sprite = MyScriptableObject.backgroundSprites[3];
        } else if(MyScriptableObject.floorNum > 10) {
            render.sprite = MyScriptableObject.backgroundSprites[2];
        } else if(MyScriptableObject.floorNum > 5) {
            render.sprite = MyScriptableObject.backgroundSprites[1];
        } else if(MyScriptableObject.floorNum > 0) {
            render.sprite = MyScriptableObject.backgroundSprites[0];
        }
        
        if(MyScriptableObject.floorNum > 15)
        {
            render.sprite = MyScriptableObject.backgrounds[3].backSprite;
            source.clip = MyScriptableObject.backgrounds[3].audio;
            source.Play();
        } else if(MyScriptableObject.floorNum > 10)
        {
            render.sprite = MyScriptableObject.backgrounds[2].backSprite;
            source.clip = MyScriptableObject.backgrounds[2].audio;
            source.Play();
        } else if(MyScriptableObject.floorNum > 5)
        {
            render.sprite = MyScriptableObject.backgrounds[1].backSprite;
            source.clip = MyScriptableObject.backgrounds[1].audio;
            source.Play();
        } else if(MyScriptableObject.floorNum > 0)
        {
            render.sprite = MyScriptableObject.backgrounds[0].backSprite;
            source.clip = MyScriptableObject.backgrounds[0].audio;
            source.Play();
        }
        */
        if (MyScriptableObject.floorNum > 15)
        {
            render.sprite = MyScriptableObject.backgrounds[3].backSprite;
            source.clip = MyScriptableObject.backgrounds[3].audio;
            source.Play();
        }
        else if (MyScriptableObject.floorNum > 4)
        {
            render.sprite = MyScriptableObject.backgrounds[2].backSprite;
            source.clip = MyScriptableObject.backgrounds[2].audio;
            source.Play();
        }
        else if (MyScriptableObject.floorNum > 2)
        {
            render.sprite = MyScriptableObject.backgrounds[1].backSprite;
            source.clip = MyScriptableObject.backgrounds[1].audio;
            source.Play();
        }
        else if (MyScriptableObject.floorNum > 0)
        {
            render.sprite = MyScriptableObject.backgrounds[0].backSprite;
            source.clip = MyScriptableObject.backgrounds[0].audio;
            source.Play();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnApplicationQuit() //게임 종료 시 초기화(게임오버 시에도 동일한 조건으로 초기화)
    {
        MyScriptableObject.floorNum = 1;
        MyScriptableObject.isShuffle = false;
    }
}
