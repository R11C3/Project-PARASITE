using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerAim : MonoBehaviour
{

    [SerializeField] public LayerMask groundLayer;

    private GameObject lineRendererObject;
    private Transform characterTransform;
    private Camera mainCamera;
    private LineRenderer lineRenderer;
    private GameObject gun;
    private GameObject gunB;
    private Transform gunBarrel;
    public Vector3 mousePosition;

    void Awake()
    {
        mainCamera=  Camera.main;
        characterTransform = GetComponent<Transform>();
        lineRendererObject = GameObject.Find("Line Renderer");
        lineRenderer = lineRendererObject.GetComponent<LineRenderer>();
        gun = GameObject.Find("Test Gun");
        gunB = GameObject.Find("End Of Barrel");
        gunBarrel = gunB.GetComponent<Transform>();

        lineRenderer.positionCount = 3;
    }

    // Update is called once per frame
    void Update()
    {
        GetMousePosition();
        Aim();
    }

    private void GetMousePosition() 
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundLayer))
        {
            mousePosition = hitInfo.point;
        }
        else
        {
            mousePosition = Vector3.zero;
        }
    }

    private void Aim()
    {
        Vector3 position = mousePosition;

        Vector3 direction = position - characterTransform.position;

        direction.y = 0;

        if(direction.sqrMagnitude != 0.0f)
        {
            characterTransform.forward = direction;
        }

        Vector3 lineRenderGoal = position;
        lineRenderGoal.y += 1.2f;
        Vector3 lineRenderStart = gunBarrel.position;
        //lineRenderStart.y += 1.2f;

        lineRenderer.SetPosition(2, position);
        lineRenderer.SetPosition(1, lineRenderGoal);
        lineRenderer.SetPosition(0, lineRenderStart);
    }

    public void LookTo(Vector3 goal)
    {
        Vector3 direction = goal - characterTransform.position;

        direction.y = 0;

        characterTransform.forward = direction;
    }
}
