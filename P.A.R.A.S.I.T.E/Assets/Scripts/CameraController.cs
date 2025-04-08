using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(!input._isAimPressed)
        {
            SmoothFollow();
            LookTo(target.transform.position);
        }
        if(input._isAimPressed)
        {
            SmoothAim();
        }
    }

    public void SmoothFollow()
    {
        targetPos = target.transform.position + offset;
        ZoomChange();
        Vector3 smoothFollow = Vector3.Lerp(transform.position, targetPos, smoothSpeed);

        if(input._isAimPressed) LookTo(smoothFollow);

        transform.position = smoothFollow;
    }

    public void SmoothAim()
    {
        targetPos = aimSphere.transform.position + aimOffset;
        ZoomChange();
        Vector3 smoothFollow = Vector3.Lerp(transform.position, targetPos, smoothSpeed);

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
