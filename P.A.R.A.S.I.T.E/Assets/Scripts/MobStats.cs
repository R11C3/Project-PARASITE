using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public enum Holding{Rifle, Pistol, None};
public enum Action{None, Inventory, Looting};
public enum Stance{Walking, Running, Crouching, Aiming};

public class MobStats : Damageable
{
    [Header("Stats")]
    public float speed;
    public float sprintMult;
    public float crouchMult;
    public float acceleration;
    public float deceleration;
    public float maxStamina;
    public float currentStamina;
    public float staminaRegen;
    public float staminaDegen;

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

    protected override void Load()
    {
        speed = ((SO_Mob)stats).speed;
        sprintMult = ((SO_Mob)stats).sprintMult;
        crouchMult = ((SO_Mob)stats).crouchMult;
        acceleration = ((SO_Mob)stats).acceleration;
        deceleration = ((SO_Mob)stats).deceleration;
        damageable = stats.damageable;
        sprintSpeed = speed * sprintMult;
        crouchSpeed = speed * crouchMult;
        maxStamina = ((SO_Mob)stats).maxStamina;
        currentStamina = ((SO_Mob)stats).maxStamina;
        staminaRegen = ((SO_Mob)stats).staminaRegen;
        staminaDegen = ((SO_Mob)stats).staminaDegen;
        base.Load();
    }
}
