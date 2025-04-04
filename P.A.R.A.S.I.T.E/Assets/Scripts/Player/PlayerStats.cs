using System.Dynamic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class PlayerStats : MobStats
{
    [SerializeField]
    private SO_Input _input;

    [Header("Health")]

    [Header("Stats")]
    public float rollTime;
    public float rollSpeed;
    public float rollDelay;

    [Header("Weapon Inventory")]
    public WeaponInventory weaponInventory;
    public int activeSlot = 0;
    [Header("Item Inventory")]
    public Inventory inventory = new Inventory();

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
        base.Load();
        rollTime = stats.rollTime;
        rollSpeed = stats.rollSpeed;
        rollDelay = stats.rollDelay;
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
