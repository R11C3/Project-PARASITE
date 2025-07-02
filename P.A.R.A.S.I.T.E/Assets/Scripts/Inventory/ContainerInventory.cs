using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ContainerInventory
{
    [SerializeField]
    public List<SO_Item> itemList;

    [SerializeField]
    public int size;

    public ContainerInventory()
    {
        itemList = new List<SO_Item>();
        size = 1;
    }

    public ContainerInventory(int size)
    {
        itemList = new List<SO_Item>();
        this.size = size;
    }

    public void Add(SO_Item item)
    {
        itemList.Add(item);
    }
}
