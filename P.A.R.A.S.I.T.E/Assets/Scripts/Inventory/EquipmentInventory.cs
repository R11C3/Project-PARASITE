using System;
using System.Collections;
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
    public SO_Rig rig;
    [SerializeField]
    public SO_Backpack backpack;

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
        if((backpack == null && item.type == SO_Item.Type.Backpack) && !success)
        {
            backpack = (SO_Backpack)item;
            source.gridHandler.LoadBackpackInventories();
            success = true;
        }
        if((rig == null && item.type == SO_Item.Type.Rig) && !success)
        {
            rig = (SO_Rig)item;
            source.gridHandler.LoadRigInventories();
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
        bool success = backpack.inventories.Add(item);
        Debug.Log(success);
        return success;
    }

    private bool AddItemToRig(SO_Item item)
    {
        bool success = rig.inventories.Add(item);
        Debug.Log(success);
        return success;
    }
}
