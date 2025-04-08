using System;
using System.Collections;
using UnityEngine;

public enum WeaponSlot{Primary, Sling, Holster, None}

[Serializable]
public class EquipmentInventory
{
    [SerializeField]
    private SO_Gun primary;
    [SerializeField]
    private SO_Gun sling;
    [SerializeField]
    private SO_Gun holster;

    public WeaponSlot equipped;

    public SO_Gun EquippedGun()
    {
        switch (equipped)
        {
            case WeaponSlot.Primary:
                return primary;
            case WeaponSlot.Sling:
                return sling;
            case WeaponSlot.Holster:
                return holster;
        }
        return null;
    }

    public void SwitchGun(WeaponSlot slot)
    {
        switch (slot)
        {
            case WeaponSlot.Primary:
                equipped = WeaponSlot.Primary;
                break;
            case WeaponSlot.Sling:
                equipped = WeaponSlot.Sling;
                break;
            case WeaponSlot.Holster:
                equipped = WeaponSlot.Holster;
                break;
        }
    }

    public SO_Gun ReplaceGun(WeaponSlot slot, SO_Gun gun)
    {
        SO_Gun returnGun;
        switch (slot)
        {
            case WeaponSlot.Primary:
                returnGun = primary;
                primary = gun;
                return returnGun;
            case WeaponSlot.Sling:
                returnGun = sling;
                sling = gun;
                return returnGun;
            case WeaponSlot.Holster:
                returnGun = holster;
                holster = gun;
                return returnGun;
        }
        return null;
    }

    public SO_Gun GetGun(WeaponSlot slot)
    {
        switch (slot)
        {
            case WeaponSlot.Primary:
                return primary;
            case WeaponSlot.Sling:
                return sling;
            case WeaponSlot.Holster:
                return holster;
        }
        return null;
    }

    public void ExposeInventory()
    {
        
    }
}
