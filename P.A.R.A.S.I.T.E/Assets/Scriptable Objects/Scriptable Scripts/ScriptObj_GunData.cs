using UnityEngine;

[CreateAssetMenu(fileName = "ScriptObj_GunData", menuName = "Scriptable Objects/GunData")]
public class ScriptObj_GunData : ScriptableObject
{
    [SerializeField]
    public float damage;
    [SerializeField]
    public float fireSpeed;
    [SerializeField]
    public bool automatic;
    [SerializeField]
    public ParticleSystem firingParticle;
    [SerializeField]
    public ParticleSystem impactParticle;
    [SerializeField]
    public Vector3 accuracy;
    [SerializeField]
    public bool shotgun;
}
