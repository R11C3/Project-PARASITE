using UnityEngine;

[CreateAssetMenu(fileName = "SO_Stock", menuName = "Scriptable Objects/Weapon Mod/Stock")]
public class SO_Stock : SO_WeaponMod
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
        itemStats.itemStatsList.Add(new ItemStatistic("Ergonomics", 0));
    }
}
