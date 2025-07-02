using UnityEngine;

public class PickUp : Interactable
{
    [SerializeField]
    public SO_Item itemData;

    private SO_Item item;

    private ItemHoverText hoverText;

    public void Start()
    {
        item = Instantiate(itemData);
        TryGetComponent<ItemHoverText>(out hoverText);
    }

    public override void Interact(GameObject source)
    {
        Debug.Log("Interacted");
        PlayerStats playerStats;
        source.TryGetComponent<PlayerStats>(out playerStats);

        bool success = playerStats.equipmentInventory.AddItem(playerStats, item);

        var hitbox = GetComponent<Collider>();

        hitbox.enabled = false;

        if (hoverText != null)
        {
            hoverText.hoverText.text = " ";
        }

        if (success) Destroy(transform.gameObject);
    }
}
