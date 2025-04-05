using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{

    public Direction _Direction;

    private Animator animator;
    private Transform characterTransform;
    private Quaternion _currentRotation;
    private PlayerStats player;
    [SerializeField]
    private SO_Input _input;
    private PlayerController playerController;
    private PlayerRoll playerRoll;
    private Vector3 _currentVelocity;

    private int isWalkingHash;
    private int isRollingHash;
    private int isCrouchingHash;

    private bool _isWalking;
    private bool _isCrouching;
    private bool _isRolling;
    private bool _rolling;
    private bool _isMovementPressed;

    private float _MovementAngle;
    private float _linearSpeed;
    
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
        characterTransform = GetComponent<Transform>();
        playerController = GetComponent<PlayerController>();
        playerRoll = GetComponent<PlayerRoll>();
        player = GetComponent<PlayerStats>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRollingHash = Animator.StringToHash("isRolling");
        isCrouchingHash = Animator.StringToHash("isCrouching");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateValues();
        UpdateBools();
        AnimationSpeeds();
        HandleAnimation();
    }

    void UpdateBools()
    {
        _isWalking = animator.GetBool(isWalkingHash);
        _isRolling = animator.GetBool(isRollingHash);
        _isCrouching = animator.GetBool(isCrouchingHash);
        _rolling = playerRoll._rolling;
    }

    void UpdateValues()
    {
        _currentRotation = characterTransform.rotation;
        _linearSpeed = playerController._speed;
        _currentVelocity = playerController._currentVelocity;

        float _LookAngle = _currentRotation.eulerAngles.y;
        _MovementAngle = Mathf.Atan2(_currentVelocity.x, _currentVelocity.z) * Mathf.Rad2Deg;
        _MovementAngle = (_MovementAngle - _LookAngle + 360) % 360;
    }

    void AnimationSpeeds()
    {
        animator.SetFloat("rollSpeed", 1/ player.rollTime);
        animator.SetFloat("walkingSpeed", playerController._speed - 1);
        animator.SetFloat("Angle", _MovementAngle);
        animator.SetFloat("linearSpeed", _linearSpeed * 10);
        animator.SetFloat("crouchingSpeed", _linearSpeed * 50);
    }

    void HandleAnimation()
    {
        if (_input._isMovementPressed && !_isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }
        else if (!_input._isMovementPressed && _isWalking)
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
        if(_input._isCrouchPressed && !_isCrouching)
        {
            animator.SetBool(isCrouchingHash, true);
        }
        else if(!_input._isCrouchPressed && _isCrouching)
        {
            animator.SetBool(isCrouchingHash, false);
        }
    }
}
