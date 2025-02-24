using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{

    [SerializeField] public LayerMask groundLayer;

    private Transform characterTransform;
    private Camera mainCamera;

    void Awake()
    {
        mainCamera=  Camera.main;
        characterTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
    }

    private Vector3 GetMousePosition() 
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundLayer))
        {
            return hitInfo.point;
        }
        else
        {
            return Vector3.zero;
        }
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
