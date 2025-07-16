using System;
using System.Collections;
using System.Linq;
using Mono.Cecil.Cil;
using UnityEngine;

public enum WeaponSlot{Primary, Sling, Holster, None}

[Serializable]
public class EquipmentInventory
{
    [Header("Weapons")]
    [SerializeField]
    public SO_Gun primary;
    [SerializeField]
    public SO_Gun sling;
    [SerializeField]
    public SO_Gun holster;
    public WeaponSlot equipped;
    
    [Header("Equipment")]
    [SerializeField]
    public Rig rig;
    [SerializeField]
    public Backpack backpack;

    [Header("Armors")]
    [SerializeField]
    private SO_Item armor;

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

    public void CheckNone()
    {
        switch (equipped)
        {
            case WeaponSlot.Primary:
                if(primary == null)
                    equipped = WeaponSlot.None;
                else
                    equipped = WeaponSlot.Primary;
                return;
            case WeaponSlot.Sling:
                if(sling == null)
                    equipped = WeaponSlot.None;
                else
                    equipped = WeaponSlot.Sling;
                return;
            case WeaponSlot.Holster:
                if(holster == null)
                    equipped = WeaponSlot.None;
                else
                    equipped = WeaponSlot.Holster;
                return;
        }
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

    public bool AddItem(PlayerStats source, SO_Item item)
    {
        bool success = false;
        
        if(primary == null && item.type == SO_Item.Type.Weapon && !success)
        {
            primary = (SO_Gun)item;
            success = true;
        }
        if(sling == null && item.type == SO_Item.Type.Weapon && !success)
        {
            sling = (SO_Gun)item;
            success = true;
        }
        if(holster == null && item.type == SO_Item.Type.Pistol && !success)
        {
            holster = (SO_Gun)item;
            success = true;
        }
        if(rig != null && !success)
        {
            success = AddItemToRig(item);
        }
        if(backpack != null && !success)
        {
            success = AddItemToBackpack(item);
        }
        return success;
    }

    private bool AddItemToBackpack(SO_Item item)
    {
        bool success = backpack.inventory.Add(item);
        Debug.Log(success);
        return success;
    }

    private bool AddItemToRig(SO_Item item)
    {
        bool success = rig.inventory.Add(item);
        Debug.Log(success);
        return success;
    }

    public SO_Magazine GetMagWithMostAmmo(SO_Gun gun)
    {
        SO_Magazine mag = GameObject.Instantiate(gun.attachments.compatibleMagazines[0]);
        mag.currentAmmo = 0;

        bool success = false;

        foreach(SO_Magazine magazine in rig.inventory.itemList)
        {
            if(magazine.currentAmmo > mag.currentAmmo)
            {
                bool flag = false;
                foreach(SO_Magazine compatibleMag in gun.attachments.compatibleMagazines)
                {
                    if(compatibleMag.Equals(magazine))
                    {
                        flag = true;
                    }
                }
                if(flag)
                {
                    mag = magazine;
                    success = true;
                }
            }
        }

        if(success)
        {
            return mag;
        }

        return null;
    }
}
