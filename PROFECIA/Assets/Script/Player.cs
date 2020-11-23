using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Stat health;

    [SerializeField]
    private LevelStat exp;
    
    private void Awake()
    {
        health.Initialize();
        exp.Initialize();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            health.CurrentVal += 10;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            exp.CurrentVal += 10;
            exp.HandleLevel();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            health.CurrentVal -= 10;
        }
    }
}
