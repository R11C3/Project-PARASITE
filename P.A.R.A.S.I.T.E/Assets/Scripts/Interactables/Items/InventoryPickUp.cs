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
        item.obj = gameObject;
    }

    public override void Interact(GameObject source)
    {
        Debug.Log("Interacted");
        PlayerStats playerStats;
        source.TryGetComponent<PlayerStats>(out playerStats);

        playerStats.inventory.Add(item);

        gameObject.SetActive(false);
    }
}
