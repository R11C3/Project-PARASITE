using UnityEngine;

[CreateAssetMenu(fileName = "ScriptObj_GunData", menuName = "Scriptable Objects/GunData")]
public class ScriptObj_GunData : ScriptableObject
{
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
    public float damage;
    public float fireSpeed;
    public int maxAmmo;
    public int reloadTime;
    public bool automatic;
    public GameObject firingParticle;
    public GameObject impactParticle;
    public Vector3 accuracy;
    public bool shotgun;

    [Header("Instance Data (DO NOT TOUCH)")]
    public int currentAmmo;
}