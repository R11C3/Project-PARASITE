using System;
using UnityEngine;

[Serializable]
public class ItemStatistic
{
    public string statName;
    [Tooltip("Set to -1 to not display")]
    public float statValue;
    public bool visible;

    public ItemStatistic(string name)
    {
        statName = name;
        statValue = 0;
        visible = true;
    }

    public ItemStatistic(string name, float value)
    {
        statName = name;
        statValue = value;
        visible = true;
    }
}
