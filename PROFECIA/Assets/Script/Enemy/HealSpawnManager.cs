using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSpawnManager : MonoBehaviour
{
    public Transform[] points;
    public GameObject HealPack;

    // Start is called before the first frame update
    void Start()
    {
        points = GameObject.Find("HealSpawnPoint").GetComponentsInChildren<Transform>();
        StartCoroutine(this.CreateHeal());
    }

    IEnumerator CreateHeal()
    {
        if(true) //짝수층 일때
        {
            int idx = Random.Range(1, points.Length);

            Instantiate(HealPack, points[idx].position, Quaternion.identity);

            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
