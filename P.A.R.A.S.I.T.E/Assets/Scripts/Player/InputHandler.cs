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
        _input.VaultEvent += OnVault;
        _input.VaultCanceledEvent += OnVaultCanceled;
        _input.CrouchEvent += OnCrouch;
        _input.CrouchCanceledEvent += OnCrouchCanceled;
        _input.SprintEvent += OnSprint;
        _input.SprintCanceledEvent += OnSprintCanceled;
        _input.FireEvent += OnFire;
        _input.FireCanceledEvent += OnFireCanceled;
        _input.FireModeEvent += OnFireMode;
        _input.FireModeCanceledEvent += onFireModeCanceled;
        _input.AimEvent += OnAim;
        _input.AimCanceledEvent += OnAimCanceled;
        _input.ReloadEvent += OnReload;
        _input.ReloadCanceledEvent += OnReloadCanceled;
        _input.OneEvent += OnOne;
        _input.OneCanceledEvent += OnOneCanceled;
        _input.TwoEvent += OnTwo;
        _input.TwoCanceledEvent += OnTwoCanceled;
        _input.ThreeEvent += OnThree;
        _input.ThreeCanceledEvent += OnThreeCanceled;
        _input.InteractEvent += OnInteract;
        _input.InteractCanceledEvent += OnInteractCanceled;
        _input.InventoryEvent += OnInventory;
        _input.InventoryCanceledEvent += OnInventoryCanceled;
        _input.DragEvent += OnDrag;
        _input.DragCanceledEvent += OnDragCanceled;

        _input.DropEvent += OnDrop;
        _input.DropCanceledEvent += OnDropCanceled;
    }

    void OnDisable()
    {
        _input.MoveEvent -= OnMove;
        _input.VaultEvent -= OnVault;
        _input.VaultCanceledEvent -= OnVaultCanceled;
        _input.CrouchEvent -= OnCrouch;
        _input.CrouchCanceledEvent -= OnCrouchCanceled;
        _input.SprintEvent -= OnSprint;
        _input.SprintCanceledEvent -= OnSprintCanceled;
        _input.FireEvent -= OnFire;
        _input.FireCanceledEvent -= OnFireCanceled;
        _input.AimEvent -= OnAim;
        _input.AimCanceledEvent -= OnAimCanceled;
        _input.FireModeEvent -= OnFireMode;
        _input.FireModeCanceledEvent -= onFireModeCanceled;
        _input.ReloadEvent -= OnReload;
        _input.ReloadCanceledEvent -= OnReloadCanceled;
        _input.OneEvent -= OnOne;
        _input.OneCanceledEvent -= OnOneCanceled;
        _input.TwoEvent -= OnTwo;
        _input.TwoCanceledEvent -= OnTwoCanceled;
        _input.ThreeEvent -= OnThree;
        _input.ThreeCanceledEvent -= OnThreeCanceled;
        _input.InteractEvent -= OnInteract;
        _input.InteractCanceledEvent -= OnInteractCanceled;
        _input.InventoryEvent -= OnInventory;
        _input.InventoryCanceledEvent -= OnInventoryCanceled;
        _input.DragEvent -= OnDrag;
        _input.DragCanceledEvent -= OnDragCanceled;

        _input.DropEvent -= OnDrop;
        _input.DropCanceledEvent -= OnDropCanceled;
    }

    private void OnMove(Vector2 movement)
    {
        _input._currentMovementInput = movement;
        if(movement.x != 0 || movement.y != 0)
            _input._isMovementPressed = true;
        else
            _input._isMovementPressed = false;
    }

    private void OnVault()
    {
        _input._isVaultPressed = true;
    }

    private void OnVaultCanceled()
    {
        _input._isVaultPressed = false;
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

    private void OnAim()
    {
        _input._isAimPressed = true;
    }

    private void OnAimCanceled()
    {
        _input._isAimPressed = false;
    }

    private void OnFireMode()
    {
        _input._isFireModePressed = true;
    }

    private void onFireModeCanceled()
    {
        _input._isFireModePressed = false;
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

    private void OnThree()
    {
        _input._isThreePressed = true;
    }

    private void OnThreeCanceled()
    {
        _input._isThreePressed = false;
    }

    private void OnInteract()
    {
        _input._isInteractPressed = true;
    }

    private void OnInteractCanceled()
    {
        _input._isInteractPressed = false;
    }

    private void OnInventory()
    {
        _input._isInventoryPressed = true;
    }

    private void OnInventoryCanceled()
    {
        _input._isInventoryPressed = false;
    }

    private void OnDrag()
    {
        _input._isDragPressed = true;
    }

    private void OnDragCanceled()
    {
        _input._isDragPressed = false;
    }

    private void OnDrop()
    {
        _input._isDropPressed = true;
    }

    private void OnDropCanceled()
    {
        _input._isDropPressed = false;
    }
}
