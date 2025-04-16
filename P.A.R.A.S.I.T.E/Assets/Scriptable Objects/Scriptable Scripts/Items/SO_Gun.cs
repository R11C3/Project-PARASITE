using System;
using UnityEngine;

public enum GunType{Rifle, Pistol, Shotgun}
public enum FireMode{Automatic, Burst, Single}

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
    public FireMode[] fireModes;
    public float damage;
    public float fireSpeed;
    public int maxAmmo;
    public int reloadTime;
    public bool automatic;
    public GameObject firingParticle;
    public GameObject impactParticle;
    public Vector3 accuracy;

    [Header("Shotgun Statistics")]
    public int pelletCount;

    [Header("Instance Statistics")]
    public int currentAmmo;
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
        currentFireMode = fireModes[fireModeIndex % fireModes.Length];
        if(fireModeIndex >= fireModes.Length)
        {
            fireModeIndex = 0;
        }
    }

    public override bool Equals(SO_Item other)
    {
        return base.Equals(other);
    }
}