using Mono.Cecil.Cil;
using UnityEngine;

[RequireComponent(typeof(MobStats))]

public class Enemy : MonoBehaviour
{
    private MobStats mob;

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

        Debug.Log(gameObject + " damaged\nHealth remaining: " + mob.currentHealth);
    }
}
