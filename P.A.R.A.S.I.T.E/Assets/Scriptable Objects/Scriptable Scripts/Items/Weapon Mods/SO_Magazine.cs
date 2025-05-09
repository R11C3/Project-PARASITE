using UnityEngine;

[CreateAssetMenu(fileName = "SO_Magazine", menuName = "Scriptable Objects/Weapon Mod/Magazine")]
public class SO_Magazine : SO_WeaponMod
{
    public int maxAmmo;
    public int currentAmmo;

    void Reset()
    {
        type = Type.Mod;
    }
}
