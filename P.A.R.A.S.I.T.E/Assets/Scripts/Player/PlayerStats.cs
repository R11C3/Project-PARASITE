using System.Dynamic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class PlayerStats : MobStats
{
    [SerializeField]
    [Header("Input SO")]
    private SO_Input _input;

    [Header("Roll Stats")]
    public float rollTime;
    public float rollSpeed;
    public float rollDelay;

    [Header("Equipment Inventory")]
    public EquipmentInventory equipmentInventory;
    [Header("UI's")]
    public InventoryGridHandler gridHandler;
    public HUDController playerUI;

    [HideInInspector]
    public bool changingWeapons = false;
    [HideInInspector]
    public bool reloading = false;
    private bool canToggle = true;

    void Start()
    {
        Load();
        playerUI.visible = true;
        gridHandler.visible = false;
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
        
        UISwitch();
    }

    protected override void Load()
    {
        rollTime = stats.rollTime;
        rollSpeed = stats.rollSpeed;
        rollDelay = stats.rollDelay;
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

    private void UISwitch()
    {
        if(_input._isInventoryPressed && canToggle)
        {
            playerUI.visible = !playerUI.visible;
            gridHandler.visible = !gridHandler.visible;
            canToggle = false;
            gridHandler.LoadBackpackInventoryItems();
        }
        else if(!_input._isInventoryPressed)
        {
            canToggle = true;
        }
    }
}
