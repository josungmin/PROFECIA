using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManger : MonoBehaviour
{
    //points는 배열로 담을 수 있도록 한다.
    public Transform[] points;
    public GameObject monster1;
    public GameObject monster2;
    public GameObject monster3;
    public GameObject monster4;
    public GameObject monster5;


    // Use this for initialization
    void Start()
    {
        // points를 게임시작과 함께 배열에 담기
        points = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();
        StartCoroutine(this.CreateMonster());
    }

    IEnumerator CreateMonster()
    {
        for(int len = 1; len < points.Length; len++)
        {
            int idx = Random.Range(1, 6);

            int count1 = 0;
            int count2 = 0;
            int count3 = 0;
            int count4 = 0;
            int count5 = 0;

            if (idx == 1 && count1 < 2)
            {
                Instantiate(monster1, points[len].position, Quaternion.identity);
                count1++;
            }

            if (idx == 2 && count2 < 2)
            {
                Instantiate(monster2, points[len].position, Quaternion.identity);
                count2++;
            }

            if (idx == 3 && count3 < 2)
            {
                Instantiate(monster3, points[len].position, Quaternion.identity);
                count3++;
            }

            if (idx == 4 && count4 < 2)
            {
                Instantiate(monster4, points[len].position, Quaternion.identity);
                count4++;
            }

            if (idx == 5 && count5 < 2)
            {
                Instantiate(monster5, points[len].position, Quaternion.identity);
                count5++;
            }


            //Instantiate(monster, points[len].position, Quaternion.identity);
        }

        yield return null;

        // 계속해서 createTime동안 monster생성
        /*
        while (true)
        {
            int idx = Random.Range(1, points.Length);

            previousIdx = idx;
            Instantiate(monster, points[idx].position, Quaternion.identity);

            yield return new WaitForSeconds(createTime);
        }
        */
    }


    // Update is called once per frame
    void Update()
    {

    }
}
