using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpscript : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(healthpotionanimate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator healthpotionanimate()
    {
        yield return new WaitForSeconds(4);
        anim.SetTrigger("play");
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
