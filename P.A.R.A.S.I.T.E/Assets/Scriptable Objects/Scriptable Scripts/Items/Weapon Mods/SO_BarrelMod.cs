using UnityEngine;

[CreateAssetMenu(fileName = "SO_BarrelMod", menuName = "Scriptable Objects/Weapon Mod/BarrelMod")]
public class SO_BarrelMod : SO_WeaponMod
{
    void Reset()
    {
        type = Type.Mod;

        AppendStats();
    }

    public override void AppendStats()
    {
        base.AppendStats();
        itemStats.itemStatsList.Add(new ItemStatistic("Recoil", 0));
        itemStats.itemStatsList.Add(new ItemStatistic("Accuracy", 0));
        itemStats.itemStatsList.Add(new ItemStatistic("Ergonomics", 0));
    }
}
