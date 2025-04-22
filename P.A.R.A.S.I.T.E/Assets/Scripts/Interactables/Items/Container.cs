using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Container : Interactable
{
    [SerializeField]
    private SO_Container containerData;
    private SO_Container container;

    [SerializeField]
    private PlayerStats stats;

    void Awake()
    {
        container = Instantiate(containerData);
        container.IntializeInventories();
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
