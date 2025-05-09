using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ItemStatisticList
{
    [SerializeField]
    public List<ItemStatistic> itemStatsList = new List<ItemStatistic>();

    //Individual BASE stats
    ItemStatistic weight = new ItemStatistic("Weight", 0);

    public ItemStatisticList()
    {
        itemStatsList.Add(weight);
    }

    public ItemStatistic GetByName(string name)
    {
        foreach (ItemStatistic stat in itemStatsList)
        {
            if(name.Equals(stat.statName))
                return stat;
        }

        return null;
    }

    public bool UpdateByName(string name, int amount)
    {
        foreach (ItemStatistic stat in itemStatsList)
        {
            if(name.Equals(stat.statName))
            {
                stat.statValue += amount;
                return true;
            }
        }

        return false;
    }
}
