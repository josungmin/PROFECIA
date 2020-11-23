using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeMap : MonoBehaviour {

    private Rect buttonPos;
    public MyScriptableObject MyScriptableObject;

    void Start()
    {
        buttonPos = new Rect(10.0f, 10.0f, 150.0f, 50.0f);
    }

    private void Update()
    {

    }
    /*
    void OnGUI()
    {
        if (GUI.Button(buttonPos, "Go next scene"))
        {
            MyScriptableObject.floorNum += 1;
            SceneManager.LoadScene("Sample", LoadSceneMode.Single);
        }if(GUI.Button(new Rect(10.0f, 40.0f, 150.0f, 50.0f), "Quit"))
        {
            Application.Quit();
        }
    }
    */
}
