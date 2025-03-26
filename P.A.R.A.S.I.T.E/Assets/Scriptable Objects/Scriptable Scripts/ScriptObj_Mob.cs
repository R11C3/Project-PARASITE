using UnityEngine;

[CreateAssetMenu(fileName = "ScriptObj_Mob", menuName = "Scriptable Objects/Mob")]
public class ScriptObj_Mob : ScriptableObject
{
    [Header("UnModified Stats")]
    public float speed;
    public float acceleration;
    public float deceleration;
    public float sprintSpeed;
    public float rollSpeed;
    public float rollTime;
    public float rollDelay;
    public float health;

    [Header("Specialty")]
    public bool damageable;
}
