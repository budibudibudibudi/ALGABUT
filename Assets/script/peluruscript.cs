using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peluruscript : MonoBehaviour
{
    Rigidbody2D rb;
    private void Start()
    {
        Destroy(gameObject, 5);
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        rb.velocity = transform.right * 20;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            Destroy(collision.gameObject);
            gamemanage.score++;
            Destroy(gameObject);
            FindObjectOfType<spawnscript>().minspawn--;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "peluru" || collision.gameObject.layer == 6)
            Destroy(gameObject);
    }
}
