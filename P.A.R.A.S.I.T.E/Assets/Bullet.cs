using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            Enemy target;
            other.gameObject.TryGetComponent<Enemy>(out target);
            if(target != null) target.DoDamage(damage);
            Destroy(gameObject);
        }
        else if(other.gameObject.layer == 0)
        {
            
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
