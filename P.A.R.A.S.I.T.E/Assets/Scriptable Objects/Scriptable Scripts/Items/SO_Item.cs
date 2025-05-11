using System;
using System.Collections.Generic;
using System.Linq;
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
    public enum Type{Pistol, Weapon, Key, Material, Consumable, Medical, Mod, Armor, Helmet, Rig, Backpack, Deployable, Magazine, Ammo}

    [Header("Item Name & Type")]
    public string ID = Guid.NewGuid().ToString();
    public string itemName;
    public Type type;
    [Header("Description")]
    [TextArea]
    public string description;
    [Header("InSceneObject & Sprite")]
    public GameObject obj;
    public Texture2D sprite;
    [Header("Size in Inventory")]
    public Dimensions dimensions;
    [Header("Location in inventory")]
    public Dimensions location;
    [Header("Statistics")]
    public ItemStatisticList itemStats = new();

    public virtual bool Equals(SO_Item other)
    {
        return itemName.Equals(other.itemName);
    }

    public virtual void AppendStats()
    {
        itemStats.itemStatsList.Add(new ItemStatistic("Weight", 0));
    }

    void Reset()
    {
        AppendStats();
    }
}
