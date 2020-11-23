using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

    private StageCheck stageCheck;

    void Start()
    {
        stageCheck = GameObject.FindWithTag("Player").GetComponent <StageCheck> ();
    }

	void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            stageCheck.cardCheck = true;
            Destroy(gameObject);
        }
    }
}
