using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SO_Input", menuName = "Scriptable Objects/Input")]
public class SO_Input : ScriptableObject
{
    [SerializeField]
    private InputActionAsset _input;

    private InputAction _moveAction;
    private InputAction _sprintAction;
    private InputAction _vaultAction;
    private InputAction _crouchAction;
    private InputAction _fireAction;
    private InputAction _aimAction;
    private InputAction _fireModeAction;
    private InputAction _reloadAction;
    private InputAction _interactAction;
    private InputAction _oneAction;
    private InputAction _twoAction;
    private InputAction _threeAction;
    private InputAction _inventoryAction;
    private InputAction _dragAction;

    [Header("Booleans")]
    public bool _isMovementPressed;

    public event UnityAction<Vector2> MoveEvent;
    public event UnityAction SprintEvent;
    public event UnityAction VaultEvent;
    public event UnityAction CrouchEvent;
    public event UnityAction FireEvent;
    public event UnityAction AimEvent;
    public event UnityAction FireModeEvent;
    public event UnityAction ReloadEvent;
    public event UnityAction OneEvent;
    public event UnityAction TwoEvent;
    public event UnityAction ThreeEvent;
    public event UnityAction InteractEvent;
    public event UnityAction InventoryEvent;
    public event UnityAction DragEvent;

    public event UnityAction SprintCanceledEvent;
    public event UnityAction VaultCanceledEvent;
    public event UnityAction CrouchCanceledEvent;
    public event UnityAction FireCanceledEvent;
    public event UnityAction AimCanceledEvent;
    public event UnityAction FireModeCanceledEvent;
    public event UnityAction ReloadCanceledEvent;
    public event UnityAction OneCanceledEvent;
    public event UnityAction TwoCanceledEvent;
    public event UnityAction ThreeCanceledEvent;
    public event UnityAction InteractCanceledEvent;
    public event UnityAction InventoryCanceledEvent;
    public event UnityAction DragCanceledEvent;
    
    private InputAction _dropAction;
    public event UnityAction DropEvent;
    public event UnityAction DropCanceledEvent;
    public bool _isDropPressed;

    void OnEnable()
    {
        _moveAction = _input.FindAction("Move");
        _sprintAction = _input.FindAction("Sprint");
        _vaultAction = _input.FindAction("Vault");
        _crouchAction = _input.FindAction("Crouch");
        _fireAction = _input.FindAction("Fire");
        _aimAction = _input.FindAction("Aim");
        _fireModeAction = _input.FindAction("FireMode");
        _reloadAction = _input.FindAction("Reload");
        _oneAction = _input.FindAction("WeaponSlotOne");
        _twoAction = _input.FindAction("WeaponSlotTwo");
        _threeAction = _input.FindAction("WeaponSlotThree");
        _interactAction = _input.FindAction("Interact");
        _inventoryAction = _input.FindAction("Inventory");
        _dragAction = _input.FindAction("ClickDrag");

        _moveAction.started += OnMovementInput;
        _moveAction.performed += OnMovementInput;
        _moveAction.canceled += OnMovementInput;

        _sprintAction.started += OnSprintInput;
        _sprintAction.canceled += OnSprintInput;

        _vaultAction.started += OnVaultInput;
        _vaultAction.canceled += OnVaultInput;

        _crouchAction.started += OnCrouchInput;
        _crouchAction.canceled += OnCrouchInput;

        _fireAction.started += OnFireInput;
        _fireAction.canceled += OnFireInput;

        _aimAction.started += OnAimInput;
        _aimAction.canceled += OnAimInput;

        _fireModeAction.started += OnFireModeInput;
        _fireModeAction.canceled += OnFireModeInput;

        _reloadAction.started += OnReloadInput;
        _reloadAction.canceled += OnReloadInput;

        _oneAction.started += OnWeaponSlotOneInput;
        _oneAction.canceled += OnWeaponSlotOneInput;

        _twoAction.started += OnWeaponSlotTwoInput;
        _twoAction.canceled += OnWeaponSlotTwoInput;

        _threeAction.started += OnWeaponSlotThreeInput;
        _threeAction.canceled += OnWeaponSlotThreeInput;

        _interactAction.started += OnInteractInput;
        _interactAction.canceled += OnInteractInput;

        _inventoryAction.started += OnInventoryInput;
        _inventoryAction.canceled += OnInventoryInput;

        _dragAction.started += OnDragInput;
        _dragAction.canceled += OnDragInput;

        _moveAction.Enable();
        _sprintAction.Enable();
        _vaultAction.Enable();
        _crouchAction.Enable();
        _fireAction.Enable();
        _aimAction.Enable();
        _fireModeAction.Enable();
        _reloadAction.Enable();
        _oneAction.Enable();
        _twoAction.Enable();
        _threeAction.Enable();
        _interactAction.Enable();
        _inventoryAction.Enable();
        _dragAction.Enable();

        _dropAction = _input.FindAction("Drop");
        _dropAction.started += OnDropInput;
        _dropAction.canceled += OnDropInput;
        _dropAction.Enable();
    }

