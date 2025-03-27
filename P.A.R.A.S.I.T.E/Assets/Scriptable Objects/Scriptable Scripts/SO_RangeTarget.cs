using UnityEngine;

[CreateAssetMenu(fileName = "SO_RangeTarget", menuName = "Scriptable Objects/RangeTarget")]
public class SO_RangeTarget : SO_Mob
{
    public override void DoDamage(float damage)
    {
        health -= damage;
    }

    public override void Reload()
    {
        throw new System.NotImplementedException();
    }

    public override void Shoot()
    {
        throw new System.NotImplementedException();
    }
}
