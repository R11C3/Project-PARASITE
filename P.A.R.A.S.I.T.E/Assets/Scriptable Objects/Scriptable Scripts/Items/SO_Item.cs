using System;
using UnityEditor;
using UnityEngine;

[Serializable]
    public struct Dimensions
    {
        public int height;
        public int width;
    }

[CreateAssetMenu(fileName = "SO_Item", menuName = "Scriptable Objects/Item")]
[Serializable]
public abstract class SO_Item : ScriptableObject
{
    public enum Type{Weapon, Key, Material, Consumable, Medical, Mod, Armor, Helmet, Rig, Backpack}

    [Header("Item Name & Type")]
    public string ID = Guid.NewGuid().ToString();
    public string itemName;
    public Type type;
    [Header("InSceneObject & Sprite")]
    public GameObject obj;
    public Sprite sprite;
    [Header("Size in Inventory")]
    public Dimensions dimensions;

    public virtual bool Equals(SO_Item other)
    {
        return itemName.Equals(other.itemName);
    }
}
