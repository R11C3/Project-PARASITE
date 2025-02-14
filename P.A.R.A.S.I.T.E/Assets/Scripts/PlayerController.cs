using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _dashDistance = 2.0f;
    [SerializeField] private float _dashDelay = 1.0f;
    [SerializeField] private float _rotationFactorPerFrame = 15.0f;
    private PlayerInput playerInput;
    private CharacterController characterController;
    private Animator animator;

    private int isWalkingHash;

    private Vector2 _currentMovementInput;
    private Vector3 _currentMovement;
    private Vector3 _dashDirection;

    private bool _isMovementPressed;
    private bool _isDashPressed;
    private bool _canDash = true;

    void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");

        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
        playerInput.CharacterControls.Dash.started += onDash;
        playerInput.CharacterControls.Dash.canceled += onDash;
    }

    void onMovementInput (InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _currentMovement.x = _currentMovementInput.x;
        _currentMovement.z = _currentMovementInput.y;
        _isMovementPressed = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
    }

    void onDash (InputAction.CallbackContext context)
    {
        _isDashPressed = context.ReadValueAsButton();
    }

    void handleMovement()
    {
        characterController.Move(_currentMovement * _speed * Time.deltaTime);
    }

    void handleDash()
    {
        if (_isDashPressed && _canDash)
        {
            _canDash = false;
            _dashDirection.x = _currentMovement.x;
            _dashDirection.z = _currentMovement.z;
            characterController.Move(_dashDirection * _dashDistance * Time.deltaTime);
            StartCoroutine(dashDelay());
        }
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

        if (_isMovementPressed && !_isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }
        else if (!_isMovementPressed && _isWalking)
        {
            animator.SetBool(isWalkingHash, false);
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
        handleDash();
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

    IEnumerator dashDelay()
    {
        yield return new WaitForSeconds(_dashDelay);
        _canDash = true;
    }

}
