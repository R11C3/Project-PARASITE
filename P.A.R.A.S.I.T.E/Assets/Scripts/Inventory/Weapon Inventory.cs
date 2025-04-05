using System;
using UnityEngine;

[Serializable]
public class WeaponInventory
{
    [SerializeField]
    private SO_Gun[] inventory;
    private int size;

    public WeaponInventory()
    {
        size = 2;
        inventory = new SO_Gun[size];
    }

    public SO_Gun Get(int index)
    {
        return inventory[index];
    }

    public void Replace(int index, SO_Gun gun)
    {
        inventory[index] = gun;
    }

    private int FindIndex(SO_Gun item)
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

    public SO_Gun FindItem(SO_Gun item)
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
        Array.Fill<SO_Gun>(inventory, null);
    }

    public void ExposeInventory()
    {
        string str = "";
        foreach (SO_Gun item in inventory)
        {
            str += item.ToString() + "\n";
        }
        Debug.Log(str);
    }
}
