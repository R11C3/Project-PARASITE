using System.Collections;
using UnityEngine;

public class PlayerStats : MobStats
{
    [Header("Input SO")]
    [SerializeField]
    private SO_Input input;
    [SerializeField]
    private SO_InventoryInput inventoryInput;
    private PlayerShoot playerShoot;

    [Header("Equipment Inventory")]
    public EquipmentInventory equipmentInventory;
    [Header("UI's")]
    public InventoryGridHandler gridHandler;
    public HUDController playerUI;
    [Header("GameObjects for Backpack/Rig")]
    public GameObject rig;
    public GameObject backpack;

    [HideInInspector]
    public bool changingWeapons = false;
    public bool reloading = false;
    public bool canToggle = true;
    public bool canSprint = true;

    private bool isDragging = false;

    private Vector2 inputMovement;

    void Start()
    {
        Load();
        playerUI.visible = true;
        gridHandler.visible = false;
        canToggle = true;
        playerShoot = GetComponent<PlayerShoot>();
        equipmentInventory.backpack.InitializeInventories();
        equipmentInventory.rig.InitializeInventories();
    }

    void OnEnable()
    {
        input.MoveEvent += OnMovement;
        input.SprintEvent += OnSprint;
        input.SprintCanceledEvent += OnSprintCanceled;
        input.CrouchEvent += OnCrouch;
        input.CrouchCanceledEvent += OnCrouchCanceled;
        input.AimEvent += OnAim;
        input.AimCanceledEvent += OnAimCanceled;
        input.ReloadEvent += OnReload;
        input.InventoryEvent += OnInventory;
        input.InventoryCanceledEvent += OnInventoryCanceled;

        inventoryInput.PrimaryClickEvent += OnPrimaryClick;
        inventoryInput.PrimaryDragEvent += OnPrimaryDrag;
        inventoryInput.PrimaryDragPerformedEvent += OnPrimaryDrag;
        inventoryInput.PrimaryDragCanceledEvent += OnPrimaryDragCanceled;
    }

    void OnDisable()
    {
        input.MoveEvent -= OnMovement;
        input.SprintEvent -= OnSprint;
        input.SprintCanceledEvent -= OnSprintCanceled;
        input.CrouchEvent -= OnCrouch;
        input.CrouchCanceledEvent -= OnCrouchCanceled;
        input.AimEvent -= OnAim;
        input.AimCanceledEvent -= OnAimCanceled;
        input.ReloadEvent -= OnReload;
        input.InventoryEvent -= OnInventory;
        input.InventoryCanceledEvent -= OnInventoryCanceled;

        inventoryInput.PrimaryClickEvent -= OnPrimaryClick;
        inventoryInput.PrimaryDragEvent -= OnPrimaryDrag;
        inventoryInput.PrimaryDragPerformedEvent -= OnPrimaryDrag;
    }

    void OnMovement(Vector2 input)
    {
        inputMovement = input;
    }

    void OnSprint()
    {
        if (canSprint)
            SprintPressed();
    }

    void OnSprintCanceled()
    {
        MovementPressed();
    }

    void OnCrouch()
    {
        CrouchPressed();
    }

    void OnCrouchCanceled()
    {
        MovementPressed();
    }

    void OnAim()
    {
        AimPressed();
    }

    void OnAimCanceled()
    {
        MovementPressed();
    }

    void OnReload()
    {
        if (!reloading)
        {
            reloading = true;
            Reload();
        }
    }

    void OnPrimaryClick()
    {
        if (action == Action.Inventory)
        {
            gridHandler.PassiveSelection();
        }
    }

    void OnPrimaryDrag()
    {
        isDragging = true;
    }

    void OnPrimaryDragCanceled()
    {
        isDragging = false;
    }

    void OnInventory()
    {
        UISwitch();
    }

