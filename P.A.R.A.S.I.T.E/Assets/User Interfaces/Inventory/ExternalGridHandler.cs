using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ExternalGridHandler : MonoBehaviour
{
    public String assetPath = "Assets/Scriptable Objetcs/Input.asset";
    private SO_Input input;

    public Rect inventoryBounds;

    public SO_Container container;

    public TemplateContainer template;
    private VisualElement root;
    private VisualElement gridHolder;
    
    private Dimensions slotDimensions = new Dimensions { width = 100, height = 100 };

    void Start()
    {
        root = template;
        gridHolder = root.Q<VisualElement>("inventory");
        root.visible = false;
        input = (SO_Input)AssetDatabase.LoadAssetAtPath(assetPath, typeof(SO_Input));
    }

    private void RemoveInventoryChildren()
    {
        gridHolder.Clear();
    }

    private void LoadInventory()
    {
        RemoveInventoryChildren();

        GridInventory inventory = container.inventory;
        int width = inventory.dimensions.width;
        int height = inventory.dimensions.height;

        gridHolder.style.maxWidth = slotDimensions.width * width + 5;
        gridHolder.style.maxHeight = slotDimensions.height * height + 5;

        for (int i = 0; i < height * width; i++)
        {
            VisualElement slot = new VisualElement();
            slot.AddToClassList("item-slot");
            gridHolder.Add(slot);
        }
    }

    public void LoadInventoryItems()
    {
        LoadInventory();

        foreach (SO_Item item in container.inventory.itemList)
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

            gridHolder.Add(visualIconContainer);
            visualIconContainer.Add(visualIcon);
        }
    }

    private VisualElement selected;
    private bool isDragging = false;

    private Vector2 originalPosition;
    private Vector2 originalMousePosition;

    public Vector2 layout;

    private SO_Item item;

    public void SelectItem()
    {
        if (!isDragging)
        {
            inventoryBounds = gridHolder.worldBound;

            Vector2 mousePosition = Input.mousePosition;
            mousePosition.y = Screen.height - mousePosition.y;

            IPanel panel = root.panel;
            Vector2 position = RuntimePanelUtils.ScreenToPanel(panel, mousePosition);

            selected = panel.Pick(position);

            if (selected != null && selected.name.Equals("visualIcon"))
            {
                selected = selected.parent;
            }

            if (selected != null && selected.name.Equals("visualIconContainer"))
            {
                item = container.inventory.grid[(int)Math.Round((-gridHolder.worldBound.position.y + selected.worldBound.position.y) / 100), (int)Math.Round((-gridHolder.worldBound.position.x + selected.worldBound.position.x) / 100)];
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

    private IEnumerator ItemDrag()
    {
        originalPosition = selected.transform.position;
        originalMousePosition = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
        while (isDragging)
        {
            if (!input._isFirePressed)
            {
                isDragging = false;
            }

            Vector2 mousePosition = Input.mousePosition;
            mousePosition.y = Screen.height - mousePosition.y;

            Vector2 change = originalMousePosition - mousePosition;

            selected.transform.position = originalPosition - change;

            // RotateItem();

            inventoryBounds = gridHolder.worldBound;

            yield return null;
        }
        isDragging = false;

        yield return 0;

        inventoryBounds = gridHolder.worldBound;
        
        if(inventoryBounds.Contains(selected.worldBound.center))
        {
            Vector3 finalPosition = selected.worldBound.position - gridHolder.worldBound.position;

            Vector2 coordinates = new Vector2((int)Math.Round(finalPosition.y / 100), (int)Math.Round(finalPosition.x / 100));

            container.inventory.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, null);

            bool success = false;

            if (finalPosition.y <= gridHolder.resolvedStyle.width && finalPosition.y >= 0 && finalPosition.x <= gridHolder.resolvedStyle.height && finalPosition.x >= 0)
                success = container.inventory.CheckNull((int)coordinates.x, (int)coordinates.y, item.dimensions.width, item.dimensions.height);

            if (success)
            {
                container.inventory.itemList.Remove(item);
                Dimensions location = new Dimensions { height = item.location.height, width = item.location.width };
                item.location = new Dimensions { height = (int)Math.Round(finalPosition.y / 100), width = (int)Math.Round(finalPosition.x / 100) };
                container.inventory.AddToGrid(location.height, location.width, item.dimensions.width, item.dimensions.height, null);
                container.inventory.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, item);
                container.inventory.itemList.Add(item);
            }
            else
            {
                container.inventory.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, item);
                selected.transform.position = originalPosition;
            }
        }
        else 
        {
            selected.transform.position = originalPosition;    
        }

        selected.RemoveFromClassList("selected");

        LoadInventoryItems();

        selected = null;

        yield return null;
    }
}
