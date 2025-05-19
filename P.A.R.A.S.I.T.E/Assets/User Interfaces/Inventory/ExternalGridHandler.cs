using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ExternalGridHandler : MonoBehaviour
{
    [SerializeField]
    private SO_Input input;
    private PlayerStats stats;
    private PlayerAim mouseAim;

    public bool visible;

    [SerializeField]
    public SO_Container container;
    [SerializeField]
    private SO_Backpack backpack;
    [SerializeField]
    private SO_Rig rig;

    private VisualElement root;
    private VisualElement backpackHolder;
    private VisualElement rigHolder;
    private VisualElement containerHolder;

    private VisualElement itemImageHolder;
    private VisualElement itemStatsHolder;
    private Label itemDescriptionLabel;

    private Dimensions slotDimensions = new Dimensions { width = 75, height = 75 };

    void Start()
    {
        stats = gameObject.transform.parent.GetComponent<PlayerStats>();
        mouseAim = gameObject.transform.parent.GetComponent<PlayerAim>();
        root = GetComponent<UIDocument>().rootVisualElement;
        backpackHolder = root.Q<VisualElement>("backpack");
        rigHolder = root.Q<VisualElement>("rig");
        containerHolder = root.Q<VisualElement>("container-items");
        itemImageHolder = root.Q<VisualElement>("item-image");
        itemStatsHolder = root.Q<VisualElement>("item-stats");
        itemDescriptionLabel = root.Q<Label>("item-description-label");
        root.visible = false;
    }

    void Update()
    {
        root.visible = visible;
        if (!visible)
        {
            passiveItem = null;
        }
    }

    private void RemoveContainerChildren()
    {
        containerHolder.Clear();
    }

    private void LoadContainer()
    {
        RemoveContainerChildren();

        GridInventory inventory = container.inventory;
        int width = inventory.dimensions.width;
        int height = inventory.dimensions.height;

        containerHolder.style.maxWidth = slotDimensions.width * width + 5;
        containerHolder.style.maxHeight = slotDimensions.height * height + 5;

        for (int i = 0; i < height * width; i++)
        {
            VisualElement slot = new VisualElement();
            slot.AddToClassList("item-slot");
            containerHolder.Add(slot);
        }
    }

    public void LoadContainerItems()
    {
        LoadContainer();

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

            containerHolder.Add(visualIconContainer);
            visualIconContainer.Add(visualIcon);

            if (item.type == SO_Item.Type.Ammo)
            {
                Label visualIconNumbers = new Label();
                visualIconNumbers.AddToClassList("visual-icon-numbers");
                visualIconNumbers.text = ((SO_Ammo)item).currentStack.ToString();
                visualIconNumbers.name = "visualIconNumbers";
                visualIconContainer.Add(visualIconNumbers);
                visualIconNumbers.BringToFront();
            }
            else if (item.type == SO_Item.Type.Magazine)
            {
                Label visualIconNumbers = new Label();
                visualIconNumbers.AddToClassList("visual-icon-numbers");
                visualIconNumbers.text = ((SO_Magazine)item).currentAmmo.ToString() + "/" + ((SO_Magazine)item).itemStats.GetByName("Max Ammo").statValue.ToString();
                visualIconNumbers.name = "visualIconNumbers";
                visualIconContainer.Add(visualIconNumbers);
                visualIconNumbers.BringToFront();
            }
            else if (item.hasDurability)
            {
                Label visualIconNumbers = new Label();
                visualIconNumbers.AddToClassList("visual-icon-numbers");
                visualIconNumbers.text = item.currentDurability.ToString() + "/" + item.maxDurability.ToString();
                visualIconNumbers.name = "visualIconNumbers";
                visualIconContainer.Add(visualIconNumbers);
                visualIconNumbers.BringToFront();
            }
        }
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

            if (item.type == SO_Item.Type.Ammo)
            {
                Label visualIconNumbers = new Label();
                visualIconNumbers.AddToClassList("visual-icon-numbers");
                visualIconNumbers.text = ((SO_Ammo)item).currentStack.ToString();
                visualIconNumbers.name = "visualIconNumbers";
                visualIconContainer.Add(visualIconNumbers);
                visualIconNumbers.BringToFront();
            }
            else if (item.type == SO_Item.Type.Magazine)
            {
                Label visualIconNumbers = new Label();
                visualIconNumbers.AddToClassList("visual-icon-numbers");
                visualIconNumbers.text = ((SO_Magazine)item).currentAmmo.ToString() + "/" + ((SO_Magazine)item).itemStats.GetByName("Max Ammo").statValue.ToString();
                visualIconNumbers.name = "visualIconNumbers";
                visualIconContainer.Add(visualIconNumbers);
                visualIconNumbers.BringToFront();
            }
            else if (item.hasDurability)
            {
                Label visualIconNumbers = new Label();
                visualIconNumbers.AddToClassList("visual-icon-numbers");
                visualIconNumbers.text = item.currentDurability.ToString() + "/" + item.maxDurability.ToString();
                visualIconNumbers.name = "visualIconNumbers";
                visualIconContainer.Add(visualIconNumbers);
                visualIconNumbers.BringToFront();
            }
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

            if (item.type == SO_Item.Type.Ammo)
            {
                Label visualIconNumbers = new Label();
                visualIconNumbers.AddToClassList("visual-icon-numbers");
                visualIconNumbers.text = ((SO_Ammo)item).currentStack.ToString();
                visualIconNumbers.name = "visualIconNumbers";
                visualIconContainer.Add(visualIconNumbers);
                visualIconNumbers.BringToFront();
            }
            else if (item.type == SO_Item.Type.Magazine)
            {
                Label visualIconNumbers = new Label();
                visualIconNumbers.AddToClassList("visual-icon-numbers");
                visualIconNumbers.text = ((SO_Magazine)item).currentAmmo.ToString() + "/" + ((SO_Magazine)item).itemStats.GetByName("Max Ammo").statValue.ToString();
                visualIconNumbers.name = "visualIconNumbers";
                visualIconContainer.Add(visualIconNumbers);
                visualIconNumbers.BringToFront();
            }
            else if (item.hasDurability)
            {
                Label visualIconNumbers = new Label();
                visualIconNumbers.AddToClassList("visual-icon-numbers");
                visualIconNumbers.text = item.currentDurability.ToString() + "/" + item.maxDurability.ToString();
                visualIconNumbers.name = "visualIconNumbers";
                visualIconContainer.Add(visualIconNumbers);
                visualIconNumbers.BringToFront();
            }
        }
    }

    private VisualElement selected;
    private bool isDragging = false;

    private Vector2 originalPosition;
    private Vector2 originalMousePosition;

    public Rect containerBounds;
    private Rect backpackBounds;
    private Rect rigBounds;

    public Vector2 layout;

    private SO_Item item;

    private enum Selection { Container, Rig, Backpack, None };
    private Selection selection = Selection.None;

    public void SelectItem()
    {
        if (!isDragging)
        {
            containerBounds = containerHolder.worldBound;
            backpackBounds = backpackHolder.worldBound;
            rigBounds = rigHolder.worldBound;

            Vector2 mousePosition = Input.mousePosition;
            mousePosition.y = Screen.height - mousePosition.y;

            IPanel panel = root.panel;
            Vector2 position = RuntimePanelUtils.ScreenToPanel(panel, mousePosition);

            selected = panel.Pick(position);

            if (backpackBounds.Contains(position))
                selection = Selection.Backpack;
            else if (rigBounds.Contains(position))
                selection = Selection.Rig;
            else if (containerBounds.Contains(position))
                selection = Selection.Container;

            if (selected != null && selected.name.Equals("visualIcon"))
            {
                selected = selected.parent;
            }

            if (selected != null && selected.name.Equals("visualIconContainer"))
            {
                if (selection == Selection.Backpack)
                    item = backpack.inventories.grid[(int)Math.Round((-backpackHolder.worldBound.position.y + selected.worldBound.position.y) / slotDimensions.width), (int)Math.Round((-backpackHolder.worldBound.position.x + selected.worldBound.position.x) / slotDimensions.width)];
                else if (selection == Selection.Rig)
                    item = rig.inventories.grid[(int)Math.Round((-rigHolder.worldBound.position.y + selected.worldBound.position.y) / slotDimensions.width), (int)Math.Round((-rigHolder.worldBound.position.x + selected.worldBound.position.x) / slotDimensions.width)];
                else if (selection == Selection.Container)
                    item = container.inventory.grid[(int)Math.Round((-containerHolder.worldBound.position.y + selected.worldBound.position.y) / slotDimensions.width), (int)Math.Round((-containerHolder.worldBound.position.x + selected.worldBound.position.x) / slotDimensions.width)];
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
        while (isDragging && selected != null)
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

            containerBounds = containerHolder.worldBound;

            yield return null;
        }
        isDragging = false;

        yield return 0;

        containerBounds = containerHolder.worldBound;
        backpackBounds = backpackHolder.worldBound;
        rigBounds = rigHolder.worldBound;

        if (containerBounds.Contains(selected.worldBound.center))
        {
            Vector3 finalPosition = selected.worldBound.position - containerHolder.worldBound.position;

            Vector2 coordinates = new Vector2((int)Math.Round(finalPosition.y / slotDimensions.width), (int)Math.Round(finalPosition.x / slotDimensions.width));

            if (selection == Selection.Backpack) backpack.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, null);
            if (selection == Selection.Rig) rig.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, null);
            if (selection == Selection.Container) container.inventory.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, null);

            bool success = false;

            if (finalPosition.y <= containerHolder.resolvedStyle.width && finalPosition.y >= 0 && finalPosition.x <= containerHolder.resolvedStyle.height && finalPosition.x >= 0)
                success = container.inventory.CheckNull((int)coordinates.x, (int)coordinates.y, item.dimensions.width, item.dimensions.height);

            if (success)
            {
                if (selection == Selection.Backpack) backpack.inventories.itemList.Remove(item);
                if (selection == Selection.Rig) rig.inventories.itemList.Remove(item);
                if (selection == Selection.Container) container.inventory.itemList.Remove(item);
                Dimensions location = new Dimensions { height = item.location.height, width = item.location.width };
                item.location = new Dimensions { height = (int)Math.Round(finalPosition.y / slotDimensions.width), width = (int)Math.Round(finalPosition.x / slotDimensions.width) };
                container.inventory.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, item);
                container.inventory.itemList.Add(item);
            }
            else
            {
                if (selection == Selection.Backpack) backpack.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, item);
                if (selection == Selection.Rig) rig.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, item);
                if (selection == Selection.Container) container.inventory.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, item);
                selected.transform.position = originalPosition;

                SO_Item tryStack = container.inventory.grid[(int)Math.Round(finalPosition.y / slotDimensions.width), (int)Math.Round(finalPosition.x / slotDimensions.width)];
                if (tryStack != null && tryStack.type == SO_Item.Type.Ammo && item.type == SO_Item.Type.Ammo)
                {
                    if (tryStack.Equals(item))
                    {
                        ((SO_Ammo)tryStack).currentStack += ((SO_Ammo)item).currentStack;
                        ((SO_Ammo)item).currentStack = 0;
                        if (((SO_Ammo)tryStack).currentStack > ((SO_Ammo)tryStack).maxStack)
                        {
                            ((SO_Ammo)item).currentStack = ((SO_Ammo)tryStack).currentStack - ((SO_Ammo)tryStack).maxStack;
                            ((SO_Ammo)tryStack).currentStack = ((SO_Ammo)tryStack).maxStack;
                        }

                        if (((SO_Ammo)item).currentStack == 0)
                        {
                            if (selection == Selection.Backpack) backpack.inventories.Remove(item);
                            if (selection == Selection.Rig) rig.inventories.Remove(item);
                            if (selection == Selection.Container) container.inventory.Remove(item);
                        }
                    }
                }
                if (tryStack != null && tryStack.type == SO_Item.Type.Magazine && item.type == SO_Item.Type.Ammo)
                {
                    if (((SO_Magazine)tryStack).compatibleAmmo.itemName.Equals(item.itemName))
                    {
                        ((SO_Magazine)tryStack).currentAmmo += ((SO_Ammo)item).currentStack;
                        ((SO_Ammo)item).currentStack = 0;
                        if (((SO_Magazine)tryStack).currentAmmo > ((SO_Magazine)tryStack).itemStats.GetByName("Max Ammo").statValue)
                        {
                            ((SO_Ammo)item).currentStack = ((SO_Magazine)tryStack).currentAmmo - (int)((SO_Magazine)tryStack).itemStats.GetByName("Max Ammo").statValue;
                            ((SO_Magazine)tryStack).currentAmmo = (int)((SO_Magazine)tryStack).itemStats.GetByName("Max Ammo").statValue;
                        }

                        if (((SO_Ammo)item).currentStack == 0)
                        {
                            if (selection == Selection.Backpack) backpack.inventories.Remove(item);
                            if (selection == Selection.Rig) rig.inventories.Remove(item);
                            if (selection == Selection.Container) container.inventory.Remove(item);
                        }
                    }
                }
            }
        }
        else if (backpackBounds.Contains(selected.worldBound.center))
        {
            Vector3 finalPosition = selected.worldBound.position - backpackHolder.worldBound.position;

            Vector2 coordinates = new Vector2((int)Math.Round(finalPosition.y / slotDimensions.width), (int)Math.Round(finalPosition.x / slotDimensions.width));

            if (selection == Selection.Backpack) backpack.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, null);
            if (selection == Selection.Rig) rig.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, null);
            if (selection == Selection.Container) container.inventory.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, null);

            bool success = false;

            if (finalPosition.y <= backpackHolder.resolvedStyle.width && finalPosition.y >= 0 && finalPosition.x <= backpackHolder.resolvedStyle.height && finalPosition.x >= 0)
                success = backpack.inventories.CheckNull((int)coordinates.x, (int)coordinates.y, item.dimensions.width, item.dimensions.height);

            if (success)
            {
                if (selection == Selection.Backpack) backpack.inventories.itemList.Remove(item);
                if (selection == Selection.Rig) rig.inventories.itemList.Remove(item);
                if (selection == Selection.Container) container.inventory.itemList.Remove(item);
                Dimensions location = new Dimensions { height = item.location.height, width = item.location.width };
                item.location = new Dimensions { height = (int)Math.Round(finalPosition.y / slotDimensions.width), width = (int)Math.Round(finalPosition.x / slotDimensions.width) };
                backpack.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, item);
                backpack.inventories.itemList.Add(item);
            }
            else
            {
                if (selection == Selection.Backpack) backpack.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, item);
                if (selection == Selection.Rig) rig.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, item);
                if (selection == Selection.Container) container.inventory.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, item);
                selected.transform.position = originalPosition;

                SO_Item tryStack = backpack.inventories.grid[(int)Math.Round(finalPosition.y / slotDimensions.width), (int)Math.Round(finalPosition.x / slotDimensions.width)];
                if (tryStack != null && tryStack.type == SO_Item.Type.Ammo && item.type == SO_Item.Type.Ammo)
                {
                    if (tryStack.Equals(item))
                    {
                        ((SO_Ammo)tryStack).currentStack += ((SO_Ammo)item).currentStack;
                        ((SO_Ammo)item).currentStack = 0;
                        if (((SO_Ammo)tryStack).currentStack > ((SO_Ammo)tryStack).maxStack)
                        {
                            ((SO_Ammo)item).currentStack = ((SO_Ammo)tryStack).currentStack - ((SO_Ammo)tryStack).maxStack;
                            ((SO_Ammo)tryStack).currentStack = ((SO_Ammo)tryStack).maxStack;
                        }

                        if (((SO_Ammo)item).currentStack == 0)
                        {
                            if (selection == Selection.Backpack) backpack.inventories.Remove(item);
                            if (selection == Selection.Rig) rig.inventories.Remove(item);
                            if (selection == Selection.Container) container.inventory.Remove(item);
                        }
                    }
                }
                if (tryStack != null && tryStack.type == SO_Item.Type.Magazine && item.type == SO_Item.Type.Ammo)
                {
                    if (((SO_Magazine)tryStack).compatibleAmmo.itemName.Equals(item.itemName))
                    {
                        ((SO_Magazine)tryStack).currentAmmo += ((SO_Ammo)item).currentStack;
                        ((SO_Ammo)item).currentStack = 0;
                        if (((SO_Magazine)tryStack).currentAmmo > ((SO_Magazine)tryStack).itemStats.GetByName("Max Ammo").statValue)
                        {
                            ((SO_Ammo)item).currentStack = ((SO_Magazine)tryStack).currentAmmo - (int)((SO_Magazine)tryStack).itemStats.GetByName("Max Ammo").statValue;
                            ((SO_Magazine)tryStack).currentAmmo = (int)((SO_Magazine)tryStack).itemStats.GetByName("Max Ammo").statValue;
                        }

                        if (((SO_Ammo)item).currentStack == 0)
                        {
                            if (selection == Selection.Backpack) backpack.inventories.Remove(item);
                            if (selection == Selection.Rig) rig.inventories.Remove(item);
                            if (selection == Selection.Container) container.inventory.Remove(item);
                        }
                    }
                }
            }
        }
        else if (rigBounds.Contains(selected.worldBound.center) && item.type != SO_Item.Type.Weapon)
        {
            Vector3 finalPosition = selected.worldBound.position - rigHolder.worldBound.position;

            Vector2 coordinates = new Vector2((int)Math.Round(finalPosition.y / slotDimensions.width), (int)Math.Round(finalPosition.x / slotDimensions.width));

            if (selection == Selection.Backpack) backpack.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, null);
            if (selection == Selection.Rig) rig.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, null);
            if (selection == Selection.Container) container.inventory.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, null);

            bool success = false;

            if (finalPosition.y <= rigHolder.resolvedStyle.width && finalPosition.y >= 0 && finalPosition.x <= rigHolder.resolvedStyle.height && finalPosition.x >= 0)
                success = rig.inventories.CheckNull((int)coordinates.x, (int)coordinates.y, item.dimensions.width, item.dimensions.height);

            if (success)
            {
                if (selection == Selection.Backpack) backpack.inventories.itemList.Remove(item);
                if (selection == Selection.Rig) rig.inventories.itemList.Remove(item);
                if (selection == Selection.Container) container.inventory.itemList.Remove(item);
                Dimensions location = new Dimensions { height = item.location.height, width = item.location.width };
                item.location = new Dimensions { height = (int)Math.Round(finalPosition.y / slotDimensions.width), width = (int)Math.Round(finalPosition.x / slotDimensions.width) };
                rig.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, item);
                rig.inventories.itemList.Add(item);
            }
            else
            {
                if (selection == Selection.Backpack) backpack.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, item);
                if (selection == Selection.Rig) rig.inventories.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, item);
                if (selection == Selection.Container) container.inventory.AddToGrid(item.location.height, item.location.width, item.dimensions.width, item.dimensions.height, item);
                selected.transform.position = originalPosition;

                SO_Item tryStack = rig.inventories.grid[(int)Math.Round(finalPosition.y / slotDimensions.width), (int)Math.Round(finalPosition.x / slotDimensions.width)];
                if (tryStack != null && tryStack.type == SO_Item.Type.Ammo && item.type == SO_Item.Type.Ammo)
                {
                    if (tryStack.Equals(item))
                    {
                        ((SO_Ammo)tryStack).currentStack += ((SO_Ammo)item).currentStack;
                        ((SO_Ammo)item).currentStack = 0;
                        if (((SO_Ammo)tryStack).currentStack > ((SO_Ammo)tryStack).maxStack)
                        {
                            ((SO_Ammo)item).currentStack = ((SO_Ammo)tryStack).currentStack - ((SO_Ammo)tryStack).maxStack;
                            ((SO_Ammo)tryStack).currentStack = ((SO_Ammo)tryStack).maxStack;
                        }

                        if (((SO_Ammo)item).currentStack == 0)
                        {
                            if (selection == Selection.Backpack) backpack.inventories.Remove(item);
                            if (selection == Selection.Rig) rig.inventories.Remove(item);
                            if (selection == Selection.Container) container.inventory.Remove(item);
                        }
                    }
                }
                if (tryStack != null && tryStack.type == SO_Item.Type.Magazine && item.type == SO_Item.Type.Ammo)
                {
                    if (((SO_Magazine)tryStack).compatibleAmmo.itemName.Equals(item.itemName))
                    {
                        ((SO_Magazine)tryStack).currentAmmo += ((SO_Ammo)item).currentStack;
                        ((SO_Ammo)item).currentStack = 0;
                        if (((SO_Magazine)tryStack).currentAmmo > ((SO_Magazine)tryStack).itemStats.GetByName("Max Ammo").statValue)
                        {
                            ((SO_Ammo)item).currentStack = ((SO_Magazine)tryStack).currentAmmo - (int)((SO_Magazine)tryStack).itemStats.GetByName("Max Ammo").statValue;
                            ((SO_Magazine)tryStack).currentAmmo = (int)((SO_Magazine)tryStack).itemStats.GetByName("Max Ammo").statValue;
                        }

                        if (((SO_Ammo)item).currentStack == 0)
                        {
                            if (selection == Selection.Backpack) backpack.inventories.Remove(item);
                            if (selection == Selection.Rig) rig.inventories.Remove(item);
                            if (selection == Selection.Container) container.inventory.Remove(item);
                        }
                    }
                }
            }
        }
        else
        {
            selected.transform.position = originalPosition;
        }

        selected.RemoveFromClassList("selected");

        if (stats.equipmentInventory.backpack != null) LoadBackpackInventoryItems();
        if (stats.equipmentInventory.rig != null) LoadRigInventoryItems();
        LoadContainerItems();

        selected = null;

        yield return null;
    }

    public SO_Item passiveItem;
    Selection passiveSelection;
    VisualElement passiveSelected;

    public void PassiveSelection()
    {
        if (!isDragging)
        {
            itemImageHolder.style.backgroundImage = StyleKeyword.None;
            itemDescriptionLabel.text = " ";
            itemStatsHolder.Clear();

            backpackBounds = backpackHolder.worldBound;
            rigBounds = rigHolder.worldBound;
            containerBounds = containerHolder.worldBound;

            Vector2 mousePosition = Input.mousePosition;
            mousePosition.y = Screen.height - mousePosition.y;

            IPanel panel = root.panel;
            Vector2 position = RuntimePanelUtils.ScreenToPanel(panel, mousePosition);

            passiveSelected = panel.Pick(position);

            if (backpackBounds.Contains(position))
                passiveSelection = Selection.Backpack;
            else if (rigBounds.Contains(position))
                passiveSelection = Selection.Rig;
            else if (containerBounds.Contains(position))
                passiveSelection = Selection.Container;
            else
                passiveSelection = Selection.None;

            if (passiveSelected != null && passiveSelected.name.Equals("visualIcon"))
            {
                passiveSelected = passiveSelected.parent;
            }

            if (passiveSelected != null && passiveSelected.name.Equals("visualIconContainer"))
            {
                if (passiveSelection == Selection.Backpack)
                    passiveItem = backpack.inventories.grid[(int)Math.Round((-backpackHolder.worldBound.position.y + passiveSelected.worldBound.position.y) / slotDimensions.width), (int)Math.Round((-backpackHolder.worldBound.position.x + passiveSelected.worldBound.position.x) / slotDimensions.width)];
                else if (passiveSelection == Selection.Rig)
                    passiveItem = rig.inventories.grid[(int)Math.Round((-rigHolder.worldBound.position.y + passiveSelected.worldBound.position.y) / slotDimensions.width), (int)Math.Round((-rigHolder.worldBound.position.x + passiveSelected.worldBound.position.x) / slotDimensions.width)];
                else if (passiveSelection == Selection.Container)
                    passiveItem = container.inventory.grid[(int)Math.Round((-containerHolder.worldBound.position.y + passiveSelected.worldBound.position.y) / slotDimensions.width), (int)Math.Round((-containerHolder.worldBound.position.x + passiveSelected.worldBound.position.x) / slotDimensions.width)];
                LoadItemStats();
                itemImageHolder.style.backgroundImage = passiveItem.sprite;
                itemDescriptionLabel.text = passiveItem.description;
            }
            else
            {
                Debug.Log("No pick");
            }
        }
    }

    public void LoadItemStats()
    {
        foreach (ItemStatistic stat in passiveItem.itemStats.itemStatsList)
        {
            VisualElement itemStatsBox = new VisualElement();
            itemStatsBox.AddToClassList("item-stats-box");

            Label itemStatsName = new Label();
            itemStatsName.AddToClassList("item-stats-name");

            Label itemStatsValue = new Label();
            itemStatsValue.AddToClassList("item-stats-number");

            itemStatsBox.Add(itemStatsName);
            itemStatsBox.Add(itemStatsValue);
            itemStatsName.text = stat.statName;
            itemStatsValue.text = stat.statValue.ToString();

            if (stat.statValue == -1)
            {
                itemStatsValue.text = " ";
            }

            itemStatsHolder.Add(itemStatsBox);
        }
    }
}