    void OnDisable()
    {
        _moveAction.started -= OnMovementInput;
        _moveAction.performed -= OnMovementInput;
        _moveAction.canceled -= OnMovementInput;

        _sprintAction.started -= OnSprintInput;
        _sprintAction.canceled -= OnSprintInput;

        _vaultAction.started -= OnVaultInput;
        _vaultAction.canceled -= OnVaultInput;

        _crouchAction.started -= OnCrouchInput;
        _crouchAction.canceled -= OnCrouchInput;

        _fireAction.started -= OnFireInput;
        _fireAction.canceled -= OnFireInput;

        _aimAction.started -= OnAimInput;
        _aimAction.canceled -= OnAimInput;

        _fireModeAction.started -= OnFireModeInput;
        _fireModeAction.canceled -= OnFireModeInput;

        _reloadAction.started -= OnReloadInput;
        _reloadAction.canceled -= OnReloadInput;

        _oneAction.started -= OnWeaponSlotOneInput;
        _oneAction.canceled -= OnWeaponSlotOneInput;

        _twoAction.started -= OnWeaponSlotTwoInput;
        _twoAction.canceled -= OnWeaponSlotTwoInput;

        _threeAction.started -= OnWeaponSlotThreeInput;
        _threeAction.canceled -= OnWeaponSlotThreeInput;

        _interactAction.started -= OnInteractInput;
        _interactAction.canceled -= OnInteractInput;

        _inventoryAction.started -= OnInventoryInput;
        _inventoryAction.canceled -= OnInventoryInput;

        _dragAction.started -= OnDragInput;
        _dragAction.canceled -= OnDragInput;

        _moveAction.Disable();
        _sprintAction.Disable();
        _vaultAction.Disable();
        _crouchAction.Disable();
        _fireAction.Disable();
        _aimAction.Disable();
        _fireModeAction.Disable();
        _reloadAction.Disable();
        _oneAction.Disable();
        _twoAction.Disable();
        _threeAction.Disable();
        _interactAction.Disable();
        _inventoryAction.Disable();
        _dragAction.Disable();

        _dropAction.started -= OnDropInput;
        _dropAction.canceled -= OnDropInput;
        _dropAction.Disable();
    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        if (MoveEvent != null && context.started)
        {
            MoveEvent.Invoke(context.ReadValue<Vector2>());
        }
        if (MoveEvent != null && context.performed)
        {
            MoveEvent.Invoke(context.ReadValue<Vector2>());
        }
        if (MoveEvent != null && context.canceled)
        {
            MoveEvent.Invoke(context.ReadValue<Vector2>());
        }
    }

