using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {

    public MyScriptableObject my;
    public PlayerBasicStat basicStat;
    public StatPointData stat;
    public LevelChanger level;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    //게임 시작 버튼용 함수
    public void buttonToStart()
    {
        PlayerPrefs.SetInt("CurrentHP", basicStat.maxHP + (stat.HP * 10));
        PlayerPrefs.SetFloat("SwordEnergy", basicStat.maxEnergy + (stat.energy * 10));
        PlayerPrefs.SetFloat("GauntletEnergy", basicStat.maxEnergy + (stat.energy * 10));
        //버튼을 클릭할 시, "Sample"이란 이름의 씬을 Load(필요시 이름 변경)
        //오류가 날 경우 이름을 올바르게 입력했는지 확인하고,
        //Unity메뉴에 File > Build Setting 에 입력한 씬이 Build 목록에 있는지 확인
        my.isShuffle = false;
        level.FadeToLevel("Sample");
        //SceneManager.LoadScene("Sample", LoadSceneMode.Single);
    }

    public void ButtonClickSound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
