using UnityEngine;

[CreateAssetMenu(fileName = "SO_Magazine", menuName = "Scriptable Objects/Weapon Mod/Magazine")]
public class SO_Magazine : SO_WeaponMod
{
    public int currentAmmo;

    void Reset()
    {
        type = Type.Mod;
        AppendStats();
    }

    public override void AppendStats()
    {
        base.AppendStats();
        itemStats.itemStatsList.Add(new ItemStatistic("Max Ammo", 0));
    }
}
