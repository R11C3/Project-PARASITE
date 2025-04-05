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

    [Header("Weapon Inventory")]
    public WeaponInventory weaponInventory = new WeaponInventory();
    public int activeSlot = 0;
    [Header("Item Inventory")]
    public Inventory inventory = new Inventory();

    [HideInInspector]
    public bool changingWeapons = false;

    void Start()
    {
        Load();
        inventory.ClearInventory();
    }

    // Update is called once per frame
    void Update()
    {
        if(_input._isMovementPressed)
            MovementPressed();
        if(_input._isCrouchPressed)
            CrouchPressed();
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
        speed = 2.0f;
    }
    private void CrouchPressed()
    {
        speed = 1.0f;
    }
}
