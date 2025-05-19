using System;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;

public class BallisticGun : MonoBehaviour
{

    [SerializeField]
    public SO_Gun gun;

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
    private PlayerStats stats;
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
    private GameObject projectile;
    private Camera mainCamera;
    private float lastShootTime;
    [SerializeField]
    private bool canShoot = true;
    public bool switching = false;
    public bool fireHeld = false;
    public bool aiming = false;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    public void LoadStats()
    {
        gun.CalculateWeaponStats();
        GetComponent<MeshFilter>().mesh = gun.model;
        transform.localPosition = gun.offset;
        transform.localRotation = gun.offsetRotation;
        barrel.GetComponent<Transform>().localPosition = gun.endOfBarrel;
        leftHand.GetComponent<Transform>().localPosition = gun.leftHandPosition;
        leftHand.GetComponent<Transform>().localRotation = gun.leftHandRotation;
        leftHandHint.GetComponent<Transform>().localPosition = gun.leftHandHintPosition;
        leftHandHint.GetComponent<Transform>().localRotation = gun.leftHandRotation;
        gun.currentFireMode = gun.stats.fireModes[gun.fireModeIndex];
    }

    public void Shoot()
    {
        if (gun.attachments.magazine != null)
        {
            if (gun.attachments.magazine.currentAmmo > 0)
            {
                if (gun.currentFireMode == FireMode.Automatic)
                {
                    ShootingSystem();
                }
                else if (gun.currentFireMode == FireMode.Single && !fireHeld)
                {
                    ShootingSystem();
                }
            }
        }
    }

    public void ShootingSystem()
    {
        if (gun.gunType == GunType.Shotgun)
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
        if (lastShootTime + gun.stats.fireSpeed < Time.time)
        {
            canShoot = true;
        }
        if (canShoot && gun.attachments.magazine.currentAmmo > 0 && !stats.reloading && !switching)
        {
            shootingSystem.Play();
            mainCamera.GetComponent<CameraShake>().Shake();
            canShoot = false;

            Vector3 direction = AccuracyVariation();

            StartCoroutine(SpawnProjectile(direction, 50f));
            lastShootTime = Time.time;
            gun.attachments.magazine.currentAmmo--;
        }
    }

    public void MultiBulletSystem()
    {
        shootingSystem.transform.position = barrel.GetComponent<Transform>().position;
        if (lastShootTime + gun.stats.fireSpeed < Time.time)
        {
            canShoot = true;
        }
        if (canShoot && gun.attachments.magazine.currentAmmo > 0 && !stats.reloading && !switching)
        {
            shootingSystem.Play();
            mainCamera.GetComponent<CameraShake>().Shake();
            canShoot = false;

            for (int i = 0; i < gun.pelletCount; i++)
            {
                Vector3 direction = AccuracyVariation();

                StartCoroutine(SpawnProjectile(direction, 50f));
            }

            lastShootTime = Time.time;
            gun.attachments.magazine.currentAmmo--;
        }
    }

    private Vector3 AccuracyVariation()
    {
        Vector3 direction = playerAim.GetMousePosition();
        Vector3 target = direction - characterTransform.position;

        if (aiming)
        {
            target += new Vector3(
            UnityEngine.Random.Range(-gun.stats.accuracy * 0.5f, gun.stats.accuracy * 0.5f), -1.5f,
            UnityEngine.Random.Range(-gun.stats.accuracy * 0.5f, gun.stats.accuracy * 0.5f));
        }
        else
        {
            target += new Vector3(
            UnityEngine.Random.Range(-gun.stats.accuracy, gun.stats.accuracy), -1.5f,
            UnityEngine.Random.Range(-gun.stats.accuracy, gun.stats.accuracy));
        }

        target.Normalize();

        return target;
    }

    private IEnumerator SpawnProjectile(Vector3 direction, float distance)
    {
        Vector3 original = barrel.transform.position;

        GameObject bullet = Instantiate(projectile, barrel.transform.position, Quaternion.identity);

        // bullet.GetComponent<Bullet>().bullet = gun.stats.bullet;
        // bullet.GetComponent<Bullet>().source = characterTransform.gameObject.tag;

        // while (bullet != null && Math.Abs(Vector3.Distance(bullet.transform.position, original)) < distance)
        // {
        //     bullet.transform.localPosition += direction * gun.stats.bullet.velocity * Time.deltaTime;
        //     yield return null;
        // }

        bullet.GetComponent<Bullet>().Fire(direction, distance, gun.stats.bullet, characterTransform.gameObject.tag);

        yield return null;
    }
}
