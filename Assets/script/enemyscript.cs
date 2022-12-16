using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyscript : MonoBehaviour
{
    GameObject player;
    public GameObject explosion;
    public bool sus;
    public GameObject healthpotion;
    // Start is called before the first frame update
    void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position,5*Time.deltaTime);
    }
    private void OnDestroy()
    {
        if (sus)
            Instantiate(healthpotion, transform.position, Quaternion.identity);
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "weakness")
        {
            Debug.Log("weakness");
        }
    }
}
