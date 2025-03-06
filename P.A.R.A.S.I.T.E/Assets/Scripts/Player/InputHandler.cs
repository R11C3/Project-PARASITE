using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{

    private PlayerInput playerInput;
    private Camera mainCamera;

    public Vector2 _currentMovementInput;
    [HideInInspector] public Vector3 _forward, _right, _forwardMovement, _rightMovement, _initialMovement, _currentMovement;
    public float dampening = 5.0f;
    public bool _isMovementPressed;
    public bool _isSprintPressed;
    public bool _isRollPressed;
    public bool _isFirePressed;
    public bool _isReloadPressed;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        mainCamera = Camera.main;

        playerInput.CharacterControls.Move.started += OnMovementInput;
        playerInput.CharacterControls.Move.canceled += OnMovementInput;
        playerInput.CharacterControls.Move.performed += OnMovementInput;
        playerInput.CharacterControls.Sprint.started += OnSprintInput;
        playerInput.CharacterControls.Sprint.canceled += OnSprintInput;
        playerInput.CharacterControls.Roll.started += OnRollInput;
        playerInput.CharacterControls.Roll.canceled += OnRollInput;
        playerInput.CharacterControls.Fire.started += OnFireInput;
        playerInput.CharacterControls.Fire.canceled += OnFireInput;
        playerInput.CharacterControls.Reload.started += OnReloadInput;
        playerInput.CharacterControls.Reload.canceled += OnReloadInput;

        _forward = mainCamera.transform.forward;
        _forward.y = 0;
        _forward = Vector3.Normalize(_forward);
        _right = Quaternion.Euler(new Vector3(0,90,0)) * _forward;
    }

    void OnMovementInput (InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _rightMovement = _right * _currentMovementInput.x;
        _forwardMovement = _forward * _currentMovementInput.y;
        _initialMovement = Vector3.Normalize(_rightMovement + _forwardMovement);
        _currentMovement = Vector3.Lerp(_currentMovement, _initialMovement, dampening);
        _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
    }

    void OnSprintInput (InputAction.CallbackContext context)
    {
        _isSprintPressed = context.ReadValueAsButton();
    }

    void OnRollInput (InputAction.CallbackContext context)
    {
        _isRollPressed = context.ReadValueAsButton();
    }

    void OnFireInput (InputAction.CallbackContext context)
    {
        _isFirePressed = context.ReadValueAsButton();
    }

    void OnReloadInput (InputAction.CallbackContext context)
    {
        _isReloadPressed = context.ReadValueAsButton();
    }

    void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
}
