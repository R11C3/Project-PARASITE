using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_InventoryItem", menuName = "Scriptable Objects/SO_InventoryItem")]
public abstract class SO_InventoryItem : SO_Item
{
    public Dimensions[] storageDimensions;

    public GridInventory[] inventories;

    public void InitializeInventories()
    {
        for(int i = 0; i < storageDimensions.Length; i++)
        {
            inventories.Append(new GridInventory(storageDimensions[i].width, storageDimensions[i].height));
        }
    }
}
