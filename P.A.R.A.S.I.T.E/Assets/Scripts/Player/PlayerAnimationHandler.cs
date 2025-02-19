using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{

    private Animator animator;
    private CharacterController characterController;
    private Quaternion _currentRotation;
    private PlayerController playerController;

    private int isWalkingHash;
    private int isRollingHash;

    private bool _isWalking;
    private bool _isRolling;
    private bool _rolling;
    private bool _isMovementPressed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        playerController = GetComponent<PlayerController>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRollingHash = Animator.StringToHash("isRolling");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBools();
        AnimationSpeeds();
        HandleAnimation();
    }

    void UpdateBools()
    {
        _isWalking = animator.GetBool(isWalkingHash);
        _isRolling = animator.GetBool(isRollingHash);
        _isMovementPressed = playerController._isMovementPressed;
        _rolling = playerController._rolling;
    }

    void AnimationSpeeds()
    {
        animator.SetFloat("rollSpeed", 1/ playerController._rollTime);
        animator.SetFloat("walkingSpeed", playerController._speed - 1);
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
