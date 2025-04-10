using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_InventoryItem", menuName = "Scriptable Objects/SO_InventoryItem")]
[Serializable]
public abstract class SO_InventoryItem : SO_Item
{
    [SerializeField]
    public Dimensions[] storageDimensions;
    [SerializeField]
    public GridInventory[] inventories;

    public void InitializeInventories()
    {
        for(int i = 0; i < storageDimensions.Length; i++)
        {
            inventories.Append(new GridInventory(storageDimensions[i].width, storageDimensions[i].height));
        }
    }
}
