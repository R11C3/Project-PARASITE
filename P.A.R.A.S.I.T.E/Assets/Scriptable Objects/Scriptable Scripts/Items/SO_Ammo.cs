using UnityEngine;

[CreateAssetMenu(fileName = "SO_Ammo", menuName = "Scriptable Objects/Item/Ammo")]
public class SO_Ammo : SO_Item
{
    [Header("Stacking")]
    public int maxStack;
    public int currentStack;

    public override void AppendStats()
    {
        
    }

    void Reset()
    {
        type = Type.Ammo;
        AppendStats();
    }
}
