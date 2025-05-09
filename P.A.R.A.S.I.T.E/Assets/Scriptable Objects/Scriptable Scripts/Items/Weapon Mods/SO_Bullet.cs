using UnityEngine;

[CreateAssetMenu(fileName = "SO_Bullet", menuName = "Scriptable Objects/Weapon Mod/Bullet")]
public class SO_Bullet : SO_Item
{
    public float velocity;
    public float penetration;
    public float bluntDamage;

    void Reset()
    {
        type = Type.Ammo;
    }
}
