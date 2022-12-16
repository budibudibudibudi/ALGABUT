using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pintuscript : MonoBehaviour
{
    public GameObject[] pintu;
    public bool isopen = false;
    // Start is called before the first frame update
    void Start()
    {
        pintu = new GameObject[2];
        for (int i = 0; i < pintu.Length; i++)
        {
            pintu[i] = this.transform.GetChild(i).gameObject;
        }
    }

    private void Update()
    {
        if(isopen)
        {
            pintu[1].SetActive(true);
            pintu[0].SetActive(false);
        }
        else
        {
            pintu[0].SetActive(true);
            pintu[1].SetActive(false);

        }
    }
}
