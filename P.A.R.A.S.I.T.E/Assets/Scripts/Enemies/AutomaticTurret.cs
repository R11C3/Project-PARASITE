using System;
using System.Collections;
using UnityEngine;

public class AutomaticTurret : MonoBehaviour
{
    [SerializeField]
    private SO_Gun gun;
    [SerializeField]
    private GameObject barrel;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private ParticleSystem shootingParticle;
    [SerializeField]
    private ParticleSystem impactParticle;

    [SerializeField]
    private float sightDistance;

    [SerializeField]
    private GameObject target;

    [SerializeField]
    private LayerMask mask;

    private float lastShootTime;
    private bool canShoot = true;

    void Start()
    {

    }

    void Update()
    {
        if (Math.Abs(Vector3.Distance(gameObject.transform.position, target.transform.position)) < 10f)
        {
            LookAt();
            Shoot();
        }
    }

    void LookAt()
    {
        transform.LookAt(target.transform.position);
    }

    void Shoot()
    {
        shootingParticle.transform.position = barrel.transform.position;
        if(lastShootTime + gun.stats.fireSpeed < Time.time)
        {
            canShoot = true;
        }
        if (canShoot)
        {
            Vector3 direction = target.transform.position - gameObject.transform.position;
            direction.y = target.transform.position.y + 0.5f;

            RaycastHit hit;
            if (Physics.Raycast(barrel.transform.position, direction, out hit, 100f))
            {
                if (hit.transform.gameObject.layer == 6)
                {
                    direction = AccuracyVariation(direction);

                    shootingParticle.Play();
                    Camera.main.GetComponent<CameraShake>().Shake();
                    canShoot = false;

                    StartCoroutine(SpawnProjectile(direction, 50f));

                }
            }
            lastShootTime = Time.time;
        }
    }

    Vector3 AccuracyVariation(Vector3 direction)
    {
        direction += new Vector3(
            UnityEngine.Random.Range(-gun.stats.accuracy, gun.stats.accuracy), 0,
            UnityEngine.Random.Range(-gun.stats.accuracy, gun.stats.accuracy));

        direction.Normalize();
        return direction;
    }

    IEnumerator SpawnProjectile(Vector3 direction, float distance)
    {
        GameObject bullet = Instantiate(projectile, barrel.transform.position, Quaternion.identity);

        bullet.GetComponent<Bullet>().Fire(direction, distance, gun.stats.bullet, transform.gameObject.tag);

        yield return null;
    }
}
