using UnityEngine;

[CreateAssetMenu(fileName = "SO_Magazine", menuName = "Scriptable Objects/Weapon Mod/Magazine")]
public class SO_Magazine : SO_WeaponMod
{
    public int currentAmmo;

    public SO_Ammo compatibleAmmo;

    void Reset()
    {
        type = Type.Magazine;
        AppendStats();
    }

    public override void AppendStats()
    {
        base.AppendStats();
        itemStats.itemStatsList.Add(new ItemStatistic("Ergonomics", 0));
        itemStats.itemStatsList.Add(new ItemStatistic("Max Ammo", 0));
    }
}
