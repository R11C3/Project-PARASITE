using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

[Serializable] 
public class GridInventory
{
    [SerializeField]
    public List<SO_Item> itemList;

    [SerializeField]
    public SO_Item[,] grid;
    public Dimensions dimensions;

    public GridInventory (int width, int height)
    {
        dimensions.width = width;
        dimensions.height = height;
        grid = new SO_Item[dimensions.height,dimensions.width];
        itemList = new List<SO_Item>();
    }

    public bool Add(SO_Item item)
    {
        int width = item.dimensions.width;
        int height = item.dimensions.height;
        bool success = false;

        for(int i = 0; i < dimensions.height; i++)
        {
            for(int j = 0; j < dimensions.width; j++)
            {
                if(grid[i,j] == null && i + height < dimensions.height && j + width < dimensions.width)
                {
                    if(CheckNull(i, j, width, height))
                    {
                        itemList.Add(item);
                        AddToGrid(i, j, width, height, item);
                        item.location = new Dimensions{height = i, width = j};
                        success = true;
                        break;
                    }
                }
            }
            if(success) break;
        }

        ExposeInventory();

        return success;
    }

    public bool CheckNull(int row, int col, int width, int height)
    {
        bool success = true;
        for(int i = row; i < row + height; i++)
        {
            for(int j = col; j < col + width; j++)
            {
                if(grid[i,j] != null)
                {
                    success = false;
                }
            }
        }

        return success;
    }

    public void AddToGrid(int row, int col, int width, int height, SO_Item item)
    {
        for(int i = row; i < row + height; i++)
        {
            for(int j = col; j < col + width; j++)
            {
                grid[i,j] = item;
            }
        }
    }

    public bool Remove(SO_Item item)
    {
        int width = item.dimensions.width;
        int height = item.dimensions.height;
        bool success = false;

        for(int i = 0; i < dimensions.height; i++)
        {
            for(int j = 0; j < dimensions.width; j++)
            {
                if(grid[i,j] == item)
                {
                    AddToGrid(i, j, width, height, null);
                    itemList.Remove(item);
                    success = true;
                    break;
                }
            }
        }

        ExposeInventory();

        return success;
    }

    public void DropItem(GameObject source, SO_Item item)
    {
        Vector3 position = source.transform.position;
        position.x += 1f;
        item.obj.transform.position = position;
        item.obj.SetActive(true);
        Remove(item);
    }

    public SO_Item Get(int index)
    {
        return itemList[index];
    }

    public void ExposeInventory()
    {
        string str = "";
        foreach(SO_Item item in itemList)
        {
            str += item.itemName + "\n";
        }

        Debug.Log(str);
    }
}