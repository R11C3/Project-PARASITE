using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_LootTable", menuName = "Loot Table")]
[Serializable]
public class SO_LootTable : ScriptableObject
{
    public SpawnChance[] lootTable;

    public List<SO_Item> table;

    public void GenerateTable()
    {
        foreach(SpawnChance obj in lootTable)
        {
            for(int i = 0; i < obj.chance; i++)
            {
                table.Add(obj.item);
            }
        }
    }

    public SO_Item GetRandomItem()
    {
        int length = table.Count;

        SO_Item item = table[UnityEngine.Random.Range(0, length)];

        table.Remove(item);

        return item;
    }

    [Serializable]
    public struct SpawnChance
    {
        [SerializeField]
        public SO_Item item;
        [SerializeField]
        public float chance;
    }
}
