using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class tempscript : MonoBehaviour
{
    public GameObject circle;
    private void Start()
    {
        Debug.Log(this.transform.parent.gameObject.name);
    }
}
