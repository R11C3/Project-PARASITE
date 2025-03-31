using UnityEngine;

public class InventorySlot
{
    public SO_Item item {get; set;}
    public int amount {get; set;}

    public InventorySlot(SO_Item item)
    {
        this.item = item;
        amount = 1;
    }

    public InventorySlot(SO_Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }

    public bool Add(int amount)
    {
        this.amount += amount;
        return true;
    }

    public bool Remove(int amount)
    {
        this.amount -= amount;
        if(this.amount < 0)
        {
            this.amount = 1;
            return false;
        }
        return true;
    }

    public bool Equals(InventorySlot comparison)
    => comparison.item.itemName.Equals(item.itemName);
}