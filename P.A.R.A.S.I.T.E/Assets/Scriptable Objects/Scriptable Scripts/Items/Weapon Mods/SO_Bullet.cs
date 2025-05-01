using UnityEngine;

[CreateAssetMenu(fileName = "SO_Bullet", menuName = "Scriptable Objects/Weapon Mod/Bullet")]
public class SO_Bullet : ScriptableObject
{
    public float velocity;
    public float penetration;
    public float bluntDamage;
}
