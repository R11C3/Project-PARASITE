using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "SO_InventoryInputs", menuName = "Scriptable Objects/Input Schemes/Inventory Input")]
public class SO_InventoryInput : ScriptableObject
{
    [SerializeField]
    private InputActionAsset input;

    private InputAction primaryClick;
    private InputAction secondaryClick;
    private InputAction primaryDrag;

    public event UnityAction PrimaryClickEvent;
    public event UnityAction SecondaryClickEvent;
    public event UnityAction PrimaryDragEvent;

    public event UnityAction PrimaryClickCanceledEvent;
    public event UnityAction SecondaryClickCanceledEvent;
    public event UnityAction PrimaryDragCanceledEvent;

    void OnEnable()
    {
        primaryClick = input.FindAction("PrimaryClick");
        secondaryClick = input.FindAction("SecondaryClick");
        primaryDrag = input.FindAction("PrimaryDrag");

        primaryClick.started += OnPrimaryClick;
        primaryClick.canceled += OnPrimaryClick;

        secondaryClick.started += OnSecondaryClick;
        secondaryClick.canceled += OnSecondaryClick;

        primaryDrag.started += OnPrimaryDrag;
        primaryDrag.canceled += OnPrimaryDrag;

        primaryClick.Enable();
        secondaryClick.Enable();
        primaryDrag.Enable();
    }

    void OnDisable()
    {
        primaryClick.started -= OnPrimaryClick;
        primaryClick.canceled -= OnPrimaryClick;

        secondaryClick.started -= OnSecondaryClick;
        secondaryClick.canceled -= OnSecondaryClick;

        primaryDrag.started -= OnPrimaryDrag;
        primaryDrag.canceled -= OnPrimaryDrag;

        primaryClick.Disable();
        secondaryClick.Disable();
        primaryDrag.Disable();
    }

    void OnPrimaryClick(InputAction.CallbackContext context)
    {
        if (PrimaryClickEvent != null && context.started)
        {
            PrimaryClickEvent.Invoke();
        }
        if (PrimaryClickCanceledEvent != null && context.canceled)
        {
            PrimaryClickCanceledEvent.Invoke();
        }
    }

    void OnSecondaryClick(InputAction.CallbackContext context)
    {
        if (SecondaryClickEvent != null && context.started)
        {
            SecondaryClickEvent.Invoke();
        }
        if (SecondaryClickCanceledEvent != null && context.canceled)
        {
            SecondaryClickCanceledEvent.Invoke();
        }
    }

    void OnPrimaryDrag(InputAction.CallbackContext context)
    {
        if (PrimaryDragEvent != null && context.started)
        {
            PrimaryDragEvent.Invoke();
        }
        if (PrimaryDragCanceledEvent != null && context.canceled)
        {
            PrimaryDragCanceledEvent.Invoke();
        }
    }
}
