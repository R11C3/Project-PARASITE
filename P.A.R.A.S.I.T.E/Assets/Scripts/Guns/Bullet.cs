using UnityEngine;

public class Bullet : MonoBehaviour
{
    public SO_Bullet bullet;
    public string source;

    private float damage;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6 && !other.gameObject.tag.Equals(source))
        {
            MobStats target;
            other.gameObject.TryGetComponent<MobStats>(out target);
            if(target != null) target.DoDamage(CalculateDamage());
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

    float CalculateDamage()
    {
        return (bullet.velocity/10 + bullet.penetration) * bullet.bluntDamage;
    }
}
