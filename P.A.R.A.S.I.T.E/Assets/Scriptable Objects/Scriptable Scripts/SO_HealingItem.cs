using UnityEngine;

[CreateAssetMenu(fileName = "SO_HealingItem", menuName = "Scriptable Objects/Item/Medical")]
public class SO_HealingItem : SO_Item
{
    public void Reset()
    {
        type = Type.Medical;
    }
}
