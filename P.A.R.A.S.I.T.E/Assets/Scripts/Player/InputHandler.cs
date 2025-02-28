using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{

    private PlayerInput playerInput;
    [SerializeField]
    private Camera mainCamera;

    public Vector2 _currentMovementInput;
    [HideInInspector] public Vector3 _forward, _right, _forwardMovement, _rightMovement, _initialMovement, _currentMovement;
    public float dampening = 5.0f;
    public bool _isMovementPressed;
    public bool _isSprintPressed;
    public bool _isRollPressed;
    public bool _isFirePressed;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();

        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
        playerInput.CharacterControls.Sprint.started += onSprintInput;
        playerInput.CharacterControls.Sprint.canceled += onSprintInput;
        playerInput.CharacterControls.Roll.started += onRollInput;
        playerInput.CharacterControls.Roll.canceled += onRollInput;
        playerInput.CharacterControls.Fire.started += onFireInput;
        playerInput.CharacterControls.Fire.canceled += onFireInput;

        _forward = mainCamera.transform.forward;
        _forward.y = 0;
        _forward = Vector3.Normalize(_forward);
        _right = Quaternion.Euler(new Vector3(0,90,0)) * _forward;
    }

    void onMovementInput (InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _rightMovement = _right * _currentMovementInput.x;
        _forwardMovement = _forward * _currentMovementInput.y;
        _initialMovement = Vector3.Normalize(_rightMovement + _forwardMovement);
        _currentMovement = Vector3.Lerp(_currentMovement, _initialMovement, dampening);
        _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
    }

    void onSprintInput (InputAction.CallbackContext context)
    {
        _isSprintPressed = context.ReadValueAsButton();
    }

    void onRollInput (InputAction.CallbackContext context)
    {
        _isRollPressed = context.ReadValueAsButton();
    }

    void onFireInput (InputAction.CallbackContext context)
    {
        _isFirePressed = context.ReadValueAsButton();
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
