using UnityEngine;

[CreateAssetMenu(fileName = "SO_Consumable", menuName = "Scriptable Objects/Item/Consumable")]
public class SO_Consumable : SO_Item
{
    public void Reset()
    {
        type = Type.Consumable;
        itemStats.itemStatsList.Add(new ItemStatistic("Uses Left", 0));
    }

    public float energy;
    public float hydration;
}
