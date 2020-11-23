using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    [SerializeField]private ObjectMove objectMove;

    // Start is called before the first frame update
    void Start()
    {
        //objectMove = GameObject.FindGameObjectWithTag("Enemy").GetComponent<ObjectMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if(objectMove.find == true)
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            objectMove.find = true;
        }
    }
}
