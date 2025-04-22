using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Container : Interactable
{
    [SerializeField]
    private UIDocument lootingInventory;
    [SerializeField]
    private VisualTreeAsset containerUI;
    private VisualElement containerVisualElement;
    [SerializeField]
    private SO_Container containerData;
    private SO_Container container;

    private VisualElement root;
    private VisualElement body;
    private PlayerStats stats;

    [SerializeField]
    private SO_Input input;

    void Awake()
    {
        containerVisualElement = containerUI.Instantiate();
        container = Instantiate(containerData);
        container.IntializeInventories();
    }

    public override void Interact(GameObject source)
    {
        // source.TryGetComponent<PlayerStats>(out stats);

        // stats.gridHandler.LootingContainer(containerVisualElement, container);
    }
}
