using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public SO_Bullet bullet;
    public string source;

    private float damage;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6 && !other.gameObject.tag.Equals(source))
        {
            MobStats target;
            other.gameObject.TryGetComponent<MobStats>(out target);
            if (target != null) target.DoDamage(CalculateDamage());
            Destroy(gameObject);
        }
        else if (!other.gameObject.tag.Equals(source))
        {
            Destroy(gameObject);
        }
        else
        {

        }
    }

    float CalculateDamage()
    {
        return (bullet.velocity / 10 + bullet.penetration) * bullet.bluntDamage;
    }

    public void Fire(Vector3 direction, float distance, SO_Bullet bullet, string source)
    {
        StartCoroutine(FireRoutine(direction, distance, bullet, source));
    }

    IEnumerator FireRoutine(Vector3 direction, float distance, SO_Bullet bullet, string source)
    {
        this.bullet = bullet;
        this.source = source;

        Vector3 original = transform.position;

        while (bullet != null && Math.Abs(Vector3.Distance(transform.position, original)) < distance)
        {
            transform.localPosition += direction * bullet.velocity * Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);

        yield return null;
    }
}
