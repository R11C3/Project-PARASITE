using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _rollSpeed = 20.0f;
    [SerializeField] private float _rollTime = 1.667f;
    [SerializeField] private float _rollDelay = 1.0f;
    [SerializeField] private float _rotationFactorPerFrame = 15.0f;
    private PlayerInput playerInput;
    private CharacterController characterController;
    private Animator animator;
    private Camera camera;

    private int isWalkingHash;
    private int isRollingHash;

    private Vector2 _currentMovementInput;
    private Vector3 _currentMovement, _forward, _right, _forwardMovement, _rightMovement;

    private bool _isMovementPressed;
    private bool _isRollPressed;
    private bool _canRoll = true;
    private bool _rolling = false;

    void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        camera = Camera.main;

        //Animation
        isWalkingHash = Animator.StringToHash("isWalking");
        isRollingHash = Animator.StringToHash("isRolling");
        animator.SetFloat("rollSpeed", 1/_rollTime);
        animator.SetFloat("walkingSpeed", _speed - 1);

        //Binding inputs to actions
        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
        playerInput.CharacterControls.Roll.started += onRoll;
        playerInput.CharacterControls.Roll.canceled += onRoll;

        //Direction relative movement vectors
        _forward = camera.transform.forward;
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

        if(_currentMovement.x == 0 && _currentMovement.z == 0)
        {
            _currentMovement.z = 1;
        }

        Vector3 _rollMovement = _currentMovement;

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

        Quaternion _currentRotation = _currentRotation = transform.rotation;;

        if (_isMovementPressed)
        {
            Quaternion _targetRotation = Quaternion.LookRotation(_positionToLookAt);
            transform.rotation = Quaternion.Slerp(_currentRotation, _targetRotation, _rotationFactorPerFrame * Time.deltaTime);
        }
    }

    void handleAnimation()
    {
        bool _isWalking = animator.GetBool(isWalkingHash);
        bool _isRolling = animator.GetBool(isRollingHash);

        if (_isMovementPressed && !_isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }
        else if (!_isMovementPressed && _isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }
        if (_rolling && !_isRolling)
        {
            animator.SetBool(isRollingHash, true);
        }
        else if (!_rolling && _isRolling)
        {
            animator.SetBool(isRollingHash, false);
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
        handleAnimation();
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
