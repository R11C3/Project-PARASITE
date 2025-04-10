using System.ComponentModel.Design.Serialization;
using System.Linq;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryGridHandler : MonoBehaviour
{
    [SerializeField]
    private SO_Input input;
    private PlayerStats stats;
    
    public bool visible;

    [SerializeField]
    private SO_Backpack backpack;

    private VisualElement root;
    private VisualElement backpackHolder;

    private Dimensions slotDimensions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stats = gameObject.transform.parent.GetComponent<PlayerStats>();
        root = GetComponent<UIDocument>().rootVisualElement;
        backpackHolder = root.Q<VisualElement>("backpack");
        ConfigureSlotDimensions();
        root.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        root.visible = visible;
    }

    void ConfigureSlotDimensions()
    {
        slotDimensions = new Dimensions {width = 100, height = 100 };
    }

    public void LoadBackpackInventories()
    {
        
        backpack = stats.equipmentInventory.backpack;
        int width = backpack.inventories[0].dimensions.width;
        int height = backpack.inventories[0].dimensions.height;

        backpackHolder.style.maxWidth = slotDimensions.width * width + 5;
        backpackHolder.style.maxHeight = slotDimensions.height * height + 5;

        for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                VisualElement slot = new VisualElement();
                slot.AddToClassList("item-slot");
                backpackHolder.Add(slot);
            }
        }
    }

    public void RemoveBackpackItemSlots()
    {
        backpackHolder.Clear();
    }

    public void LoadBackpackInventoryItems()
    {
        foreach(SO_Item item in backpack.inventories[0].itemList)
        {
            VisualElement visualIconContainer = new VisualElement();
            visualIconContainer.AddToClassList("visual-icon-container");

            visualIconContainer.style.height = item.dimensions.height * slotDimensions.height;
            visualIconContainer.style.width = item.dimensions.width * slotDimensions.width;

            visualIconContainer.style.top = item.location.height * slotDimensions.height;
            visualIconContainer.style.left = item.location.width * slotDimensions.width;

            visualIconContainer.style.backgroundImage = item.sprite;

            backpackHolder.Add(visualIconContainer);
        }

    }

    void ToggleVisibility(bool visible)
    {
        root.visible = visible;
    }
}
