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
    private SO_Input input;
    private PlayerController playerController;
    private Vector3 _currentVelocity;

    private int isWalkingHash;
    private int isCrouchingHash;
    private int isClimbingHash;

    private float _MovementAngle;
    [SerializeField]
    private float _linearSpeed;
    private Vector2 inputMovement;

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

    void OnEnable()
    {
        input.MoveEvent += OnMovement;
        input.CrouchEvent += OnCrouch;
        input.CrouchCanceledEvent += OnCrouchCanceled;
    }

    void OnDisable()
    {
        input.MoveEvent -= OnMovement;
        input.CrouchEvent -= OnCrouch;
        input.CrouchCanceledEvent -= OnCrouchCanceled;
    }

    void OnMovement(Vector2 input)
    {
        inputMovement = input;
    }

    void OnCrouch()
    {
        if (_linearSpeed > 0 && player.action == Action.None)
        {
            animator.SetBool(isCrouchingHash, true);
        }
    }

    void OnCrouchCanceled()
    {
        if (player.action == Action.None)
        {
            animator.SetBool(isCrouchingHash, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateValues();
        AnimationSpeeds();
        HandleAnimation();
    }

    void UpdateValues()
    {
        _currentRotation = characterTransform.rotation;
        _linearSpeed = playerController.speed;
        _currentVelocity = playerController.currentVelocity;

        float _LookAngle = _currentRotation.eulerAngles.y;
        _MovementAngle = Mathf.Atan2(_currentVelocity.x, _currentVelocity.z) * Mathf.Rad2Deg;
        _MovementAngle = (_MovementAngle - _LookAngle + 360) % 360;
    }

    void AnimationSpeeds()
    {
        animator.SetFloat("walkingSpeed", playerController.speed - 1);
        animator.SetFloat("Angle", _MovementAngle);
        animator.SetFloat("linearSpeed", _linearSpeed * 15);
        animator.SetFloat("crouchingSpeed", _linearSpeed * 50);

        if (player.action != Action.None && player.action != Action.Ladder)
        {
            animator.SetFloat("linearSpeed", 0);
        }
    }

    void HandleAnimation()
    {
        if (inputMovement.x != 0 || inputMovement.y != 0 && player.action == Action.None)
        {
            animator.SetBool(isWalkingHash, true);
        }
        else if (player.action == Action.None)
        {
            animator.SetBool(isWalkingHash, false);
        }
        if (player.action == Action.Ladder)
        {
            animator.SetBool(isClimbingHash, true);
        }
        else
        {
            animator.SetBool(isClimbingHash, false);
        }
    }
}
