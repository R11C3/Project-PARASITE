using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_InventoryItem", menuName = "Scriptable Objects/SO_InventoryItem")]
[Serializable]
public abstract class SO_InventoryItem : SO_Item
{
    [SerializeField]
    public Dimensions storageDimensions;
    [SerializeField]
    public GridInventory inventories;

    public abstract void InitializeInventories();
}
