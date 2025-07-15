using System;
using JetBrains.Annotations;
using UnityEngine;

[Serializable]
public class Backpack
{
    private int sizeLevel;
    private int weightLevel;

    [SerializeField]
    public Mesh mesh;
    [SerializeField]
    public Material material;
    [SerializeField]
    public Texture2D sprite;

    public GridInventory inventory = new GridInventory(5, 2);

    public void SizeUpgrade()
    {
        GridInventory newInventory = new GridInventory(6, inventory.dimensions.height + 2);

        foreach (SO_Item item in inventory.itemList)
        {
            newInventory.Add(item);
        }

        inventory = newInventory;
    }
}