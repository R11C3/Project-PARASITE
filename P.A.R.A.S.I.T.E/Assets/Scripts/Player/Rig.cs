using System;
using JetBrains.Annotations;
using UnityEngine;

[Serializable]
public class Rig
{
    private int sizeLevel;
    private int weightLevel;

    public Mesh mesh;
    public Material material;
    public Texture2D sprite;
    public Vector3 rigScale;
    public Vector3 rigRotation;
    public Vector3 rigLocation;

    public GridInventory inventory = new GridInventory(3, 2);

    public void SizeUpgrade()
    {
        GridInventory newInventory;

        if (inventory.dimensions.width < 6)
        {
            newInventory = new GridInventory(inventory.dimensions.width++, inventory.dimensions.height);
        }
        else
        {
            newInventory = new GridInventory(inventory.dimensions.width, inventory.dimensions.height++);
        }

        foreach (SO_Item item in inventory.itemList)
            {
                newInventory.Add(item);
            }

        inventory = newInventory;
    }
}