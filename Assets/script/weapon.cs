using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public GameObject peluru;
    private void Start()
    {
    }
    public void shoot()
    {
        Instantiate(peluru, FindObjectOfType<playerscript>().shootpoint.position, FindObjectOfType<playerscript>().shootpoint.rotation);
        
    }
}
