using UnityEngine;

public class InventoryPickUp : Interactable
{
    [SerializeField]
    private SO_InventoryItem itemData;

    private SO_InventoryItem item;

    public void Start()
    {
        item = Instantiate(itemData);
        item.InitializeInventories();
        item.InitializePhysical();
    }

    public override void Interact(GameObject source)
    {
        Debug.Log("Interacted");
        PlayerStats playerStats;
        source.TryGetComponent<PlayerStats>(out playerStats);

        bool success = playerStats.equipmentInventory.AddItem(playerStats, item);

        if(success)
        {
            gameObject.GetComponent<Collider>().enabled = false;
            Destroy(gameObject);
        }
    }
}
