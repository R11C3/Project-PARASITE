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

    private float lastShootTime;
    private bool canShoot = true;

    void Start()
    {

    }

    void Update()
    {
        if(Math.Abs(Vector3.Distance(transform.position, target.transform.position)) < sightDistance)
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
        if(canShoot)
        {
            shootingParticle.Play();
            Camera.main.GetComponent<CameraShake>().Shake();
            canShoot = false;

            Vector3 direction = AccuracyVariation();
            
            StartCoroutine(SpawnProjectile(direction, 50f));

            lastShootTime = Time.time;
        }
    }

    Vector3 AccuracyVariation()
    {
        Vector3 direction = target.transform.position - gameObject.transform.position;

        direction += new Vector3(
            UnityEngine.Random.Range(-gun.stats.accuracy, gun.stats.accuracy), 1.5f,
            UnityEngine.Random.Range(-gun.stats.accuracy, gun.stats.accuracy));

        direction.Normalize();
        return direction;
    }

    IEnumerator SpawnProjectile(Vector3 direction, float distance)
    {
        Vector3 original = barrel.transform.position;

        GameObject bullet = Instantiate(projectile, barrel.transform.position, Quaternion.identity);

        bullet.GetComponent<Bullet>().bullet = gun.stats.bullet;
        bullet.GetComponent<Bullet>().source = gameObject.tag;

        while(bullet != null && Math.Abs(Vector3.Distance(bullet.transform.position, original)) < distance)
        {
            bullet.transform.localPosition += direction * gun.stats.bullet.velocity * Time.deltaTime;
            yield return null;
        }

        Destroy(bullet);

        yield return null;
    }
}
