using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Item", menuName = "Scriptable Objects/Item")]
public class SO_Item : ScriptableObject
{
    public String itemName;
    public GameObject Fab;
    public Sprite sprite;
}
