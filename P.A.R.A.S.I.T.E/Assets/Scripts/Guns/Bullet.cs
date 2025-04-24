using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            MobStats target;
            other.gameObject.TryGetComponent<MobStats>(out target);
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
