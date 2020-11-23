using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateFog : MonoBehaviour
{
    public Transform fogPoint;
    public GameObject fogPrefab;
    Fog temp;
    [SerializeField] door dr;

    void Start()
    {
        temp = fogPrefab.GetComponent<Fog>();
        temp.openCheck = dr;
        Instantiate(fogPrefab, fogPoint.position, fogPoint.rotation);
    }
}
