using UnityEngine;

[CreateAssetMenu(fileName = "SO_Consumable", menuName = "Scriptable Objects/Item/Consumable")]
public class SO_Consumable : SO_Item
{
    public void Reset()
    {
        type = Type.Consumable;
        hasDurability = true;
        maxDurability = 1;
        AppendStats();
    }

    public override void AppendStats()
    {
        base.AppendStats();
        itemStats.itemStatsList.Add(new ItemStatistic("Energy", 0));
        itemStats.itemStatsList.Add(new ItemStatistic("Hydration", 0));
    }

    public float energy;
    public float hydration;
}
