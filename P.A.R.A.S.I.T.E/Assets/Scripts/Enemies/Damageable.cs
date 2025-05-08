using UnityEngine;

public abstract class Damageable : MonoBehaviour
{
    [Header ("Scriptable Object")]
    public SO_Damageable stats;

    [Header ("Health")]
    public float maxHealth;
    public float currentHealth;
    public bool damageable;

    protected virtual void Load()
    {
        maxHealth = stats.maxHealth;
        currentHealth = stats.maxHealth;
    }

    public virtual void DoDamage(float damage)
    {
        if(damageable)
            currentHealth -= damage;

        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
        Debug.Log(damage + " inflicted onto " + gameObject + "\nHealth Remaining: " + currentHealth);
    }
}
