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
    private InputAction _rollAction;
    private InputAction _crouchAction;
    private InputAction _fireAction;
    private InputAction _fireModeAction;
    private InputAction _reloadAction;
    private InputAction _interactAction;
    private InputAction _oneAction;
    private InputAction _twoAction;

    [Header("Current Movement")]
    public Vector3 _currentMovement;
    public Vector2 _currentMovementInput;

    [Header("Booleans")]
    public bool _isMovementPressed;
    public bool _isSprintPressed;
    public bool _isCrouchPressed;
    public bool _isRollPressed;
    public bool _isFirePressed;
    public bool _isFireModePressed;
    public bool _isReloadPressed;
    public bool _isOnePressed;
    public bool _isTwoPressed;
    public bool _isInteractPressed;

    public event UnityAction<Vector2> MoveEvent;
    public event UnityAction SprintEvent;
    public event UnityAction RollEvent;
    public event UnityAction CrouchEvent;
    public event UnityAction FireEvent;
    public event UnityAction FireModeEvent;
    public event UnityAction ReloadEvent;
    public event UnityAction OneEvent;
    public event UnityAction TwoEvent;
    public event UnityAction InteractEvent;

    public event UnityAction SprintCanceledEvent;
    public event UnityAction RollCanceledEvent;
    public event UnityAction CrouchCanceledEvent;
    public event UnityAction FireCanceledEvent;
    public event UnityAction FireModeCanceledEvent;
    public event UnityAction ReloadCanceledEvent;
    public event UnityAction OneCanceledEvent;
    public event UnityAction TwoCanceledEvent;
    public event UnityAction InteractCanceledEvent;
    
    private InputAction _dropAction;
    public event UnityAction DropEvent;
    public event UnityAction DropCanceledEvent;
    public bool _isDropPressed;

    void OnEnable()
    {
        _moveAction = _input.FindAction("Move");
        _sprintAction = _input.FindAction("Sprint");
        _rollAction = _input.FindAction("Roll");
        _crouchAction = _input.FindAction("Crouch");
        _fireAction = _input.FindAction("Fire");
        _fireModeAction = _input.FindAction("FireMode");
        _reloadAction = _input.FindAction("Reload");
        _oneAction = _input.FindAction("WeaponSlotOne");
        _twoAction = _input.FindAction("WeaponSlotTwo");
        _interactAction = _input.FindAction("Interact");

        _moveAction.started += OnMovementInput;
        _moveAction.performed += OnMovementInput;
        _moveAction.canceled += OnMovementInput;

        _sprintAction.started += OnSprintInput;
        _sprintAction.canceled += OnSprintInput;

        _rollAction.started += OnRollInput;
        _rollAction.canceled += OnRollInput;

        _crouchAction.started += OnCrouchInput;
        _crouchAction.canceled += OnCrouchInput;

        _fireAction.started += OnFireInput;
        _fireAction.canceled += OnFireInput;

        _fireModeAction.started += OnFireModeInput;
        _fireModeAction.canceled += OnFireModeInput;

        _reloadAction.started += OnReloadInput;
        _reloadAction.canceled += OnReloadInput;

        _oneAction.started += OnWeaponSlotOneInput;
        _oneAction.canceled += OnWeaponSlotOneInput;

        _twoAction.started += OnWeaponSlotTwoInput;
        _twoAction.canceled += OnWeaponSlotTwoInput;

        _interactAction.started += OnInteractInput;
        _interactAction.canceled += OnInteractInput;

        _moveAction.Enable();
        _sprintAction.Enable();
        _rollAction.Enable();
        _crouchAction.Enable();
        _fireAction.Enable();
        _fireModeAction.Enable();
        _reloadAction.Enable();
        _oneAction.Enable();
        _twoAction.Enable();
        _interactAction.Enable();

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

        _rollAction.started -= OnRollInput;
        _rollAction.canceled -= OnRollInput;

        _crouchAction.started -= OnCrouchInput;
        _crouchAction.canceled -= OnCrouchInput;

        _fireAction.started -= OnFireInput;
        _fireAction.canceled -= OnFireInput;

        _fireModeAction.started -= OnFireModeInput;
        _fireModeAction.canceled -= OnFireModeInput;

        _reloadAction.started -= OnReloadInput;
        _reloadAction.canceled -= OnReloadInput;

        _oneAction.started -= OnWeaponSlotOneInput;
        _oneAction.canceled -= OnWeaponSlotOneInput;

        _twoAction.started -= OnWeaponSlotTwoInput;
        _twoAction.canceled -= OnWeaponSlotTwoInput;

        _interactAction.started -= OnInteractInput;
        _interactAction.canceled -= OnInteractInput;

        _moveAction.Disable();
        _sprintAction.Disable();
        _rollAction.Disable();
        _crouchAction.Disable();
        _fireAction.Disable();
        _fireModeAction.Disable();
        _reloadAction.Disable();
        _oneAction.Disable();
        _twoAction.Disable();
        _interactAction.Disable();

        _dropAction.started -= OnDropInput;
        _dropAction.canceled -= OnDropInput;
        _dropAction.Disable();
    }

    void OnMovementInput (InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
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

    void OnRollInput (InputAction.CallbackContext context)
    {
        if(RollEvent != null && context.started)
        {
            RollEvent.Invoke();
        }
        if(RollCanceledEvent != null && context.canceled)
        {
            RollCanceledEvent.Invoke();
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
        if(DropEvent != null && context.canceled)
        {
            DropCanceledEvent.Invoke();
        }
    }
}
