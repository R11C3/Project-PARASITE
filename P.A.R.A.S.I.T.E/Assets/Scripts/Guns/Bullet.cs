using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public string source;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6 && !other.gameObject.tag.Equals(source))
        {
            MobStats target;
            other.gameObject.TryGetComponent<MobStats>(out target);
            if(target != null) target.DoDamage(damage);
            Destroy(gameObject);
        }
        else if(!other.gameObject.tag.Equals(source))
        {
            Destroy(gameObject);
        }
        else
        {
            
        }
    }
}
