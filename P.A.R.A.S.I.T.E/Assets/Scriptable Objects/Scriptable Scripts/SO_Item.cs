using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Item", menuName = "Scriptable Objects/Item")]
public class SO_Item : ScriptableObject
{
    public String _name;
    public GameObject mesh;
    public Sprite sprite;
}
