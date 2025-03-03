using UnityEngine;

[CreateAssetMenu(fileName = "ScriptObj_GunData", menuName = "Scriptable Objects/GunData")]
public class ScriptObj_GunData : ScriptableObject
{
    [SerializeField]
    private float damage;
    [SerializeField]
    private float fireSpeed;
    [SerializeField]
    private bool automatic;
    [SerializeField]
    private ParticleSystem firingParticle;
    [SerializeField]
    private ParticleSystem impactParticle;
    [SerializeField]
    private Vector3 accuracy;
    [SerializeField]
    private bool shotgun;
}
