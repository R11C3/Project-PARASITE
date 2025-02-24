using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [HideInInspector] public float _speed = 0.0f;
    public float dampening = 5.0f;
    [SerializeField] public float _maxWalkSpeed = 2.0f;
    [SerializeField] private float _acceleration = 6.0f;
    [SerializeField] private float _deceleration = 13.0f;
    [SerializeField] private float _maxSprintSpeed = 6.5f;
    [SerializeField] private float _sprintAcceleration = 13f;
    [SerializeField] private float _sprintDeceleration = 6.5f;

    private PlayerInput playerInput;
    private CharacterController characterController;
    private Camera mainCamera;
    private PlayerRoll playerRoll;

    [HideInInspector] public Vector3 _currentMovement, _currentVelocity, _lastMovement, _initialMovement;
    [HideInInspector] public Vector3 _currentMovementNonNormalized;
    [HideInInspector] public Vector2 _currentMovementInput;
    [HideInInspector] public Vector3 _forward, _right, _forwardMovement, _rightMovement;
    [HideInInspector] public Quaternion _currentRotation;

    public bool _isMovementPressed;
    public bool _isSprintPressed;
    private bool _isRollPressed;
    private bool _rolling;

    void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        playerRoll = GetComponent<PlayerRoll>();
        mainCamera = Camera.main;

        //Binding inputs to actions
        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
        playerInput.CharacterControls.Sprint.started += onSprintInput;
        playerInput.CharacterControls.Sprint.canceled += onSprintInput;

        //Direction relative movement vectors
        _forward = mainCamera.transform.forward;
        _forward.y = 0;
        _forward = Vector3.Normalize(_forward);
        _right = Quaternion.Euler(new Vector3(0,90,0)) * _forward;

        _lastMovement = new Vector3(0.0f, -9.8f, 0.0f);
    }

    void onMovementInput (InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _currentMovementNonNormalized.x = _currentMovementInput.x;
        _currentMovementNonNormalized.z = _currentMovementInput.y;
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

    void UpdateValues()
    {
        _rolling = playerRoll._rolling;
        _isRollPressed = playerRoll._isRollPressed;
        // _speed = playerSprint._speed;
    }

    void handleMovement()
    {

        if(_isMovementPressed && _speed <= _maxWalkSpeed)
        {
            _speed += _acceleration * Time.deltaTime;
        }

        if(!_isMovementPressed && _speed > 0.0f)
        {
            _speed -= _deceleration * Time.deltaTime;
        }

        if(_speed < 0.0f)
        {
            _speed = 0.0f;
        }

        if (!_isMovementPressed)
        {
            _currentMovement = _lastMovement;
            characterController.Move(new Vector3(_currentMovement.x * _speed * Time.deltaTime, _currentMovement.y * Time.deltaTime, _currentMovement.z * _speed * Time.deltaTime));
        }

        if (!_rolling && _isMovementPressed)
        {
            characterController.Move(new Vector3(_currentMovement.x * _speed * Time.deltaTime, _currentMovement.y * Time.deltaTime, _currentMovement.z * _speed * Time.deltaTime));

            _lastMovement = _currentMovement;
        }

        _currentVelocity = characterController.velocity;
    }

    void handleSprint()
    {
        if(_isSprintPressed && _isMovementPressed && _speed <= _maxSprintSpeed)
        {
            _speed += _sprintAcceleration * Time.deltaTime;
        }

        if (!_isSprintPressed && _speed >= _maxWalkSpeed)
        {
            _speed -= _sprintDeceleration * Time.deltaTime;
        }
    }

    void handleGravity()
    {
        if (characterController.isGrounded)
        {
            float _groundedGravity = -0.5f;
            _currentMovement.y = _groundedGravity;
        }
        else
        {
            float _gravity = -9.8f;
            _currentMovement.y = _gravity;
        }
    }

    void Update()
    {
        UpdateValues();
        handleGravity();
        handleSprint();
        handleMovement();
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
