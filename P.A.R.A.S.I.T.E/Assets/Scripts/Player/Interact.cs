using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(PlayerStats))]

public class Interact : MonoBehaviour
{

    [SerializeField]
    private SO_Input _input;
    [SerializeField]
    private PlayerAim _aim;
    [SerializeField]
    private LayerMask mask;
    [SerializeField]
    private float interactDistance;

    private PlayerStats player;

    private Camera _camera;

    private bool _canInteract = true;
    private bool _canDrop = true;

    void Awake()
    {
        player = GetComponent<PlayerStats>();
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        InteractInput();
        DropInput();
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

    void InteractInput()
    {
        if(_input._isInteractPressed && _canInteract)
        {
            InteractAction();
            _canInteract = false;
        }
        if(!_input._isInteractPressed)
        {
            _canInteract = true;
        }
    }

    void InteractAction()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

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

    void DropInput()
    {
        if(_input._isDropPressed && _canDrop)
        {
            DropAction();
            _canDrop = false;
        }
        if(!_input._isDropPressed && !_canDrop)
        {
            _canDrop = true;
        }
    }

    void DropAction()
    {
        // SO_Item item = player.inventory.Get(0);
        // if(item != null)
        // {
        //     player.inventory.DropItem(gameObject, item);
        // }
    }
}
