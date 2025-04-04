using Mono.Cecil.Cil;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private MobStats mob;

    float health;
    bool damageable;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        mob = GetComponent<MobStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoDamage(float damage)
    {
        mob.DoDamage(damage);

        Debug.Log(gameObject + " damaged\nHealth remaining: " + health);

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
