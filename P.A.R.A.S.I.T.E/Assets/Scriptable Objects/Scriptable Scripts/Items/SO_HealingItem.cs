using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_HealingItem", menuName = "Scriptable Objects/Item/Medical")]
public class SO_HealingItem : SO_Item
{
    public void Reset()
    {
        type = Type.Medical;
        hasDurability = true;
        maxDurability = 1;
        AppendStats();
    }

    public void UpdateStats()
    {
        itemStats.UpdateByName("Uses Left", currentDurability);
    }

    public override void AppendStats()
    {
        base.AppendStats();
    }
}
