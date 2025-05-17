using UnityEngine;

[CreateAssetMenu(fileName = "SO_Foregrip", menuName = "Scriptable Objects/Weapon Mod/Foregrip")]
public class SO_Foregrip : SO_WeaponMod
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
