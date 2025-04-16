using System;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Net.NetworkInformation;
using JetBrains.Annotations;
using NUnit.Framework.Constraints;
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
    [SerializeField]
    private SO_Rig rig;

    private VisualElement root;
    private VisualElement backpackHolder;
    private VisualElement rigHolder;
    private VisualElement primaryHolder;
    private VisualElement slingHolder;
    private VisualElement holsterHolder;

    private Dimensions slotDimensions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stats = gameObject.transform.parent.GetComponent<PlayerStats>();
        mouseAim = gameObject.transform.parent.GetComponent<PlayerAim>();
        root = GetComponent<UIDocument>().rootVisualElement;
        backpackHolder = root.Q<VisualElement>("backpack");
        rigHolder = root.Q<VisualElement>("rig");
        primaryHolder = root.Q<VisualElement>("primary");
        slingHolder = root.Q<VisualElement>("sling");
        holsterHolder = root.Q<VisualElement>("holster");
        ConfigureSlotDimensions();
        root.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        root.visible = visible;
        Label inventoryHealth = root.Q<Label>("health");

        inventoryHealth.text = stats.currentHealth + " / " + stats.maxHealth;
    }

    void ConfigureSlotDimensions()
    {
        slotDimensions = new Dimensions { width = 100, height = 100 };
    }

    public void LoadWeaponImages()
    {
        primaryHolder.Clear();
        slingHolder.Clear();
        holsterHolder.Clear();
        if(stats.equipmentInventory.primary != null)
        {
            VisualElement visualIconContainer = new VisualElement();
            visualIconContainer.AddToClassList("visual-icon-container");

            VisualElement visualIcon = new VisualElement();
            visualIcon.AddToClassList("visual-icon");

            visualIconContainer.style.height = primaryHolder.style.height;
            visualIconContainer.style.width = primaryHolder.style.width;
            visualIconContainer.name = "visualIconContainer";
            visualIcon.name = "visualIcon";

            visualIconContainer.style.top = primaryHolder.style.top;
            visualIconContainer.style.left = primaryHolder.style.left;

            visualIcon.style.backgroundImage = stats.equipmentInventory.primary.sprite;

            primaryHolder.Add(visualIconContainer);
            visualIconContainer.Add(visualIcon);
        }
        if(stats.equipmentInventory.sling != null)
        {
            VisualElement visualIconContainer = new VisualElement();
            visualIconContainer.AddToClassList("visual-icon-container");

            VisualElement visualIcon = new VisualElement();
            visualIcon.AddToClassList("visual-icon");

            visualIconContainer.style.height = slingHolder.style.height;
            visualIconContainer.style.width = slingHolder.style.width;
            visualIconContainer.name = "visualIconContainer";
            visualIcon.name = "visualIcon";

            visualIconContainer.style.top = slingHolder.style.top;
            visualIconContainer.style.left = slingHolder.style.left;

            visualIcon.style.backgroundImage = stats.equipmentInventory.sling.sprite;

            slingHolder.Add(visualIconContainer);
            visualIconContainer.Add(visualIcon);
        }
        if(stats.equipmentInventory.holster != null)
        {
            VisualElement visualIconContainer = new VisualElement();
            visualIconContainer.AddToClassList("visual-icon-container");

            VisualElement visualIcon = new VisualElement();
            visualIcon.AddToClassList("visual-icon");

            visualIconContainer.style.height = holsterHolder.style.height;
            visualIconContainer.style.width = holsterHolder.style.width;
            visualIconContainer.name = "visualIconContainer";
            visualIcon.name = "visualIcon";

            visualIconContainer.style.top = holsterHolder.style.top;
            visualIconContainer.style.left = holsterHolder.style.left;

            visualIcon.style.backgroundImage = stats.equipmentInventory.holster.sprite;

            holsterHolder.Add(visualIconContainer);
            visualIconContainer.Add(visualIcon);
        }
    }

    public void ChangeToStandardSize(VisualElement element)
    {
        element.style.height = item.dimensions.height * slotDimensions.height;
        element.style.width = item.dimensions.width * slotDimensions.width;
    }

    public void LoadBackpackInventories()
    {
        RemoveBackpackItemSlots();

        backpack = stats.equipmentInventory.backpack;
        int width = backpack.inventories.dimensions.width;
        int height = backpack.inventories.dimensions.height;

        VisualElement backpackIcon = root.Q<VisualElement>("backpack-icon");
        backpackIcon.style.backgroundImage = backpack.sprite;

        backpackHolder.style.maxWidth = slotDimensions.width * width + 5;
        backpackHolder.style.maxHeight = slotDimensions.height * height + 5;

        for (int i = 0; i < height * width; i++)
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
        foreach (SO_Item item in backpack.inventories.itemList)
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

    public void LoadRigInventories()
    {
        RemoveRigItemSlots();

        rig = stats.equipmentInventory.rig;
        int width = rig.inventories.dimensions.width;
        int height = rig.inventories.dimensions.height;

        VisualElement rigIcon = root.Q<VisualElement>("rig-icon");
        rigIcon.style.backgroundImage = rig.sprite;

        rigHolder.style.maxWidth = slotDimensions.width * width + 5;
        rigHolder.style.maxHeight = slotDimensions.height * height + 5;

        for (int i = 0; i < height * width; i++)
        {
            VisualElement slot = new VisualElement();
            slot.AddToClassList("item-slot");
            rigHolder.Add(slot);
        }
    }

    public void RemoveRigItemSlots()
    {
        rigHolder.Clear();
    }

    public void LoadRigInventoryItems()
    {
        RemoveRigItemSlots();
        LoadRigInventories();
        foreach (SO_Item item in rig.inventories.itemList)
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

            rigHolder.Add(visualIconContainer);
            visualIconContainer.Add(visualIcon);
        }
    }

    private VisualElement selected;
    private bool isDragging = false;

    private Vector2 originalPosition;
    private Vector2 originalMousePosition;

    private Rect backpackBounds;
    private Rect rigBounds;
    private Rect primaryBounds;
    private Rect slingBounds;
    private Rect holsterBounds;

    public Vector2 layout;

    private SO_Item item;

    private enum Selection {Primary, Sling, Holster, Rig, Backpack, None};
    private Selection selection = Selection.None;
    public void SelectItem()
    {
        if (!isDragging)
        {
            backpackBounds = backpackHolder.worldBound;
            rigBounds = rigHolder.worldBound;
            primaryBounds = primaryHolder.worldBound;
            slingBounds = slingHolder.worldBound;
            holsterBounds = holsterHolder.worldBound;

            Vector2 mousePosition = Input.mousePosition;
            mousePosition.y = Screen.height - mousePosition.y;

            IPanel panel = root.panel;
            Vector2 position = RuntimePanelUtils.ScreenToPanel(panel, mousePosition);

            selected = panel.Pick(position);

            if(backpackBounds.Contains(position))
                selection = Selection.Backpack;
            else if(rigBounds.Contains(position))
                selection = Selection.Rig;
            else if(primaryBounds.Contains(position))
                selection = Selection.Primary;
            else if(slingBounds.Contains(position))
                selection = Selection.Sling;
            else if(holsterBounds.Contains(position))
                selection = Selection.Holster;
            else
                selection = Selection.None;

            if (selected != null && selected.name.Equals("visualIcon"))
            {
                selected = selected.parent;
            }

            if (selected != null && selected.name.Equals("visualIconContainer"))
            {
                if(selection == Selection.Backpack)
                    item = backpack.inventories.grid[(int)Math.Round((-backpackHolder.worldBound.position.y + selected.worldBound.position.y) / 100), (int)Math.Round((-backpackHolder.worldBound.position.x + selected.worldBound.position.x) / 100)];
                else if(selection == Selection.Rig)
                    item = rig.inventories.grid[(int)Math.Round((-rigHolder.worldBound.position.y + selected.worldBound.position.y) / 100), (int)Math.Round((-rigHolder.worldBound.position.x + selected.worldBound.position.x) / 100)];
                else if(selection == Selection.Primary && stats.equipmentInventory.primary != null)
                {
                    item = stats.equipmentInventory.primary;
                    ChangeToStandardSize(selected);
                }
                else if(selection == Selection.Sling && stats.equipmentInventory.sling != null)
                {
                    item = stats.equipmentInventory.sling;
                    ChangeToStandardSize(selected);
                }
                else if(selection == Selection.Holster && stats.equipmentInventory.holster != null)
                {
                    item = stats.equipmentInventory.holster;
                    ChangeToStandardSize(selected);
                }
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
        if(selection == Selection.Rig || selection == Selection.Backpack)
        {
            selected.BringToFront();
        }
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

            backpackBounds = backpackHolder.worldBound;
            rigBounds = rigHolder.worldBound;

            yield return null;
        }
        isDragging = false;

        yield return 0;

        backpackBounds = backpackHolder.worldBound;
        rigBounds = rigHolder.worldBound;
        primaryBounds = primaryHolder.worldBound;
        slingBounds = slingHolder.worldBound;
        holsterBounds = holsterHolder.worldBound;
        
        if(backpackBounds.Contains(selected.worldBound.center))
        {
            Vector3 finalPosition = selected.worldBound.position - backpackHolder.worldBound.position;

            Vector2 coordinates = new Vector2((int)Math.Round(finalPosition.y / 100), (int)Math.Round(finalPosition.x / 100));

            if(selection == Selection.Backpack) backpack.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, null);
            if(selection == Selection.Rig) rig.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, null);
            if(selection == Selection.Primary) stats.equipmentInventory.primary = null;
            if(selection == Selection.Sling) stats.equipmentInventory.sling = null;
            if(selection == Selection.Holster) stats.equipmentInventory.holster = null;

            bool success = false;

            if (finalPosition.y <= backpackHolder.resolvedStyle.width && finalPosition.y >= 0 && finalPosition.x <= backpackHolder.resolvedStyle.height && finalPosition.x >= 0)
                success = backpack.inventories.CheckNull((int)coordinates.x, (int)coordinates.y, item.dimensions.width, item.dimensions.height);

            if (success)
            {
                if(selection == Selection.Backpack) backpack.inventories.itemList.Remove(item);
                if(selection == Selection.Rig) rig.inventories.itemList.Remove(item);
                Dimensions location = new Dimensions { height = item.location.height, width = item.location.width };
                item.location = new Dimensions { height = (int)Math.Round(finalPosition.y / 100), width = (int)Math.Round(finalPosition.x / 100) };
                backpack.inventories.AddToGrid(location.height, location.width, item.dimensions.width, item.dimensions.height, null);
                backpack.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, item);
                backpack.inventories.itemList.Add(item);
            }
            else
            {
                if(selection == Selection.Backpack) backpack.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, item);
                if(selection == Selection.Rig) rig.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, item);
                if(selection == Selection.Primary) stats.equipmentInventory.primary = (SO_Gun)item;
                if(selection == Selection.Sling) stats.equipmentInventory.sling = (SO_Gun)item;
                if(selection == Selection.Holster) stats.equipmentInventory.holster = (SO_Gun)item;
                selected.transform.position = originalPosition;
            }
        }
        else if(rigBounds.Contains(selected.worldBound.center) && item.type != SO_Item.Type.Weapon)
        {
            Vector3 finalPosition = selected.worldBound.position - rigHolder.worldBound.position;

            Vector2 coordinates = new Vector2((int)Math.Round(finalPosition.y / 100), (int)Math.Round(finalPosition.x / 100));

            if(selection == Selection.Backpack) backpack.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, null);
            if(selection == Selection.Rig) rig.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, null);
            if(selection == Selection.Primary) stats.equipmentInventory.primary = null;
            if(selection == Selection.Sling) stats.equipmentInventory.sling = null;
            if(selection == Selection.Holster) stats.equipmentInventory.holster = null;

            bool success = false;

            if (finalPosition.y <= rigHolder.resolvedStyle.width && finalPosition.y >= 0 && finalPosition.x <= rigHolder.resolvedStyle.height && finalPosition.x >= 0)
                success = rig.inventories.CheckNull((int)coordinates.x, (int)coordinates.y, item.dimensions.width, item.dimensions.height);

            if (success)
            {
                if(selection == Selection.Backpack) backpack.inventories.itemList.Remove(item);
                if(selection == Selection.Rig) rig.inventories.itemList.Remove(item);
                Dimensions location = new Dimensions { height = item.location.height, width = item.location.width };
                item.location = new Dimensions { height = (int)Math.Round(finalPosition.y / 100), width = (int)Math.Round(finalPosition.x / 100) };
                rig.inventories.AddToGrid(location.height, location.width, item.dimensions.width, item.dimensions.height, null);
                rig.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, item);
                rig.inventories.itemList.Add(item);
            }
            else
            {
                if(selection == Selection.Backpack) backpack.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, item);
                if(selection == Selection.Rig) rig.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, item);
                if(selection == Selection.Primary) stats.equipmentInventory.primary = (SO_Gun)item;
                if(selection == Selection.Sling) stats.equipmentInventory.sling = (SO_Gun)item;
                if(selection == Selection.Holster) stats.equipmentInventory.holster = (SO_Gun)item;
                selected.transform.position = originalPosition;
            }
        }
        else if(primaryBounds.Contains(selected.worldBound.center))
        {
            if(item.type == SO_Item.Type.Weapon && stats.equipmentInventory.primary == null)
            {
                if(selection == Selection.Backpack) backpack.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, null);
                if(selection == Selection.Rig) rig.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, null);
                if(selection == Selection.Sling) stats.equipmentInventory.sling = null;
                if(selection == Selection.Holster) stats.equipmentInventory.holster = null;

                stats.equipmentInventory.primary = (SO_Gun)item;

                if(selection == Selection.Backpack) backpack.inventories.itemList.Remove(item);
                if(selection == Selection.Rig) rig.inventories.itemList.Remove(item);
            }
            else
            {
                selected.transform.position = originalPosition;
            }
        }
        else if(slingBounds.Contains(selected.worldBound.center))
        {
            if(item.type == SO_Item.Type.Weapon && stats.equipmentInventory.sling == null)
            {
                if(selection == Selection.Backpack) backpack.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, null);
                if(selection == Selection.Rig) rig.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, null);
                if(selection == Selection.Primary) stats.equipmentInventory.primary = null;
                if(selection == Selection.Holster) stats.equipmentInventory.holster = null;

                stats.equipmentInventory.sling = (SO_Gun)item;

                if(selection == Selection.Backpack) backpack.inventories.itemList.Remove(item);
                if(selection == Selection.Rig) rig.inventories.itemList.Remove(item);
            }
            else
            {
                selected.transform.position = originalPosition;
            }
        }
        else if(holsterBounds.Contains(selected.worldBound.center))
        {
            if(item.type == SO_Item.Type.Pistol && stats.equipmentInventory.holster == null)
            {
                if(selection == Selection.Backpack) backpack.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, null);
                if(selection == Selection.Rig) rig.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, null);
                if(selection == Selection.Primary) stats.equipmentInventory.primary = null;
                if(selection == Selection.Sling) stats.equipmentInventory.sling = null;

                stats.equipmentInventory.holster = (SO_Gun)item;

                if(selection == Selection.Backpack) backpack.inventories.itemList.Remove(item);
                if(selection == Selection.Rig) rig.inventories.itemList.Remove(item);
            }
            else
            {
                selected.transform.position = originalPosition;
            }
        }
        else 
        {
            selected.transform.position = originalPosition;    
        }

        selected.RemoveFromClassList("selected");

        if(stats.equipmentInventory.backpack != null) LoadBackpackInventoryItems();
        if(stats.equipmentInventory.rig != null) LoadRigInventoryItems();
        LoadWeaponImages();

        selected = null;

        yield return null;
    }

    private bool canRotate = true;

    public void RotateItem()
    {
        if (input._isReloadPressed && item != null && canRotate)
        {
            canRotate = false;
            int temp = item.dimensions.width;
            item.dimensions.width = item.dimensions.height;
            item.dimensions.height = temp;
            selected.style.height = item.dimensions.height * slotDimensions.height;
            selected.style.width = item.dimensions.width * slotDimensions.width;
        }
        if (!input._isReloadPressed)
        {
            canRotate = true;
        }
    }
}
