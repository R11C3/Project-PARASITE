using UnityEngine;

public class Interact : MonoBehaviour
{

    [SerializeField]
    private SO_Input _input;
    [SerializeField]
    private PlayerAim _aim;
    [SerializeField]
    private LayerMask mask;

    private Camera _camera;

    private bool _canInteract = true;

    void Awake()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(_input._isInteractPressed && _canInteract)
        {
            InteractAction();
            _canInteract = false;
        }
        if(!_input._isInteractPressed && !_canInteract)
        {
            _canInteract = true;
        }
    }

    void InteractAction()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, mask))
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
