using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(PlayerStats))]

public class Interact : MonoBehaviour
{

    [SerializeField]
    private SO_Input input;
    [SerializeField]
    private PlayerAim aim;
    [SerializeField]
    private LayerMask mask;
    [SerializeField]
    private float interactDistance;

    private PlayerStats player;

    private Camera mainCamera;

    private bool canInteract = true;

    void Awake()
    {
        player = GetComponent<PlayerStats>();
        mainCamera = Camera.main;
    }

    void OnEnable()
    {
        input.InteractEvent += OnInteract;
        input.InteractCanceledEvent += OnInteractCanceled;
    }

    void OnDisable()
    {
        input.InteractEvent -= OnInteract;
        input.InteractCanceledEvent -= OnInteractCanceled;
    }

    void OnInteract()
    {
        if (canInteract)
        {
            InteractAction();
            canInteract = false;
        }
    }

    void OnInteractCanceled()
    {
        canInteract = true;
    }

    bool InRange(Vector3 position)
    {
        if(Vector3.Distance(transform.position, position) <= interactDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void InteractAction()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mask))
        {
            if(InRange(hit.point))
            {
                Interactable target;
                GameObject hitObject = hit.transform.gameObject;
                hitObject.TryGetComponent<Interactable>(out target);
                while(target == null && hitObject != null)
                {
                    hitObject = hitObject.transform.parent.gameObject;
                    hitObject.TryGetComponent<Interactable>(out target);
                }
                target.Interact(gameObject);
            }
        }
    }
}
