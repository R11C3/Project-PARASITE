using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] public float _speed = 5.0f;
    [SerializeField] private float _rollSpeed = 20.0f;
    [SerializeField] public float _rollTime = 1.667f;
    [SerializeField] private float _rollDelay = 1.0f;
    [SerializeField] private float _rotationFactorPerFrame = 15.0f;
    private PlayerInput playerInput;
    private CharacterController characterController;
    private Camera mainCamera;

    public Vector3 _currentMovement;
    private Vector2 _currentMovementInput;
    private Vector3 _forward, _right, _forwardMovement, _rightMovement;
    public Quaternion _currentRotation;

    public bool _isMovementPressed;
    public bool _isRollPressed;
    public bool _rolling = false;
    private bool _canRoll = true;

    void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;

        //Binding inputs to actions
        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
        playerInput.CharacterControls.Roll.started += onRoll;
        playerInput.CharacterControls.Roll.canceled += onRoll;

        //Direction relative movement vectors
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
        _currentMovement = Vector3.Normalize(_rightMovement + _forwardMovement);
        _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
    }

    void onRoll (InputAction.CallbackContext context)
    {
        _isRollPressed = context.ReadValueAsButton();
    }

    void handleMovement()
    {
        if(!_rolling)
            characterController.Move(_currentMovement * _speed * Time.deltaTime);
    }

    void handleRoll()
    {
        if(_canRoll && _isRollPressed && _isMovementPressed)
        {
            StartCoroutine(roll());
        }
        else if (_canRoll && _isRollPressed)
        {
            StartCoroutine(standingRoll());
        }
    }

    public IEnumerator roll()
    {
        _canRoll = false;
        _rolling = true;
        float elapsedTime = 0.0f;

        Vector3 _rollMovement = _currentMovement;

        StartCoroutine(rollDelay());

        while (elapsedTime < _rollTime)
        {
            characterController.Move(_rollMovement * _rollSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _rolling = false;
    }

    public IEnumerator standingRoll()
    {
        _canRoll = false;
        _rolling = true;
        float elapsedTime = 0.0f;

        Vector3 _rollMovement;

        Vector3 flat = new Vector3(0.0f,0.0f,1.0f);
        _rollMovement = _currentRotation * flat;

        StartCoroutine(rollDelay());

        while (elapsedTime < _rollTime)
        {
            characterController.Move(_rollMovement * _rollSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _currentMovement.z = 0;
        _rolling = false;
    }

    public IEnumerator rollDelay()
    {
        yield return new WaitForSecondsRealtime(_rollDelay);
        _canRoll = true;
    }

    void handleRotation()
    {
        Vector3 _positionToLookAt;

        _positionToLookAt.x = _currentMovement.x;
        _positionToLookAt.y = 0.0f;
        _positionToLookAt.z = _currentMovement.z;

        _currentRotation = transform.rotation;;

        if (_isMovementPressed)
        {
            Quaternion _targetRotation = Quaternion.LookRotation(_positionToLookAt);
            transform.rotation = Quaternion.Slerp(_currentRotation, _targetRotation, _rotationFactorPerFrame * Time.deltaTime);
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
        handleRotation();
        handleGravity();
        handleRoll();
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
