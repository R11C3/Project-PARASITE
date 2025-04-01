using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Player", menuName = "Scriptable Objects/Player")]
public class SO_Player : SO_Mob
{
    [Header("Weapon Inventory")]
    public SO_Gun[] weaponInventory;
    public int activeSlot;
    [Header("Item Inventory")]
    public List<InventorySlot> itemInventory = new List<InventorySlot>();

    public void ReplaceGun(int index, SO_Gun newGun)
    {
        weaponInventory[index] = Instantiate(newGun);
    }

    public void Add(SO_Item item)
    {
        int index = findItem(item);
        if(index == -1)
        {
            itemInventory.Add(new InventorySlot(item, 1));
        }
        else
        {
            itemInventory[index].Add(1);
        }
        ExposeInventory();
    }

    public void Remove(SO_Item item)
    {
        int index = findItem(item);
        if(index >= 0)
        {
            if(!itemInventory[index].Remove(1))
            {
                itemInventory.RemoveAt(index);
            }
        }
    }

    public void ClearInventory()
    {
        if(itemInventory.Count > 0)
        {
            itemInventory.Clear();
        }
    }

    public int findItem(SO_Item item)
    {
        int count = 0;
        foreach (InventorySlot slot in itemInventory)
        {
            if(slot.item.itemName.Equals(item.itemName))
            {
                return count;
            }
            count++;
        }
        return -1;
    }

    public void ExposeInventory()
    {
        string str = "";
        foreach (InventorySlot slot in itemInventory)
        {
            str += slot.item.itemName + " | " + slot.amount + "\n";
        }
        Debug.Log(str);
    }

    public override void DoDamage(float damage)
    {
        health -= damage;
    }

    public override void Reload()
    {
        throw new System.NotImplementedException();
    }

    public override void Shoot()
    {
        throw new System.NotImplementedException();
    }
}
