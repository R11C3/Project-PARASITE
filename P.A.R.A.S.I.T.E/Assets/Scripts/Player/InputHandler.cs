using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    private SO_Input _input;

    void OnEnable()
    {
        _input.MoveEvent += OnMove;
        _input.RollEvent += OnRoll;
        _input.RollCancelledEvent += OnRollCancelled;
        _input.CrouchEvent += OnCrouch;
        _input.CrouchCancelledEvent += OnCrouchCancelled;
        _input.SprintEvent += OnSprint;
        _input.SprintCancelledEvent += OnSprintCancelled;
        _input.FireEvent += OnFire;
        _input.FireCancelledEvent += OnFireCancelled;
        _input.ReloadEvent += OnReload;
        _input.ReloadCancelledEvent += OnReloadCancelled;
        _input.OneEvent += OnOne;
        _input.OneCancelledEvent += OnOneCancelled;
        _input.TwoEvent += OnTwo;
        _input.TwoCancelledEvent += OnTwoCancelled;
    }

    void OnDisable()
    {
        _input.MoveEvent -= OnMove;
        _input.RollEvent -= OnRoll;
        _input.RollCancelledEvent -= OnRollCancelled;
        _input.CrouchEvent -= OnCrouch;
        _input.CrouchCancelledEvent -= OnCrouchCancelled;
        _input.SprintEvent -= OnSprint;
        _input.SprintCancelledEvent -= OnSprintCancelled;
        _input.FireEvent -= OnFire;
        _input.FireCancelledEvent -= OnFireCancelled;
        _input.ReloadEvent -= OnReload;
        _input.ReloadCancelledEvent -= OnReloadCancelled;
        _input.OneEvent -= OnOne;
        _input.OneCancelledEvent -= OnOneCancelled;
        _input.TwoEvent -= OnTwo;
        _input.TwoCancelledEvent -= OnTwoCancelled;
    }

    private void OnMove(Vector2 movement)
    {
        _input._currentMovementInput = movement;
        if(movement.x != 0 || movement.y != 0)
            _input._isMovementPressed = true;
        else
            _input._isMovementPressed = false;
    }

    private void OnRoll()
    {
        _input._isRollPressed = true;
    }

    private void OnRollCancelled()
    {
        _input._isRollPressed = false;
    }

    private void OnCrouch()
    {
        _input._isCrouchPressed = true;
    }

    private void OnCrouchCancelled()
    {
        _input._isCrouchPressed = false;
    }

    private void OnSprint()
    {
        _input._isSprintPressed = true;
    }

    private void OnSprintCancelled()
    {
        _input._isSprintPressed = false;
    }

    private void OnFire()
    {
        _input._isFirePressed = true;
    }

    private void OnFireCancelled()
    {
        _input._isFirePressed = false;
    }

    private void OnReload()
    {
        _input._isReloadPressed = true;
    }

    private void OnReloadCancelled()
    {
        _input._isReloadPressed = false;
    }

    private void OnOne()
    {
        _input._isOnePressed = true;
    }

    private void OnOneCancelled()
    {
        _input._isOnePressed = false;
    }

    private void OnTwo()
    {
        _input._isTwoPressed = true;
    }

    private void OnTwoCancelled()
    {
        _input._isTwoPressed = false;
    }
}
