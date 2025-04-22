using System.Dynamic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class PlayerStats : MobStats
{
    [SerializeField]
    [Header("Input SO")]
    private SO_Input _input;

    [Header("Equipment Inventory")]
    public EquipmentInventory equipmentInventory;
    [Header("UI's")]
    public InventoryGridHandler gridHandler;
    public HUDController playerUI;
    public ExternalGridHandler externalGridHandler;

    [HideInInspector]
    public bool changingWeapons = false;
    [HideInInspector]
    public bool reloading = false;
    public bool canToggle = true;

    void Start()
    {
        Load();
        playerUI.visible = true;
        gridHandler.visible = false;
        externalGridHandler.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(_input._isCrouchPressed)
            CrouchPressed();
        else if(_input._isSprintPressed)
            SprintPressed();
        else if(_input._isMovementPressed)
            MovementPressed();
        else if(_input._isAimPressed)
            AimPressed();
        
        UISwitch();

        if(action == Action.Inventory)
        {
            if(_input._isFirePressed)
            {
                gridHandler.SelectItem();
            }
        }
        if(action == Action.Looting)
        {
            if(_input._isFirePressed)
            {
                externalGridHandler.SelectItem();
            }
        }
    }

    protected override void Load()
    {
        base.Load();
    }

    private void MovementPressed()
    {
        speed = stats.speed;
        stance = Stance.Walking;
    }
    private void CrouchPressed()
    {
        speed = crouchSpeed;
        stance = Stance.Crouching;
    }

    private void SprintPressed()
    {
        speed = sprintSpeed;
        stance = Stance.Running;
    }

    private void AimPressed()
    {
        speed = stats.speed;
        stance = Stance.Aiming;
    }

    private void UISwitch()
    {
        if(_input._isInventoryPressed && canToggle && action == Action.None)
        {
            action = Action.Inventory;
            playerUI.visible = false;
            gridHandler.visible = true;
            externalGridHandler.visible = false;
            canToggle = false;
            gridHandler.LoadWeaponImages();
            if(equipmentInventory.backpack != null) gridHandler.LoadBackpackInventoryItems();
            if(equipmentInventory.rig != null) gridHandler.LoadRigInventoryItems();
            if(equipmentInventory.backpack != null) equipmentInventory.backpack.inventories.ExposeInventory();
        }
        if(_input._isInventoryPressed && canToggle && (action == Action.Inventory || action == Action.Looting))
        {
            action = Action.None;
            playerUI.visible = true;
            gridHandler.visible = false;
            externalGridHandler.visible = false;
            canToggle = false;
        }
        else if(!_input._isInventoryPressed)
        {
            canToggle = true;
        }
    }
}
