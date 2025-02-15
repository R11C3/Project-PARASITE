using System.Collections;
using Unity.VisualScripting;
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

    private int isWalkingHash;
    private int isRollingHash;

    private Vector2 _currentMovementInput;
    private Vector3 _currentMovement;

    private bool _isMovementPressed;
    private bool _isRollPressed;
    private bool _canRoll = true;
    private bool _rolling = false;

    void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRollingHash = Animator.StringToHash("isRolling");

        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
        playerInput.CharacterControls.Roll.started += onRoll;
        playerInput.CharacterControls.Roll.canceled += onRoll;
    }

    void onMovementInput (InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _currentMovement.x = _currentMovementInput.x;
        _currentMovement.z = _currentMovementInput.y;
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
        if(_canRoll && _isRollPressed)
        {
            StartCoroutine(roll());
        }
    }

    public IEnumerator roll()
    {
        _canRoll = false;
        _rolling = true;
        float elapsedTime = 0.0f;

        Vector3 _rollMovement = _currentMovement;
        if(_rollMovement.x == 0 && _rollMovement.z == 0)
        {
            _rollMovement.x = 1;
        }

        StartCoroutine(rollDelay());

        while (elapsedTime < _rollTime)
        {
            characterController.Move(_rollMovement * _rollSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
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
            Debug.Log("Rolling");
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
