using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class vcam : MonoBehaviour
{
    CinemachineVirtualCamera Cvcam;
    CinemachineConfiner2D Cconfiner2D;
    public Transform player;
    Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        Cvcam = GetComponent<CinemachineVirtualCamera>();
        Cconfiner2D = GetComponent<CinemachineConfiner2D>();
        col = GameObject.Find("Grid").transform.Find("cameraconfiner").GetComponent<Collider2D>();
        Cconfiner2D.m_BoundingShape2D = col;
        Cvcam.Follow = player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
