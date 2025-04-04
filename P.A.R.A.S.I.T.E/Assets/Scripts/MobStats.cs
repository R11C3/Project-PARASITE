using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class MobStats : MonoBehaviour
{
    [SerializeField]
    public SO_Mob stats;

    [Header("Health")]
    public float maxHealth;
    public float currentHealth = 100;

    [Header("Stats")]
    public float speed;
    public float sprintSpeed;
    public float acceleration;
    public float deceleration;
    public bool damageable;

    void Awake()
    {
        Load();
    }

    protected virtual void Load()
    {
        maxHealth = stats.maxHealth;
        speed = stats.speed;
        sprintSpeed = stats.sprintSpeed;
        acceleration = stats.acceleration;
        deceleration = stats.deceleration;
        damageable = stats.damageable;
    }

    public void DoDamage(float damage)
    {
        currentHealth -= damage;
    }
}
