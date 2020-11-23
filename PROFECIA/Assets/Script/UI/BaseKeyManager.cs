using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseKeyManager : MonoBehaviour
{
    [SerializeField] ScreenManager screenManager;
    [SerializeField] Animator anim;
    Scene scene;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            scene = SceneManager.GetSceneByName("InElevator");
            if(scene.isLoaded == true)
            {
                SceneManager.UnloadSceneAsync("InElevator");
            }else if (anim.GetBool("Open") == true)
            {
                screenManager.CloseCurrent();
            }else
            {
                screenManager.OpenPanel(anim);
            }
        }
    }
}
