using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorNumControl : MonoBehaviour
{
    [SerializeField] SpriteRenderer prevFloorNum;
    [SerializeField] SpriteRenderer currentFloorNum;
    public FloorSpriteList list;
    public MyScriptableObject bg;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        prevFloorNum.sprite = list.floorNumSprites[bg.floorNum - 1];
        currentFloorNum.sprite = list.floorNumSprites[bg.floorNum];
    }

    // Update is called once per frame
    void Update()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Floor") == false)    // && anim.GetCurrentAnimatorStateInfo(0).IsName("Arrow") == false
        {
            SceneManager.UnloadSceneAsync("InElevator");
        }
    }

    public void SetAnimBoolParameter()
    {
        anim.SetBool("isEnd", true);
    }
}
