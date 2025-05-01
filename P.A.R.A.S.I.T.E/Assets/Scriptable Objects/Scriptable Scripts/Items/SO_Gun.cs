using System;
using UnityEngine;

public enum GunType{Rifle, Pistol, Shotgun}
public enum FireMode{Automatic, Burst, Single}
public enum ReloadType{Magazine, Single}

[Serializable]
public class Statistics 
{
    public float accuracy;
    public float fireSpeed;
    public float reloadTime;
    public float magnification;
    public FireMode[] fireModes;
    public ReloadType reloadType;
    public SO_Bullet bullet;
}

[Serializable]
public class Attachments
{
    public SO_Magazine magazine;
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

    public void Reset()
    {
        type = Type.Weapon;
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

    public override bool Equals(SO_Item other)
    {
        return base.Equals(other);
    }
}