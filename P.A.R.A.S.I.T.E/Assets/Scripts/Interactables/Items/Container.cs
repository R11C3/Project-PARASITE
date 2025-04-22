using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Container : Interactable
{
    [SerializeField]
    private SO_Container containerData;
    private SO_Container container;
    [SerializeField]
    private SO_LootTable lootTableData;
    private SO_LootTable lootTable;

    [SerializeField]
    private PlayerStats stats;

    void Awake()
    {
        container = Instantiate(containerData);
        container.IntializeInventories();

        lootTable = Instantiate(lootTableData);
        lootTable.GenerateTable();
    }

    void Start()
    {
        GenerateLoot();
    }

    public SO_Item GetRandomItem()
    {
        int length = lootTable.table.Count;

        SO_Item item = lootTable.table[UnityEngine.Random.Range(0, length)];

        lootTable.table.Remove(item);

        return item;
    }

    public void GenerateLoot()
    {
        int attemptedItems = UnityEngine.Random.Range(0, container.dimensions.width * container.dimensions.height);
        for(int i = 0; i < attemptedItems; i++)
        {
            if(lootTable.table.Count <= 0)
            {
                break;
            }

            container.inventory.Add(Instantiate(GetRandomItem()));
        }
    }

    public override void Interact(GameObject source)
    {
        source.TryGetComponent<PlayerStats>(out stats);

        stats.externalGridHandler.container = container;

        if(stats.canToggle)
        {
            stats.canToggle = false;

            stats.action = Action.Looting;

            stats.playerUI.visible = false;
            stats.gridHandler.visible = false;
            stats.externalGridHandler.visible = true;
            if(stats.equipmentInventory.backpack != null) stats.externalGridHandler.LoadBackpackInventoryItems();
            if(stats.equipmentInventory.rig != null) stats.externalGridHandler.LoadRigInventoryItems();
            stats.externalGridHandler.LoadContainerItems();
        }
    }
}
