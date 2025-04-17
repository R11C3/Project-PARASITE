using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public enum Holding{Rifle, Pistol, None};
public enum Action{None, Inventory};
public enum Stance{Walking, Running, Crouching, Aiming};

public class MobStats : MonoBehaviour
{
    [SerializeField]
    public SO_Mob stats;

    [Header("Health")]
    public float maxHealth;
    public float currentHealth = 100;

    [Header("Stats")]
    public float speed;
    public float sprintMult;
    public float crouchMult;
    public float acceleration;
    public float deceleration;
    public bool damageable;

    protected float sprintSpeed;
    protected float crouchSpeed;

    [Header("In Hand")]
    public Holding inHand;
    [Header("Current Action")]
    public Action action;
    [Header("Stance")]
    public Stance stance = Stance.Walking;

    void Awake()
    {
        Load();
    }

    protected virtual void Load()
    {
        maxHealth = stats.maxHealth;
        speed = stats.speed;
        sprintMult = stats.sprintMult;
        crouchMult = stats.crouchMult;
        acceleration = stats.acceleration;
        deceleration = stats.deceleration;
        damageable = stats.damageable;
        sprintSpeed = speed * sprintMult;
        crouchSpeed = speed * crouchMult;
    }

    public void DoDamage(float damage)
    {
        if(damageable)
            currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
