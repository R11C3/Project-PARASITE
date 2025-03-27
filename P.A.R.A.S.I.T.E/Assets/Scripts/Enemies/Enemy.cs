using Mono.Cecil.Cil;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private SO_Mob statsTemplate;
    private SO_Mob stats;

    float health;
    bool damageable;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        stats = Instantiate(statsTemplate);
        InitializeStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeStats()
    {
        health = stats.health;
        damageable = stats.damageable;
    }

    public void DoDamage(float damage)
    {
        stats.DoDamage(damage);

        Debug.Log(gameObject + " damaged\nHealth remaining: " + health);

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
