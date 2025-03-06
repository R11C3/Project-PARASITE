using System.Collections;
using UnityEngine;

public class BallisticGun : MonoBehaviour
{

    [SerializeField]
    private GameObject gunBarrel;
    private Vector3 EndOfBarrel;
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
    [SerializeField]
    private float shootDelay = 0.5f;
    private float lastShootTime;
    [SerializeField]
    private bool canShoot = true;

    public void Shoot()
    {
        EndOfBarrel = gunBarrel.transform.position;
        shootingSystem.transform.position = gunBarrel.transform.position;
        if(lastShootTime + shootDelay < Time.time)
        {
            canShoot = true;
        }
        if(canShoot)
        {
            shootingSystem.Play();
            canShoot = false;

            Vector3 target = playerAim.GetMousePosition();

            Vector3 direction = target - characterTransform.position;

            direction.y = 0;

            TrailRenderer tempTrail = Instantiate(trail, EndOfBarrel, Quaternion.identity);

            if (Physics.Raycast(EndOfBarrel, direction, out RaycastHit hit, float.MaxValue, mask))
            {
                StartCoroutine(SpawnTrail(tempTrail, hit.point, true));
            }
            else
            {
                StartCoroutine(SpawnTrail(tempTrail, direction * 100, false));
            }
            lastShootTime = Time.time;
        }
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
