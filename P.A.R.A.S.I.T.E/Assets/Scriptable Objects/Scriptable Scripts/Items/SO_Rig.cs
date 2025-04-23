using UnityEngine;

[CreateAssetMenu(fileName = "SO_Rig", menuName = "Scriptable Objects/Item/Rig")]
public class SO_Rig : SO_InventoryItem
{
    public void Reset()
    {
        type = Type.Rig;
    }

    public override void InitializeInventories()
    {
        inventories = new GridInventory(storageDimensions.width, storageDimensions.height);
    }

    public override void InitializePhysical()
    {
        MeshFilter filter;
        MeshRenderer renderer;
        obj.TryGetComponent<MeshFilter>(out filter);
        obj.TryGetComponent<MeshRenderer>(out renderer);

        scale = obj.GetComponent<Transform>().localScale;
        rotation = obj.GetComponent<Transform>().localEulerAngles;

        if(filter == null || renderer == null)
        {
            filter = obj.GetComponentInChildren<MeshFilter>();
            renderer = obj.GetComponentInChildren<MeshRenderer>();
            scale = filter.gameObject.GetComponent<Transform>().localScale;
            rotation = filter.gameObject.GetComponent<Transform>().localEulerAngles;
        }
        mesh = filter.sharedMesh;
        material = renderer.sharedMaterial;
        Debug.Log(scale);
    }
}
