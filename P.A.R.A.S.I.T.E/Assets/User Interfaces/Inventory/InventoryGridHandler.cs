using System;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Net.NetworkInformation;
using JetBrains.Annotations;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class InventoryGridHandler : MonoBehaviour
{
    [SerializeField]
    private SO_Input input;
    private PlayerStats stats;
    private PlayerAim mouseAim;
    
    public bool visible;

    [SerializeField]
    private SO_Backpack backpack;

    private VisualElement root;
    private VisualElement backpackHolder;
    private VisualElement telegraph;

    private Dimensions slotDimensions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stats = gameObject.transform.parent.GetComponent<PlayerStats>();
        mouseAim = gameObject.transform.parent.GetComponent<PlayerAim>();
        root = GetComponent<UIDocument>().rootVisualElement;
        backpackHolder = root.Q<VisualElement>("backpack");
        ConfigureSlotDimensions();
        root.visible = false;
        ConfigureInventoryTelegraph();
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
        RemoveBackpackItemSlots();
        backpack = stats.equipmentInventory.backpack;
        int width = backpack.inventories.dimensions.width;
        int height = backpack.inventories.dimensions.height;

        backpackHolder.style.maxWidth = slotDimensions.width * width + 5;
        backpackHolder.style.maxHeight = slotDimensions.height * height + 5;

        Debug.Log(height * width);

        for(int i = 0; i < height * width; i++)
        {
            VisualElement slot = new VisualElement();
            slot.AddToClassList("item-slot");
            backpackHolder.Add(slot);
        }
    }

    public void RemoveBackpackItemSlots()
    {
        backpackHolder.Clear();
    }

    public void LoadBackpackInventoryItems()
    {
        RemoveBackpackItemSlots();
        LoadBackpackInventories();
        foreach(SO_Item item in backpack.inventories.itemList)
        {
            
            VisualElement visualIconContainer = new VisualElement();
            visualIconContainer.AddToClassList("visual-icon-container");

            VisualElement visualIcon = new VisualElement();
            visualIcon.AddToClassList("visual-icon");

            visualIconContainer.style.height = item.dimensions.height * slotDimensions.height;
            visualIconContainer.style.width = item.dimensions.width * slotDimensions.width;
            visualIconContainer.name = "visualIconContainer";
            visualIcon.name = "visualIcon";

            visualIconContainer.style.top = item.location.height * slotDimensions.height;
            visualIconContainer.style.left = item.location.width * slotDimensions.width;

            visualIcon.style.backgroundImage = item.sprite;

            backpackHolder.Add(visualIconContainer);
            visualIconContainer.Add(visualIcon);
        }
    }

    private void ConfigureInventoryTelegraph()
    {
        telegraph = new VisualElement();
        telegraph.name = "telegraph";

        telegraph.style.position = Position.Absolute;
        telegraph.style.visibility = Visibility.Hidden;

        telegraph.AddToClassList("item-icon-highlighted");
        backpackHolder.Add(telegraph);
    }

    private VisualElement selected;
    private bool isDragging = false;

    private Vector2 originalPosition;
    private Vector2 originalMousePosition;

    public Vector2 layout;

    private SO_Item item;

    public void SelectItem()
    {
        if(!isDragging)
        {
        Vector2 mousePosition = Input.mousePosition;
        mousePosition.y = Screen.height - mousePosition.y;

        IPanel panel = root.panel;
        Vector2 position = RuntimePanelUtils.ScreenToPanel(panel, mousePosition);

        selected = panel.Pick(position);

        if(selected != null && selected.name.Equals("visualIcon"))
        {
            selected = selected.parent;
        }

        if(selected != null && selected.name.Equals("visualIconContainer"))
        {
            item = backpack.inventories.grid[(int)Math.Round((-backpackHolder.worldBound.position.y + selected.worldBound.position.y)/100), (int)Math.Round((-backpackHolder.worldBound.position.x + selected.worldBound.position.x)/100)];
            selected.AddToClassList("selected");
            selected.BringToFront();
            isDragging = true;
            StartCoroutine(ItemDrag());
        }
        else
        {
            Debug.Log("No pick");
        }
        }
    }

    public IEnumerator ItemDrag()
    {
        originalPosition = selected.transform.position;
        originalMousePosition = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
        while(isDragging)
        {
            if(!input._isFirePressed)
            {
                isDragging = false;
            }

            Vector2 mousePosition = Input.mousePosition;
            mousePosition.y = Screen.height - mousePosition.y;

            Vector2 change = originalMousePosition - mousePosition;

            selected.transform.position = originalPosition - change;

            yield return null;
        }
        isDragging = false;

        yield return 0;

        Vector3 finalPosition = selected.worldBound.position - backpackHolder.worldBound.position;

        Vector2 coordinates = new Vector2((int)Math.Round(finalPosition.y/100), (int)Math.Round(finalPosition.x/100));

        backpack.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, null);

        bool success = false;

        if(finalPosition.y <= backpackHolder.resolvedStyle.width && finalPosition.y >= 0 && finalPosition.x <= backpackHolder.resolvedStyle.height && finalPosition.x >= 0)
            success = backpack.inventories.CheckNull((int)coordinates.x, (int)coordinates.y, item.dimensions.width, item.dimensions.height);

        if(success)
        {
            backpack.inventories.itemList.Remove(item);
            Dimensions location = new Dimensions {height = item.location.height, width = item.location.width};
            item.location = new Dimensions {height = (int)Math.Round(finalPosition.y/100), width = (int)Math.Round(finalPosition.x/100)};
            backpack.inventories.AddToGrid(location.height, location.width, item.dimensions.width, item.dimensions.height, null);
            backpack.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, item);
            backpack.inventories.itemList.Add(item);
        }
        else
        {
            backpack.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, item);
            selected.transform.position = originalPosition;
        }

        selected.RemoveFromClassList("selected");

        LoadBackpackInventoryItems();

        selected = null;

        yield return null;
    }
}
