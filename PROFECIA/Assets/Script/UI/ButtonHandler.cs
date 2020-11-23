using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public MyScriptableObject MyScriptableObject;
    public AudioSource audioSource;
    public LevelChanger level;

    public void StartButton()
    {
        MyScriptableObject.isShuffle = false;
        level.FadeToLevel("Main");
        //SceneManager.LoadScene("Main");
    }

    public void OptionButton()
    {
        //SceneManager.LoadScene("Option");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void TitleButton()
    {
        //level.FadeToLevel("Start");
        SceneManager.LoadScene("Start");
    }

    public void ReplayButton()
    {
        SceneManager.LoadScene("Main");
    }

    public void ButtonClickSound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
