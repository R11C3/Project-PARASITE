using UnityEngine;

[CreateAssetMenu(fileName = "SO_Backpack", menuName = "Scriptable Objects/Item/Backpack")]
public class SO_Backpack : SO_InventoryItem
{
    public void Reset()
    {
        type = Type.Backpack;
        itemStats.itemStatsList.Add(new ItemStatistic("Total Size", 0));
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
    }
}
