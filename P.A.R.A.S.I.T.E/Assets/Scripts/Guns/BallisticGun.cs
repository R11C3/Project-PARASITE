using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class BallisticGun : MonoBehaviour
{

    [SerializeField]
    public SO_Gun gunData;

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
    private Camera mainCamera;
    [SerializeField]
    private float speed = 100.0f;
    private float lastShootTime;
    [SerializeField]
    private bool canShoot = true;
    public bool reloading = false;
    public bool switching = false;
    public bool fireHeld = false;
    public bool aiming = false;

    void Awake()
    {
        mainCamera = Camera.main;
    }

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
        gunData.currentFireMode = gunData.fireModes[gunData.fireModeIndex];
    }

    public void Reload()
    {
        if(gunData.reloadType == ReloadType.Magazine && !reloading)
        {
            reloading = true;
            StartCoroutine(ReloadDelay());
        }
        else if(gunData.reloadType == ReloadType.Single && !reloading)
        {
            reloading = true;
            StartCoroutine(SingleReloadDelay());
        }
    }

    private IEnumerator ReloadDelay()
    {
        yield return new WaitForSecondsRealtime(gunData.reloadTime);
        gunData.currentAmmo = gunData.maxAmmo;
        reloading = false;
    }

    private IEnumerator SingleReloadDelay()
    {
        for(int i = gunData.currentAmmo; i < gunData.maxAmmo; i++)
        {
            yield return new WaitForSecondsRealtime(gunData.reloadTime);
            gunData.currentAmmo++;
        }
        reloading = false;
    }

    public void Shoot()
    {
        if(gunData.currentFireMode == FireMode.Automatic)
        {
            ShootingSystem();
        }
        else if(gunData.currentFireMode == FireMode.Single && !fireHeld)
        {
            ShootingSystem();
        }
    }

    public void ShootingSystem()
    {
        if(gunData.gunType == GunType.Shotgun)
        {
            MultiBulletSystem();
        }
        else
        {
            SingleBulletSystem();
        }
    }

    public void SingleBulletSystem()
    {
        shootingSystem.transform.position = barrel.GetComponent<Transform>().position;
        if(lastShootTime + gunData.fireSpeed < Time.time)
        {
            canShoot = true;
        }
        if(canShoot && gunData.currentAmmo > 0 && !reloading && !switching)
        {
            shootingSystem.Play();
            mainCamera.GetComponent<CameraShake>().Shake();
            canShoot = false;

            Vector3 direction = AccuracyVariation();

            TrailRenderer tempTrail = Instantiate(trail, barrel.GetComponent<Transform>().position, Quaternion.identity);

            if (Physics.Raycast(barrel.GetComponent<Transform>().position, direction, out RaycastHit hit, float.MaxValue, mask))
            {
                StartCoroutine(SpawnTrail(tempTrail, hit.point, true));

                Enemy target;
                hit.transform.gameObject.TryGetComponent<Enemy>(out target);
                if(target != null) target.DoDamage(gunData.damage);
            }
            else
            {
                StartCoroutine(SpawnTrail(tempTrail, direction * 100, false));
            }
            lastShootTime = Time.time;
            gunData.currentAmmo--;
        }
    }

    public void MultiBulletSystem()
    {
        shootingSystem.transform.position = barrel.GetComponent<Transform>().position;
        if(lastShootTime + gunData.fireSpeed < Time.time)
        {
            canShoot = true;
        }
        if(canShoot && gunData.currentAmmo > 0 && !reloading && !switching)
        {
            shootingSystem.Play();
            mainCamera.GetComponent<CameraShake>().Shake();
            canShoot = false;

            for(int i = 0; i < gunData.pelletCount; i++)
            {
                Vector3 direction = AccuracyVariation();

                TrailRenderer tempTrail = Instantiate(trail, barrel.GetComponent<Transform>().position, Quaternion.identity);

                if (Physics.Raycast(barrel.GetComponent<Transform>().position, direction, out RaycastHit hit, float.MaxValue, mask))
                {
                    StartCoroutine(SpawnTrail(tempTrail, hit.point, true));

                    Enemy target;
                    hit.transform.gameObject.TryGetComponent<Enemy>(out target);
                    if(target != null) target.DoDamage(gunData.damage);
                }
                else
                {
                    StartCoroutine(SpawnTrail(tempTrail, direction * 100, false));
                }
                
            }
            lastShootTime = Time.time;
            gunData.currentAmmo--;
        }
    }

    private Vector3 AccuracyVariation()
    {
        Vector3 direction = playerAim.GetMousePosition();
        Vector3 target = direction - characterTransform.position;

        if(aiming)
        {
            target += new Vector3(
            Random.Range(-gunData.accuracy.x * 0.5f, gunData.accuracy.x * 0.5f), -1.5f,
            Random.Range(-gunData.accuracy.z * 0.5f, gunData.accuracy.z * 0.5f));
        }
        else
        {
            target += new Vector3(
            Random.Range(-gunData.accuracy.x, gunData.accuracy.x), -1.5f,
            Random.Range(-gunData.accuracy.z, gunData.accuracy.z));
        }

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
