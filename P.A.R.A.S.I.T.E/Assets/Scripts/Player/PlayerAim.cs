using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerAim : MonoBehaviour
{

    [SerializeField] public LayerMask mask;
    [SerializeField] public LayerMask floor;
    [SerializeField] private bool renderLine;

    private Transform characterTransform;
    private Camera mainCamera;
    private PlayerStats player;
    public Vector3 mousePosition;

    void Awake()
    {
        mainCamera=  Camera.main;
        characterTransform = GetComponent<Transform>();
        player = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.action == Action.None)
        {
            GetMousePosition();
            Aim();
        }
    }

    public Vector3 GetMousePosition() 
    {
        if(player.action != Action.Inventory)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, mask))
            {
                return hitInfo.point;
            }
            else if (Physics.Raycast(ray, out RaycastHit groundHit, Mathf.Infinity, floor))
            {
                Vector3 position = groundHit.point;
                position.y += 1f;
                return position;
            }
            else
            {
                return Vector3.zero;
            }
        }
        return Vector3.zero;
    }

    private void Aim()
    {
        Vector3 position = GetMousePosition();

        Vector3 direction = position - characterTransform.position;

        direction.y = 0;

        if(direction.sqrMagnitude != 0.0f)
        {
            characterTransform.forward = direction;
        }
    }

    public void LookTo(Vector3 goal)
    {
        Vector3 direction = goal - characterTransform.position;

        direction.y = 0;

        characterTransform.forward = direction;
    }
}
