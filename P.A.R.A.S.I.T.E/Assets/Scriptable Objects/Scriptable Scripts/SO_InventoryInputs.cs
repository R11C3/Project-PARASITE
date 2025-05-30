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
    private InputAction quickDrop;

    public event UnityAction PrimaryClickEvent;
    public event UnityAction SecondaryClickEvent;
    public event UnityAction PrimaryDragEvent;
    public event UnityAction quickDropEvent;

    public event UnityAction PrimaryDragPerformedEvent;

    public event UnityAction PrimaryClickCanceledEvent;
    public event UnityAction SecondaryClickCanceledEvent;
    public event UnityAction PrimaryDragCanceledEvent;
    public event UnityAction quickDropCanceledEvent;

    void OnEnable()
    {
        primaryClick = input.FindAction("PrimaryClick");
        secondaryClick = input.FindAction("SecondaryClick");
        primaryDrag = input.FindAction("PrimaryDrag");
        quickDrop = input.FindAction("QuickDrop");

        primaryClick.started += OnPrimaryClick;
        primaryClick.canceled += OnPrimaryClick;

        secondaryClick.started += OnSecondaryClick;
        secondaryClick.canceled += OnSecondaryClick;

        primaryDrag.started += OnPrimaryDrag;
        primaryDrag.performed += OnPrimaryDrag;
        primaryDrag.canceled += OnPrimaryDrag;

        quickDrop.started += OnQuickDrop;
        quickDrop.canceled += OnQuickDrop;

        primaryClick.Enable();
        secondaryClick.Enable();
        primaryDrag.Enable();
        quickDrop.Enable();
    }

    void OnDisable()
    {
        primaryClick.started -= OnPrimaryClick;
        primaryClick.canceled -= OnPrimaryClick;

        secondaryClick.started -= OnSecondaryClick;
        secondaryClick.canceled -= OnSecondaryClick;

        primaryDrag.started -= OnPrimaryDrag;
        primaryDrag.performed -= OnPrimaryDrag;
        primaryDrag.canceled -= OnPrimaryDrag;

        quickDrop.started -= OnQuickDrop;
        quickDrop.canceled -= OnQuickDrop;

        primaryClick.Disable();
        secondaryClick.Disable();
        primaryDrag.Disable();
        quickDrop.Disable();
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
        if (PrimaryDragPerformedEvent != null && context.performed)
        {
            PrimaryDragPerformedEvent.Invoke();
        }
        if (PrimaryDragCanceledEvent != null && context.canceled)
        {
            PrimaryDragCanceledEvent.Invoke();
        }
    }

    void OnQuickDrop(InputAction.CallbackContext context)
    {
        if (quickDropEvent != null && context.started)
        {
            quickDropEvent.Invoke();
        }
        if (quickDropCanceledEvent != null && context.canceled)
        {
            quickDropCanceledEvent.Invoke();
        }
    }
}
