using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;
    string levelToLoad;


    // Start is called before the first frame update
    void Start()
    {
        levelToLoad = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeToLevel(string levelName)
    {
        levelToLoad = levelName;
        animator.SetTrigger("Fade_Out");
    }

    public void OnFadeComplete()
    {
        if(levelToLoad != null)
            SceneManager.LoadScene(levelToLoad);
    }
}
