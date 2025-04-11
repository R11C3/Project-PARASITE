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
            Debug.Log("\nPicked: " + selected.name);
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

        selected.transform.position = originalPosition;

        yield return null;
    }
}
