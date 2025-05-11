using System;
using UnityEngine;

[Serializable]
public class ItemStatistic
{
    public string statName;
    [Tooltip("Set to -1 to not display")]
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
