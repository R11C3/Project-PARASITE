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
        _input.RollCanceledEvent += OnRollCanceled;
        _input.CrouchEvent += OnCrouch;
        _input.CrouchCanceledEvent += OnCrouchCanceled;
        _input.SprintEvent += OnSprint;
        _input.SprintCanceledEvent += OnSprintCanceled;
        _input.FireEvent += OnFire;
        _input.FireCanceledEvent += OnFireCanceled;
        _input.ReloadEvent += OnReload;
        _input.ReloadCanceledEvent += OnReloadCanceled;
        _input.OneEvent += OnOne;
        _input.OneCanceledEvent += OnOneCanceled;
        _input.TwoEvent += OnTwo;
        _input.TwoCanceledEvent += OnTwoCanceled;
        _input.InteractEvent += OnInteract;
        _input.InteractCanceledEvent += OnInteractCanceled;
    }

    void OnDisable()
    {
        _input.MoveEvent -= OnMove;
        _input.RollEvent -= OnRoll;
        _input.RollCanceledEvent -= OnRollCanceled;
        _input.CrouchEvent -= OnCrouch;
        _input.CrouchCanceledEvent -= OnCrouchCanceled;
        _input.SprintEvent -= OnSprint;
        _input.SprintCanceledEvent -= OnSprintCanceled;
        _input.FireEvent -= OnFire;
        _input.FireCanceledEvent -= OnFireCanceled;
        _input.ReloadEvent -= OnReload;
        _input.ReloadCanceledEvent -= OnReloadCanceled;
        _input.OneEvent -= OnOne;
        _input.OneCanceledEvent -= OnOneCanceled;
        _input.TwoEvent -= OnTwo;
        _input.TwoCanceledEvent -= OnTwoCanceled;
        _input.InteractEvent -= OnInteract;
        _input.InteractCanceledEvent -= OnInteractCanceled;
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

    private void OnRollCanceled()
    {
        _input._isRollPressed = false;
    }

    private void OnCrouch()
    {
        _input._isCrouchPressed = true;
    }

    private void OnCrouchCanceled()
    {
        _input._isCrouchPressed = false;
    }

    private void OnSprint()
    {
        _input._isSprintPressed = true;
    }

    private void OnSprintCanceled()
    {
        _input._isSprintPressed = false;
    }

    private void OnFire()
    {
        _input._isFirePressed = true;
    }

    private void OnFireCanceled()
    {
        _input._isFirePressed = false;
    }

    private void OnReload()
    {
        _input._isReloadPressed = true;
    }

    private void OnReloadCanceled()
    {
        _input._isReloadPressed = false;
    }

    private void OnOne()
    {
        _input._isOnePressed = true;
    }

    private void OnOneCanceled()
    {
        _input._isOnePressed = false;
    }

    private void OnTwo()
    {
        _input._isTwoPressed = true;
    }

    private void OnTwoCanceled()
    {
        _input._isTwoPressed = false;
    }

    private void OnInteract()
    {
        _input._isInteractPressed = true;
    }

    private void OnInteractCanceled()
    {
        _input._isInteractPressed = false;
    }
}
