using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCard : MonoBehaviour
{
    public bool isDroped;
    private StageCheck stage;

    Rigidbody2D rb2d;
    BoxCollider2D triger;
    Vector2 DropPow;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DropItemTime());

        Physics2D.IgnoreLayerCollision(11, 14, true);
        Physics2D.IgnoreLayerCollision(14, 14, true);

        int updown = Random.Range(1, 3);
        int leftright = Random.Range(-2, 3);

        rb2d = gameObject.GetComponent<Rigidbody2D>();
        triger = gameObject.GetComponent<BoxCollider2D>();

        DropPow = new Vector2(leftright, updown);
        this.rb2d.AddForce(DropPow, ForceMode2D.Impulse);

        stage = GameObject.FindWithTag("Player").GetComponent<StageCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DropItemTime()
    {
        yield return new WaitForSeconds(0.5f);
        this.triger.enabled = true;

        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            stage.cardCheck = true;
        }
    }
}
