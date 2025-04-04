using System;
using UnityEngine;

[Serializable]
public class Inventory
{
    [SerializeField]
    private SO_Item[] inventory;
    private int size;
    private int nullIndex;

    public Inventory()
    {
        size = 20;
        inventory = new SO_Item[size];
        nullIndex = 0;
    }

    public Inventory(int size)
    {
        this.size = size;
        inventory = new SO_Item[size];
        nullIndex = 0;
    }

    public SO_Item Get(int index)
    {
        return inventory[index];
    }

    private void AdvanceNullIndex()
    {
        for(int i = 0; i < size; i++)
        {
            if(inventory[i] == null)
            {
                nullIndex = i;
            }
        }
    }

    public void Add(SO_Item item)
    {
        if(nullIndex != -1)
        {
            inventory[nullIndex] = item;
        }
        AdvanceNullIndex();
    }

    public void Remove(SO_Item item)
    {
        int removeIndex = FindIndex(item);
        if(removeIndex != -1)
        {
            inventory[removeIndex] = null;
        }
    }

    private int FindIndex(SO_Item item)
    {
        for(int i = 0; i < size; i++)
        {
            if(inventory[i].Equals(item))
            {
                return i;
            }
        }
        return -1;
    }

    public SO_Item FindItem(SO_Item item)
    {
        for(int i = 0; i < size; i++)
        {
            if(inventory[i].Equals(item))
            {
                return item;
            }
        }
        return null;
    }

    public void ClearInventory()
    {
        Array.Fill<SO_Item>(inventory, null);
    }

    public void ExposeInventory()
    {
        string str = "";
        foreach (SO_Item item in inventory)
        {
            str += item.ToString() + "\n";
        }
        Debug.Log(str);
    }
}
