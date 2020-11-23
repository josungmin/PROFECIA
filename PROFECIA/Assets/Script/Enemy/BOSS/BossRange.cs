using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRange : MonoBehaviour
{
    [SerializeField] private BossMove bossMove;

    // Start is called before the first frame update
    void Start()
    {
        //objectMove = GameObject.FindGameObjectWithTag("Enemy").GetComponent<ObjectMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossMove.find == true)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bossMove.find = true;
        }
    }
}
