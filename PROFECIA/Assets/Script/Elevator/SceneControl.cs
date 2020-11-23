using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public MyScriptableObject bg;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(100f, 20f, 150f, 40f), "Next Floor"))
        {
            bg.floorNum += 1;
            SceneManager.LoadScene("InElevator", LoadSceneMode.Additive);
        }
        if (GUI.Button(new Rect(100f, 60f, 150f, 40f), "Unload Scene"))
        {
            SceneManager.UnloadSceneAsync("InElevator");
        }
    }

    private void OnApplicationQuit()
    {
        bg.floorNum = 1;
    }
}
