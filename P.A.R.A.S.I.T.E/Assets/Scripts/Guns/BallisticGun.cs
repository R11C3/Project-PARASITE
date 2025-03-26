using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class BallisticGun : MonoBehaviour
{

    [SerializeField]
    public ScriptObj_GunData gunData;

    private float damage;
    private float firingSpeed;
    private Vector3 accuracy;
    public int maxAmmo;
    public int currentAmmo;
    private float reloadTime;

    [SerializeField]
    private GameObject barrel;
    [SerializeField]
    private GameObject leftHand;
    [SerializeField]
    private GameObject leftHandHint;
    private Vector3 endOfBarrel;
    [SerializeField]
    private PlayerAim playerAim;
    [SerializeField]
    private TrailRenderer trail;
    [SerializeField]
    private ParticleSystem shootingSystem;
    [SerializeField]
    private ParticleSystem impactSystem;
    [SerializeField]
    private LayerMask mask;
    [SerializeField]
    private Transform characterTransform;
    [SerializeField]
    private float speed = 100.0f;
    private float lastShootTime;
    [SerializeField]
    private bool canShoot = true;
    private bool reloading = false;

    public void LoadStats()
    {
        GetComponent<MeshFilter>().mesh = gunData.model;
        transform.localPosition = gunData.offset;
        transform.localRotation = gunData.offsetRotation;
        barrel.GetComponent<Transform>().localPosition = gunData.endOfBarrel;
        leftHand.GetComponent<Transform>().localPosition = gunData.leftHandPosition;
        leftHand.GetComponent<Transform>().localRotation = gunData.leftHandRotation;
        leftHandHint.GetComponent<Transform>().localPosition = gunData.leftHandHintPosition;
        leftHandHint.GetComponent<Transform>().localRotation = gunData.leftHandRotation;
    }
    public void UpdateStats()
    {
        damage = gunData.damage;
        firingSpeed = gunData.fireSpeed;
        accuracy = gunData.accuracy;
        maxAmmo = gunData.maxAmmo;
        reloadTime = gunData.reloadTime;
        endOfBarrel = barrel.transform.position;
    }

    public void Reload()
    {
        reloading = true;
        StartCoroutine(ReloadDelay());
    }

    private IEnumerator ReloadDelay()
    {
        yield return new WaitForSecondsRealtime(reloadTime);
        currentAmmo = maxAmmo;
        reloading = false;
    }

    public void Shoot()
    {
        shootingSystem.transform.position = endOfBarrel;
        if(lastShootTime + firingSpeed < Time.time)
        {
            canShoot = true;
        }
        if(canShoot && currentAmmo > 0 && !reloading)
        {
            shootingSystem.Play();
            canShoot = false;

            Vector3 direction = AccuracyVariation();

            TrailRenderer tempTrail = Instantiate(trail, endOfBarrel, Quaternion.identity);

            if (Physics.Raycast(endOfBarrel, direction, out RaycastHit hit, float.MaxValue, mask))
            {
                StartCoroutine(SpawnTrail(tempTrail, hit.point, true));

                Enemy target;
                hit.transform.gameObject.TryGetComponent<Enemy>(out target);
                target.DoDamage(gunData.damage);
            }
            else
            {
                StartCoroutine(SpawnTrail(tempTrail, direction * 100, false));
            }
            lastShootTime = Time.time;
            currentAmmo--;
        }
    }

    private Vector3 AccuracyVariation()
    {
        Vector3 direction = playerAim.GetMousePosition();
        Vector3 target = direction - characterTransform.position;

        target += new Vector3(
        Random.Range(-accuracy.x, accuracy.x),0,
        Random.Range(-accuracy.z, accuracy.z));

        target.Normalize();

        return target;
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 hit, bool impact)
    {
        float distance = Vector3.Distance(trail.transform.position, hit);
        float startingDistance = distance;
        Vector3 startPosition = trail.transform.position;

        while (distance > 0)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hit, 1 - (distance / startingDistance));
            distance -= Time.deltaTime * speed;

            yield return null;
        }

        trail.transform.position = hit;

        if (impact)
        {
            // Instantiate(impactSystem, hit, Quaternion.LookRotation(hit.normalized));
        }

        Destroy(trail.gameObject, trail.time);
    }
}
