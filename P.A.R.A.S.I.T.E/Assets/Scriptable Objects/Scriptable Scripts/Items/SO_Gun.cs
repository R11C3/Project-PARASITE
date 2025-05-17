using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum GunType{Rifle, Pistol, Shotgun}
public enum FireMode{Automatic, Burst, Single}
public enum ReloadType{Magazine, Single}

[Serializable]
public struct Statistics 
{
    public float baseAccuracy;
    [HideInInspector]
    public float accuracy;
    public float baseRecoil;
    [HideInInspector]
    public float recoil;
    public float baseErgonomics;
    [HideInInspector]
    public float ergonomics;
    public float ADSaccuracy;
    public float fireSpeed;
    public float reloadTime;
    public float magnification;
    public FireMode[] fireModes;
    public ReloadType reloadType;
    public SO_Bullet bullet;
}

[Serializable]
public struct Attachments
{
    public SO_Magazine magazine;
    public SO_Magazine[] compatibleMagazines;
    public SO_Optic optic;
    public SO_Optic[] compatibleScopes;
    public SO_Foregrip foregrip;
    public SO_Foregrip[] compatibleForegrips;
    public SO_Stock stock;
    public SO_Stock[] compatibleStocks;
    public SO_BarrelMod barrelMod;
    public SO_BarrelMod[] compatibleBarrelMods;
}

[CreateAssetMenu(fileName = "SO_Gun", menuName = "Scriptable Objects/Item/Gun")]
public class SO_Gun : SO_Item
{
    public GunType gunType;
    [Header("Mesh for Gun")]
    public Mesh model;
    [Header("Offset and IK positions")]
    public Vector3 offset;
    public Quaternion offsetRotation;
    public Vector3 endOfBarrel;
    public Vector3 leftHandPosition;
    public Quaternion leftHandRotation;
    public Vector3 leftHandHintPosition;
    public Quaternion leftHandHintRotation;

    [Header("Gun Statistics")]
    public Statistics stats;
    public Attachments attachments;
    public GameObject firingParticle;
    public GameObject impactParticle;

    [Header("Shotgun Statistics")]
    public int pelletCount;

    [Header("Instance Statistics")]
    public FireMode currentFireMode;
    [HideInInspector]
    public int fireModeIndex = 0;

    void Reset()
    {
        type = Type.Weapon;
        AppendStats();
    }

    public override void AppendStats()
    {
        base.AppendStats();
        itemStats.itemStatsList.Add(new ItemStatistic("Magnification", 0));
        itemStats.itemStatsList.Add(new ItemStatistic("Accuracy", 0.0f));
    }

    public void ChangeFireMode()
    {
        fireModeIndex++;
        currentFireMode = stats.fireModes[fireModeIndex % stats.fireModes.Length];
        if(fireModeIndex >= stats.fireModes.Length)
        {
            fireModeIndex = 0;
        }
    }

    public void CalculateWeaponStats()
    {
        float opticAccuracy, stockAccuracy, foregripAccuracy, barrelModAccuracy;
        float opticErgo, stockErgo, foregripErgo, magazineErgo, barrelModErgo;
        float stockRecoil, foregripRecoil, barrelModRecoil;

        if (attachments.optic == null)
        {
            opticAccuracy = 0;
            opticErgo = 0;
        }
        else
        {
            opticAccuracy = attachments.optic.itemStats.GetByName("Accuracy").statValue;
            opticErgo = attachments.optic.itemStats.GetByName("Ergonomics").statValue;
        }

        if (attachments.stock == null)
        {
            stockAccuracy = 0;
            stockErgo = 0;
            stockRecoil = 0;
        }
        else
        {
            stockAccuracy = attachments.stock.itemStats.GetByName("Accuracy").statValue;
            stockErgo = attachments.stock.itemStats.GetByName("Ergonomics").statValue;
            stockRecoil = attachments.stock.itemStats.GetByName("Recoil").statValue;
        }

        if (attachments.foregrip == null)
        {
            foregripAccuracy = 0;
            foregripErgo = 0;
            foregripRecoil = 0;
        }
        else
        {
            foregripAccuracy = attachments.foregrip.itemStats.GetByName("Accuracy").statValue;
            foregripErgo = attachments.foregrip.itemStats.GetByName("Ergonomics").statValue;
            foregripRecoil = attachments.foregrip.itemStats.GetByName("Recoil").statValue;
        }

        if (attachments.barrelMod == null)
        {
            barrelModAccuracy = 0;
            barrelModErgo = 0;
            barrelModRecoil = 0;
        }
        else
        {
            barrelModAccuracy = attachments.barrelMod.itemStats.GetByName("Accuracy").statValue;
            barrelModErgo = attachments.barrelMod.itemStats.GetByName("Ergonomics").statValue;
            barrelModRecoil = attachments.barrelMod.itemStats.GetByName("Recoil").statValue;
        }

        if (attachments.magazine == null)
        {
            magazineErgo = 0;
        }
        else
        {
            magazineErgo = attachments.magazine.itemStats.GetByName("Ergonomics").statValue;
        }

        stats.accuracy = stats.baseAccuracy - opticAccuracy - stockAccuracy - foregripAccuracy - barrelModAccuracy;
        stats.ergonomics = stats.baseErgonomics + opticErgo + stockErgo + foregripErgo + barrelModErgo + magazineErgo;
        stats.recoil = stats.baseRecoil + stockRecoil + foregripRecoil + barrelModRecoil;
    }

    public override bool Equals(SO_Item other)
    {
        return base.Equals(other);
    }
}