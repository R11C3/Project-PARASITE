using UnityEngine;

[CreateAssetMenu(fileName = "SO_Backpack", menuName = "Scriptable Objects/Item/Backpack")]
public class SO_Backpack : SO_InventoryItem
{
    public void Reset()
    {
        type = Type.Backpack;
    }
}
