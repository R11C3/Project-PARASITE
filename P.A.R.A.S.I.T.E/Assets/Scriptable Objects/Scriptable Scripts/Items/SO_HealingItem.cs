using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_HealingItem", menuName = "Scriptable Objects/Item/Medical")]
public class SO_HealingItem : SO_Item
{
    public void Reset()
    {
        type = Type.Medical;
        AppendStats();
    }

    public override void AppendStats()
    {
        base.AppendStats();
        itemStats.itemStatsList.Add(new ItemStatistic("Uses Left", 0));
    }
}
