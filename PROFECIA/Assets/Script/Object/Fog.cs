using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    public door openCheck;
    private SpriteRenderer spriteRenderer;
    private byte temp = 150;

    void Start()
    {
        temp = 150;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (openCheck.doorOpen)
        {
            if (temp > 0)
            {
                spriteRenderer.color = new Color32(0, 0, 0, temp);
                temp -= 3;
            }
            else Destroy(gameObject);
        }
    }
}