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
    private int isLeftStrafing;
    private int isRightStrafing;
    private int isWalkingBackwards;
    private int animationIsMovementPressed;

    private bool _isWalking;
    private bool _isLeftStrafing;
    private bool _isRightStrafing;
    private bool _isWalkingBackwards;
    private bool _isRolling;
    private bool _rolling;
    private bool _isMovementPressed;

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
        isLeftStrafing = Animator.StringToHash("isLeftStrafing");
        isRightStrafing = Animator.StringToHash("isRightStrafing");
        isWalkingBackwards = Animator.StringToHash("isWalkingBackwards");
        animationIsMovementPressed = Animator.StringToHash("isMovementPressed");
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
        _isLeftStrafing = animator.GetBool(isLeftStrafing);
        _isRightStrafing = animator.GetBool(isRightStrafing);
        _isWalkingBackwards = animator.GetBool(isWalkingBackwards);
        _isRolling = animator.GetBool(isRollingHash);
        _isMovementPressed = playerController._isMovementPressed;
        _rolling = playerRoll._rolling;
        animator.SetBool(animationIsMovementPressed, true);
    }

    void UpdateValues()
    {
        _currentRotation = characterTransform.rotation;
        _currentVelocity = characterController.velocity;
    }

    void GetDirection()
    {
        float _MovementAngle = Mathf.Atan2(_currentVelocity.x, _currentVelocity.z) * Mathf.Rad2Deg;
        float _LookAngle = _currentRotation.eulerAngles.y;

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
    }

    void HandleAnimation()
    {
        if (_isMovementPressed && !_isWalking && _Direction == Direction.FORWARDS)
        {
            animator.SetBool(isWalkingHash, true);
            animator.SetBool(isLeftStrafing, false);
            animator.SetBool(isRightStrafing, false);
            animator.SetBool(isWalkingBackwards, false);
        }
        else if (_isMovementPressed && _Direction == Direction.LEFT)
        {
            animator.SetBool(isWalkingHash, false);
            animator.SetBool(isLeftStrafing, true);
            animator.SetBool(isRightStrafing, false);
            animator.SetBool(isWalkingBackwards, false);
        }
        else if (_isMovementPressed && _Direction == Direction.RIGHT)
        {
            animator.SetBool(isWalkingHash, false);
            animator.SetBool(isLeftStrafing, false);
            animator.SetBool(isRightStrafing, true);
            animator.SetBool(isWalkingBackwards, false);
        }
        else if (_isMovementPressed && _Direction == Direction.BACKWARDS)
        {
            animator.SetBool(isWalkingHash, false);
            animator.SetBool(isLeftStrafing, false);
            animator.SetBool(isRightStrafing, false);
            animator.SetBool(isWalkingBackwards, true);
        }
        else if (!_isMovementPressed && (_isWalking || _isLeftStrafing || _isRightStrafing || _isWalkingBackwards))
        {
            animator.SetBool(isWalkingHash, false);
            animator.SetBool(isLeftStrafing, false);
            animator.SetBool(isRightStrafing, false);
            animator.SetBool(isWalkingBackwards, false);
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
