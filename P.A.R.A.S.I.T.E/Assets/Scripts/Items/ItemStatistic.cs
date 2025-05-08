using System;
using UnityEngine;

[Serializable]
public class ItemStatistic
{
    public string statName;
    public float statValue;

    public ItemStatistic(string name)
    {
        statName = name;
        statValue = 0;
    }

    public ItemStatistic(string name, float value)
    {
        statName = name;
        statValue = value;
    }
}
