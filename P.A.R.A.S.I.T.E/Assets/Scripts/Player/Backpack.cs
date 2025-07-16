using System;
using JetBrains.Annotations;
using UnityEngine;

[Serializable]
public class Backpack
{
    private int sizeLevel;
    private int weightLevel;

    public Mesh mesh;
    public Material material;
    public Texture2D sprite;
    public Vector3 backpackScale;
    public Vector3 backpackRotation;
    public Vector3 backpackLocation;

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