    void OnSprintInput (InputAction.CallbackContext context)
    {
        if(SprintEvent != null && context.started)
        {
            SprintEvent.Invoke();
        }
        if(SprintCanceledEvent != null && context.canceled)
        {
            SprintCanceledEvent.Invoke();
        }
    }

    void OnCrouchInput (InputAction.CallbackContext context)
    {
        if(CrouchEvent != null && context.started)
        {
            CrouchEvent.Invoke();
        }
        if(CrouchCanceledEvent != null && context.canceled)
        {
            CrouchCanceledEvent.Invoke();
        }
    }

    void OnVaultInput (InputAction.CallbackContext context)
    {
        if(VaultEvent != null && context.started)
        {
            VaultEvent.Invoke();
        }
        if(VaultCanceledEvent != null && context.canceled)
        {
            VaultCanceledEvent.Invoke();
        }
    }

    void OnFireInput (InputAction.CallbackContext context)
    {
        if(FireEvent != null && context.started)
        {
            FireEvent.Invoke();
        }
        if(FireCanceledEvent != null && context.canceled)
        {
            FireCanceledEvent.Invoke();
        }
    }

    void OnAimInput (InputAction.CallbackContext context)
    {
        if(AimEvent != null && context.started)
        {
            AimEvent.Invoke();
        }
        if(AimEvent != null && context.canceled)
        {
            AimCanceledEvent.Invoke();
        }
    }

    void OnFireModeInput (InputAction.CallbackContext context)
    {
        if(FireModeEvent != null && context.started)
        {
            FireModeEvent.Invoke();
        }
        if(FireCanceledEvent != null && context.canceled)
        {
            FireModeCanceledEvent.Invoke();
        }
    }

    void OnReloadInput (InputAction.CallbackContext context)
    {
        if(ReloadEvent != null && context.started)
        {
            ReloadEvent.Invoke();
        }
        if(ReloadCanceledEvent != null && context.canceled)
        {
            ReloadCanceledEvent.Invoke();
        }
    }

    void OnWeaponSlotOneInput(InputAction.CallbackContext context)
    {
        if(OneEvent != null && context.started)
        {
            OneEvent.Invoke();
        }
        if(OneCanceledEvent != null && context.canceled)
        {
            OneCanceledEvent.Invoke();
        }
    }

    void OnWeaponSlotTwoInput(InputAction.CallbackContext context)
    {
        if(TwoEvent != null && context.started)
        {
            TwoEvent.Invoke();
        }
        if(TwoCanceledEvent != null && context.canceled)
        {
            TwoCanceledEvent.Invoke();
        }
    }

    void OnWeaponSlotThreeInput(InputAction.CallbackContext context)
    {
        if(ThreeEvent != null && context.started)
        {
            ThreeEvent.Invoke();
        }
        if(ThreeCanceledEvent != null && context.canceled)
        {
            ThreeCanceledEvent.Invoke();
        }
    }

    void OnInteractInput(InputAction.CallbackContext context)
    {
        if(InteractEvent != null && context.started)
        {
            InteractEvent.Invoke();
        }
        if(InteractEvent != null && context.canceled)
        {
            InteractCanceledEvent.Invoke();
        }
    }

    void OnDropInput(InputAction.CallbackContext context)
    {
        if(DropEvent != null && context.started)
        {
            DropEvent.Invoke();
        }
        if(DropCanceledEvent != null && context.canceled)
        {
            DropCanceledEvent.Invoke();
        }
    }

    void OnInventoryInput(InputAction.CallbackContext context)
    {
        if(InventoryEvent != null && context.started)
        {
            InventoryEvent.Invoke();
        }
        if(InventoryCanceledEvent != null && context.canceled)
        {
            InventoryCanceledEvent.Invoke();
        }
    }

    void OnDragInput(InputAction.CallbackContext context)
    {
        if(DragEvent != null && context.started)
        {
            DragEvent.Invoke();
        }
        if(DragCanceledEvent != null && context.canceled)
        {
            DragCanceledEvent.Invoke();
        }
    }
}
