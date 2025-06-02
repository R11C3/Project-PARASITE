using UnityEngine;

public class PickUp : Interactable
{
    [SerializeField]
    public SO_Item itemData;

    private SO_Item item;

    public void Start()
    {
        item = Instantiate(itemData);
    }

    public override void Interact(GameObject source)
    {
        Debug.Log("Interacted");
        PlayerStats playerStats;
        source.TryGetComponent<PlayerStats>(out playerStats);

        bool success = playerStats.equipmentInventory.AddItem(playerStats, item);

        if(success) Destroy(transform.gameObject);
    }
}
