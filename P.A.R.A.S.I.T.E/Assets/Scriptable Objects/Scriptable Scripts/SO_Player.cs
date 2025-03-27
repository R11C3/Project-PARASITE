using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Player", menuName = "Scriptable Objects/Player")]
public class SO_Player : SO_Mob
{
    [Header("Weapon Inventory")]
    public SO_Gun[] weaponInventory;

    public void ReplaceGun(int index, SO_Gun newGun)
    {
        weaponInventory[index] = Instantiate(newGun);
    }

    public override void DoDamage(float damage)
    {
        health -= damage;
    }

    public override void Reload()
    {
        throw new System.NotImplementedException();
    }

    public override void Shoot()
    {
        throw new System.NotImplementedException();
    }
}
