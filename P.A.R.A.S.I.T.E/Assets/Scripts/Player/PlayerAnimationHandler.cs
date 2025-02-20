using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{

    public Direction _Direction;

    private Animator animator;
    private CharacterController characterController;
    private Transform characterTransform;
    private Quaternion _currentRotation;
    private PlayerController playerController;
    private PlayerRoll playerRoll;
    private Vector3 _currentVelocity;

    private int isWalkingHash;
    private int isRollingHash;

    private bool _isWalking;
    private bool _isRolling;
    private bool _rolling;
    private bool _isMovementPressed;

    private float _MovementAngle;
    private float _linearSpeed;
    private Vector3 _currentMovement;

    public enum Direction
    {
        FORWARDS,
        LEFT,
        RIGHT,
        BACKWARDS
    };

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        characterTransform = GetComponent<Transform>();
        playerController = GetComponent<PlayerController>();
        playerRoll = GetComponent<PlayerRoll>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRollingHash = Animator.StringToHash("isRolling");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateValues();
        UpdateBools();
        GetDirection();
        AnimationSpeeds();
        HandleAnimation();
    }

    void UpdateBools()
    {
        _isWalking = animator.GetBool(isWalkingHash);
        _isRolling = animator.GetBool(isRollingHash);
        _isMovementPressed = playerController._isMovementPressed;
        _rolling = playerRoll._rolling;
    }

    void UpdateValues()
    {
        _currentRotation = characterTransform.rotation;
        _currentVelocity = characterController.velocity;
        _currentMovement = playerController._currentMovementNonNormalized;
        _linearSpeed = playerController._currentMovementNonNormalized.magnitude;
    }

    void GetDirection()
    {
        float _LookAngle = _currentRotation.eulerAngles.y;
        _MovementAngle = Mathf.Atan2(_currentVelocity.x, _currentVelocity.z) * Mathf.Rad2Deg;

        _MovementAngle = (_MovementAngle - _LookAngle + 360)%360;
        
        if (_MovementAngle > 30 && _MovementAngle < 150)
            _Direction = Direction.RIGHT;
        else if (_MovementAngle >= 150 && _MovementAngle <= 210)
            _Direction = Direction.BACKWARDS;
        else if (_MovementAngle > 210 && _MovementAngle < 330)
            _Direction = Direction.LEFT;
        else
            _Direction = Direction.FORWARDS;
    }

    void AnimationSpeeds()
    {
        animator.SetFloat("rollSpeed", 1/ playerRoll._rollTime);
        animator.SetFloat("walkingSpeed", playerController._speed - 1);
        animator.SetFloat("Angle", _MovementAngle);
        animator.SetFloat("velocityX", _currentMovement.x);
        animator.SetFloat("velocityZ", _currentMovement.z);
        animator.SetFloat("linearSpeed", _linearSpeed * 10);
    }

    void HandleAnimation()
    {
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
}
