using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Optic", menuName = "Scriptable Objects/Weapon Mod/Optic")]
public class SO_Optic : SO_WeaponMod
{
    void Reset()
    {
        type = Type.Mod;
        itemStats.itemStatsList.Add(new ItemStatistic("Magnification", 0));
        itemStats.itemStatsList.Add(new ItemStatistic("Accuracy", 0.0f));
    }
}
