using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class door : MonoBehaviour
{
    //public GameObject doorPrefab;
    //public Animator animator;
    public bool doorOpen = false;
    private float openedPos;
    private float closedPos;
    [SerializeField] private Transform doorPosition;
    private BoxCollider2D trigger;

    private gameMaster gm;

    AudioSource audioSource;
    AudioSource aud;
    public AudioClip open; // 문열림

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>();
        trigger = gameObject.GetComponent<BoxCollider2D>();

        audioSource = GetComponent<AudioSource>();
        aud = GetComponent<AudioSource>();

        openedPos = doorPosition.position.y * -2.4f;
        closedPos = doorPosition.position.y;
    }

    void Update()
    {
        if (doorOpen)
        {
            Vector3 pos = doorPosition.position;
            doorPosition.position = new Vector3(pos.x, Mathf.Lerp(pos.y, openedPos, Time.deltaTime * 0.8f), 0);
        }
        else
        {
            Vector3 pos = doorPosition.position;
            doorPosition.position = new Vector3(pos.x, Mathf.Lerp(pos.y, closedPos, Time.deltaTime * 2.0f), 0);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            gm.DoorText.text = ("[E] to Open");
        }
        if(doorOpen)
        {
            gm.DoorText.text = ("");
        }
        if (col.CompareTag("BossRoom"))
        {
            doorOpen = false;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                doorOpen = true;
                if (!aud.isPlaying)
                    PlayerSound(open);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        gm.DoorText.text = ("");
    }

    // 효과음 재생 메소드    // 기본 : 1회재생(반복X)
    void PlayerSound(AudioClip clip)
    {
        aud.PlayOneShot(clip);
    }
}
