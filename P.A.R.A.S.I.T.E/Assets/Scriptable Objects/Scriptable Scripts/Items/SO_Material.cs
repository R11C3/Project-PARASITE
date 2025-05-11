using UnityEngine;

[CreateAssetMenu(fileName = "SO_Material", menuName = "Scriptable Objects/Item/Material")]
public class SO_Material : SO_Item
{
    public void Reset()
    {
        type = Type.Material;
        AppendStats();
    }

    public override void AppendStats()
    {
        base.AppendStats();
    }
}
