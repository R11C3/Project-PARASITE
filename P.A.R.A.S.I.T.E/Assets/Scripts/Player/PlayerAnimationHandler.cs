using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    private Animator animator;
    private Transform characterTransform;
    private Quaternion _currentRotation;
    private PlayerStats player;
    [SerializeField]
    private SO_Input _input;
    private PlayerController playerController;
    private Vector3 _currentVelocity;

    private int isWalkingHash;
    private int isCrouchingHash;
    private int isClimbingHash;

    private bool _isWalking;
    private bool _isCrouching;
    private bool _isClimbing;
    private bool _isMovementPressed;

    private float _MovementAngle;
    [SerializeField]
    private float _linearSpeed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterTransform = GetComponent<Transform>();
        playerController = GetComponent<PlayerController>();
        player = GetComponent<PlayerStats>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isCrouchingHash = Animator.StringToHash("isCrouching");
        isClimbingHash = Animator.StringToHash("isClimbing");
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
        _isCrouching = animator.GetBool(isCrouchingHash);
        _isClimbing = animator.GetBool(isClimbingHash);
    }

    void UpdateValues()
    {
        _currentRotation = characterTransform.rotation;
        _linearSpeed = playerController._speed * Time.deltaTime;
        _currentVelocity = playerController._currentVelocity;

        float _LookAngle = _currentRotation.eulerAngles.y;
        _MovementAngle = Mathf.Atan2(_currentVelocity.x, _currentVelocity.z) * Mathf.Rad2Deg;
        _MovementAngle = (_MovementAngle - _LookAngle + 360) % 360;
    }

    void AnimationSpeeds()
    {
        animator.SetFloat("walkingSpeed", playerController._speed - 1);
        animator.SetFloat("Angle", _MovementAngle);
        animator.SetFloat("linearSpeed", _linearSpeed * 286);
        animator.SetFloat("crouchingSpeed", _linearSpeed * 1429);
    }

    void HandleAnimation()
    {
        if (_input._isMovementPressed && !_isWalking && player.action == Action.None)
        {
            animator.SetBool(isWalkingHash, true);
        }
        else if (!_input._isMovementPressed && _isWalking && player.action == Action.None)
        {
            animator.SetBool(isWalkingHash, false);
        }

        if(_input._isCrouchPressed && !_isCrouching && player.action == Action.None)
        {
            animator.SetBool(isCrouchingHash, true);
        }
        else if(!_input._isCrouchPressed && _isCrouching && player.action == Action.None)
        {
            animator.SetBool(isCrouchingHash, false);
        }

        if(player.action == Action.Ladder)
        {
            animator.SetBool(isClimbingHash, true);
        }
        else
        {
            animator.SetBool(isClimbingHash, false);
        }
    }
}
