using UnityEngine;

public abstract class GunBlueprint : MonoBehaviour
{
    [SerializeField]
    private SO_Gun gun;
    [SerializeField]
    private GameObject magazine;
    [SerializeField]
    private ParticleSystem shootingSystem;
    [SerializeField]
    private TrailRenderer trail;
    [SerializeField]
    private LayerMask mask;

    public abstract void Shoot();

    public abstract void Reload();
}
