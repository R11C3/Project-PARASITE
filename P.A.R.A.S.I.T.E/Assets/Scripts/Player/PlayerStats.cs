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
    [Header("GameObjects for Backpack/Rig")]
    public GameObject rig;
    public GameObject backpack;

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
        UpdateBodyVisuals();

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
                gridHandler.PassiveSelection();
            }
            if(_input._isDragPressed)
            {
                gridHandler.SelectItem();
            }
            gridHandler.LoadItemInfo();
        }
        if(action == Action.Looting)
        {
            if(_input._isFirePressed)
            {
                externalGridHandler.PassiveSelection();
            }
            if(_input._isDragPressed)
            {
                externalGridHandler.SelectItem();
            }
            externalGridHandler.LoadItemInfo();
        }
    }

    protected override void Load()
    {
        base.Load();
    }

    private void MovementPressed()
    {
        speed = ((SO_Mob)stats).speed;
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
        speed = ((SO_Mob)stats).speed;
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

    private void UpdateBodyVisuals()
    {
        if(equipmentInventory.backpack != null)
        {
            backpack.GetComponent<MeshFilter>().mesh = equipmentInventory.backpack.mesh;
            backpack.GetComponent<MeshRenderer>().material = equipmentInventory.backpack.material;
            backpack.GetComponent<Transform>().localScale = equipmentInventory.backpack.scale;
            backpack.GetComponent<Transform>().localEulerAngles = equipmentInventory.backpack.rotation;
        }
        else
        {
            backpack.GetComponent<MeshFilter>().mesh = null;
            backpack.GetComponent<MeshRenderer>().material = null;
        }
        if(equipmentInventory.rig != null)
        {
            rig.GetComponent<MeshFilter>().mesh = equipmentInventory.rig.mesh;
            rig.GetComponent<MeshRenderer>().material = equipmentInventory.rig.material;
            rig.GetComponent<Transform>().localScale = equipmentInventory.rig.scale;
            rig.GetComponent<Transform>().localEulerAngles = equipmentInventory.rig.rotation;
        }
        else
        {
            rig.GetComponent<MeshFilter>().mesh = null;
            rig.GetComponent<MeshRenderer>().material = null;
        }
    }
}
