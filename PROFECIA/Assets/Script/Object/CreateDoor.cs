using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDoor : MonoBehaviour
{
    public Transform doorPoint;
    public GameObject doorPrefab;

    // Update is called once per frame
    void Start()
    {
        Instantiate(doorPrefab, doorPoint.position, doorPoint.rotation);
    }
}
