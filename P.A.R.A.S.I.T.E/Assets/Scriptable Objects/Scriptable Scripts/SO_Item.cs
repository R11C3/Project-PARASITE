using System;
using UnityEditor;
using UnityEngine;

public enum Type{Weapon, Key, Material}

[CreateAssetMenu(fileName = "SO_Item", menuName = "Scriptable Objects/Item")]
[Serializable]
public class SO_Item : ScriptableObject
{
    public String itemName;
    public GameObject Fab;
    public Sprite sprite;
    public Type type;
}