    void OnInventoryCanceled()
    {
        canToggle = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBodyVisuals();

        Stamina();

        if (action == Action.Inventory && isDragging)
        {
            gridHandler.SelectItem();
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

    private void UpdateBodyVisuals()
    {
        if (equipmentInventory.backpack != null)
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
        if (equipmentInventory.rig != null)
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

    private void UISwitch()
    {
        if (canToggle && action == Action.None)
        {
            action = Action.Inventory;
            playerUI.visible = false;
            gridHandler.visible = true;
            canToggle = false;
            gridHandler.LoadWeaponImages();
            if (equipmentInventory.backpack != null) gridHandler.LoadBackpackInventoryItems();
            if (equipmentInventory.rig != null) gridHandler.LoadRigInventoryItems();
            if (equipmentInventory.backpack != null) equipmentInventory.backpack.inventories.ExposeInventory();
        }
        if (canToggle && (action == Action.Inventory || action == Action.Looting))
        {
            action = Action.None;
            playerUI.visible = true;
            gridHandler.visible = false;
            canToggle = false;
            if (playerShoot.gun.gun != null)
                playerShoot.gun.gun.CalculateWeaponStats();
        }
    }

    /*
    This Section is responsible for STAMINA
    */

    private void Stamina()
    {
        if (currentStamina <= maxStamina && stance != Stance.Running)
        {
            StaminaRegen(staminaRegen);
        }
        else if (stance == Stance.Running && (inputMovement.x != 0 || inputMovement.y != 0))
        {
            StaminaDegen(staminaDegen);
        }
        if (currentStamina <= 0)
        {
            canSprint = false;
            MovementPressed();
        }
    }

    private void StaminaRegen(float amt)
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += amt * Time.deltaTime;
            if (currentStamina > 30f)
            {
                canSprint = true;
            }
        }
        if (currentStamina > maxStamina)
        {
            currentStamina = maxStamina;
        }
    }

    private void StaminaDegen(float amt)
    {
        if (currentStamina > 0)
        {
            currentStamina -= amt * Time.deltaTime;
        }
        if (currentStamina < 0)
        {
            currentStamina = 0;
        }
    }

    /*
    This section is responsible for RELOADING using the GRIDINVENTORY system
    */

    private bool Reload()
    {
        if (equipmentInventory.EquippedGun() != null && equipmentInventory.rig != null)
        {
            if (equipmentInventory.EquippedGun().stats.reloadType == ReloadType.Magazine)
            {
                SO_Magazine mag = equipmentInventory.GetMagWithMostAmmo(equipmentInventory.EquippedGun());
                Debug.Log(mag);
                if (mag != null)
                {
                    StartCoroutine(ReloadDelay());
                    equipmentInventory.rig.inventories.Remove(mag);
                    if (equipmentInventory.EquippedGun().attachments.magazine != null)
                    {
                        SO_Magazine oldMag = equipmentInventory.EquippedGun().attachments.magazine;
                        if (!equipmentInventory.rig.inventories.Add(oldMag))
                        {
                            Instantiate(oldMag.obj, transform.position, Quaternion.identity);
                        }
                    }
                    equipmentInventory.EquippedGun().attachments.magazine = mag;
                    return true;
                }
            }
            if (equipmentInventory.EquippedGun().stats.reloadType == ReloadType.Single)
            {
                StartCoroutine(SingleReloadDelay());
            }
        }
        reloading = false;
        return false;
    }

    private IEnumerator ReloadDelay()
    {
        yield return new WaitForSeconds(equipmentInventory.EquippedGun().stats.reloadTime);
        reloading = false;
    }

    private IEnumerator SingleReloadDelay()
    {
        int maxAmmo = (int)equipmentInventory.EquippedGun().attachments.magazine.itemStats.GetByName("Max Ammo").statValue;
        while (equipmentInventory.EquippedGun().attachments.magazine.currentAmmo < maxAmmo)
        {
            yield return new WaitForSeconds(equipmentInventory.EquippedGun().stats.reloadTime);
            equipmentInventory.EquippedGun().attachments.magazine.currentAmmo++;
        }
        reloading = false;
    }

    public void DropItem(SO_Item item)
    {
        Vector3 placement = transform.position + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
        GameObject droppedItem = GameObject.Instantiate(item.obj, placement, Quaternion.identity);

        droppedItem.GetComponent<PickUp>().itemData = item;
    }
}
