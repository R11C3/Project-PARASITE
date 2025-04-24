using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{

    public GameObject target;
    public GameObject aimSphere;
    [SerializeField]
    private SO_Input input;

    [SerializeField] private Vector3 offset;
    [Range(-6.0f, 2f)][SerializeField] private float zoom = 0.0f;
    [SerializeField] private float smoothSpeed = 0.1f;

    [SerializeField] private Vector3 targetPos;

    [SerializeField] private Vector3 aimOffset;

    [SerializeField] private float maxDistance;

    public float aimDistance;
    private float baseDistance;

    // Start is called before the first frame update
    void Start()
    {
        baseDistance = maxDistance;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(!input._isAimPressed)
        {
            maxDistance = baseDistance;
            SmoothFollow();
        }
        if(input._isAimPressed)
        {
            maxDistance = baseDistance * aimDistance;
            SmoothAim();
        }
    }

    public void SmoothFollow()
    {
        Vector3 difference = -transform.position + aimSphere.transform.position;

        ZoomChange();

        if(difference.magnitude > maxDistance)
        {
            difference = difference.normalized * maxDistance;
        }

        difference += offset + target.transform.position;

        Vector3 smoothFollow = Vector3.Lerp(transform.position, difference, smoothSpeed);

        transform.position = smoothFollow;
    }

    public void SmoothAim()
    {
        Vector3 difference = -transform.position + aimSphere.transform.position;

        ZoomChange();

        if(difference.magnitude > maxDistance)
        {
            difference = difference.normalized * maxDistance;
        }

        difference += aimOffset + target.transform.position;

        Vector3 smoothFollow = Vector3.Lerp(transform.position, difference, smoothSpeed);

        transform.position = smoothFollow;
    }

    public void ZoomChange()
    {
        targetPos.x = targetPos.x - zoom;
        targetPos.y = targetPos.y + zoom * 3;
        targetPos.z = targetPos.z - zoom;

        if (targetPos.y < target.transform.position.y + 1.5f)
        {
            targetPos.y = target.transform.position.y + 1.5f;
        }
    }

    public void LookTo(Vector3 position)
    {
        transform.LookAt(position);
    }

    
}
