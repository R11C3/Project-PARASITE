using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Mob", menuName = "Scriptable Objects/Mob/Mob")]
public abstract class SO_Mob : ScriptableObject
{
    [Header("UnModified Stats")]
    public float speed;
    public float acceleration;
    public float deceleration;
    public float sprintMult;
    public float crouchMult;
    public float rollSpeed;
    public float rollTime;
    public float rollDelay;
    public float maxHealth;
    public float health;
    public bool damageable;
}
