using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{

    private PlayerInput playerInput;

    public bool _isSprintPressed;
    public bool _isRollPressed;
    public bool _isFirePressed;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();

        playerInput.CharacterControls.Sprint.started += onSprintInput;
        playerInput.CharacterControls.Sprint.canceled += onSprintInput;
        playerInput.CharacterControls.Roll.started += onRollInput;
        playerInput.CharacterControls.Roll.canceled += onRollInput;
        playerInput.CharacterControls.Fire.started += onFireInput;
        playerInput.CharacterControls.Fire.canceled += onFireInput;
    }

    void onSprintInput (InputAction.CallbackContext context)
    {
        _isSprintPressed = context.ReadValueAsButton();
    }

    void onRollInput (InputAction.CallbackContext context)
    {
        _isRollPressed = context.ReadValueAsButton();
    }

    void onFireInput (InputAction.CallbackContext context)
    {
        _isFirePressed = context.ReadValueAsButton();
    }
}
