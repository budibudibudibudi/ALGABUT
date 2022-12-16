using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="itembackpack",menuName ="item")]
public class itemclass : ScriptableObject
{
    public string nama;
    public bool isstackable;
    public GameObject item;
}
