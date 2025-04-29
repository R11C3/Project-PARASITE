using UnityEngine;

[CreateAssetMenu(fileName = "SO_Damageable", menuName = "Scriptable Objects/Damageable")]
public class SO_Damageable : ScriptableObject
{
    [Header ("Health")]
    public float maxHealth;
    public float health;
    public bool damageable;
}
