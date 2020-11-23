using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class elevator : MonoBehaviour
{
    public Animator animator;
    private gameMaster gm;
    [SerializeField] private StageCheck check;
    public MyScriptableObject MyScriptableObject;
    public PlayerStat stat;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>();

        //SceneManager.LoadScene("InElevator", LoadSceneMode.Additive);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        /*
        if(check.cardCheck == true)
        {
            if (col.CompareTag("Player"))
            {
                gm.InputText.text = ("[E] to Enter");
                animator.SetBool("Activate", true);
            }
        }*/
        if (col.CompareTag("Player"))
        {
            gm.InputText.text = ("[E] to Enter");
            animator.SetBool("Activate", true);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        /*
        if(check.cardCheck == true)
        {
            if (col.CompareTag("Player"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    check.cardCheck = false;
                    MyScriptableObject.floorNum++;
                    PlayerPrefs.SetInt("CurrentHP", stat.currentHP);
                    PlayerPrefs.SetInt("CurrentEXP", stat.currentExp);
                    PlayerPrefs.SetFloat("SwordEnergy", stat.swordEnergy);
                    PlayerPrefs.SetFloat("GauntletEnergy", stat.gauntletEnergy);
                    if(MyScriptableObject.floorNum % 5 == 0)
                        SceneManager.LoadScene("Boss1", LoadSceneMode.Single);
                    else SceneManager.LoadScene("Sample", LoadSceneMode.Single);
                }
            }
        }*/
        if (col.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                /*
                check.cardCheck = false;
                MyScriptableObject.floorNum++;
                PlayerPrefs.SetInt("CurrentHP", stat.currentHP);
                PlayerPrefs.SetInt("CurrentEXP", stat.currentExp);
                PlayerPrefs.SetFloat("SwordEnergy", stat.swordEnergy);
                PlayerPrefs.SetFloat("GauntletEnergy", stat.gauntletEnergy);
                if (MyScriptableObject.floorNum % 5 == 0)
                    SceneManager.LoadScene("Boss1", LoadSceneMode.Single);
                else SceneManager.LoadScene("Sample", LoadSceneMode.Single);
                */
                MyScriptableObject.floorNum += 1;
                PlayerPrefs.SetInt("CurrentHP", stat.currentHP);
                PlayerPrefs.SetInt("CurrentEXP", stat.currentExp);
                PlayerPrefs.SetFloat("SwordEnergy", stat.swordEnergy);
                PlayerPrefs.SetFloat("GauntletEnergy", stat.gauntletEnergy);
                if (MyScriptableObject.floorNum == 1)
                    SceneManager.LoadScene("Sample");
                else if (MyScriptableObject.floorNum == 3)
                    SceneManager.LoadScene("Boss_Sample3");
                else if (MyScriptableObject.floorNum == 5)
                {
                    //MyScriptableObject.floorNum = 1;
                    //SceneManager.LoadScene("Main");
                    SceneManager.LoadScene("Boss_Sample2");
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        gm.InputText.text = ("");
    }
}
