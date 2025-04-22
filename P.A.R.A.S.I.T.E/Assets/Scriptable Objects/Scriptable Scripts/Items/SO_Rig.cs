using UnityEngine;

[CreateAssetMenu(fileName = "SO_Rig", menuName = "Scriptable Objects/Item/Rig")]
public class SO_Rig : SO_InventoryItem
{
    public void Reset()
    {
        type = Type.Rig;
    }

    public override void InitializeInventories()
    {
        inventories = new GridInventory(storageDimensions.width, storageDimensions.height);
    }
}
