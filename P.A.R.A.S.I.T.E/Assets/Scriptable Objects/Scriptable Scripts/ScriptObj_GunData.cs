using UnityEngine;

[CreateAssetMenu(fileName = "ScriptObj_GunData", menuName = "Scriptable Objects/GunData")]
public class ScriptObj_GunData : ScriptableObject
{
    [SerializeField]
    public Mesh model;
    [SerializeField]
    public Vector3 offset;
    public Quaternion offsetRotation;
    public Vector3 endOfBarrel;
    public Vector3 leftHandPosition;
    public Quaternion leftHandRotation;
    public Vector3 leftHandHintPosition;
    public Quaternion leftHandHintRotation;

    [SerializeField]
    public float damage;
    [SerializeField]
    public float fireSpeed;
    [SerializeField]
    public int maxAmmo;
    [SerializeField]
    public int reloadTime;
    [SerializeField]
    public bool automatic;
    [SerializeField]
    public GameObject firingParticle;
    [SerializeField]
    public GameObject impactParticle;
    [SerializeField]
    public Vector3 accuracy;
    [SerializeField]
    public bool shotgun;
}