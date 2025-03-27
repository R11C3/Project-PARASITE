using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Mob", menuName = "Scriptable Objects/Mob")]
public abstract class SO_Mob : ScriptableObject
{
    [Header("UnModified Stats")]
    public float speed;
    public float acceleration;
    public float deceleration;
    public float sprintSpeed;
    public float rollSpeed;
    public float rollTime;
    public float rollDelay;
    public float maxHealth;
    public float health;
    public bool damageable;

    [Header("Inventory")]
    public Dictionary<string, SO_Item> inventory;
    
    public void addItem(SO_Item item) 
    {
        inventory.Add(item.name, item);
    }

    public virtual void DoDamage(float damage)
    {
        health -= damage;
    }
    public abstract void Shoot();
    public abstract void Reload();
}
