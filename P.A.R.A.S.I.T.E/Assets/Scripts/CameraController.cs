using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject target;
    public GameObject aimSphere;
    private Camera mainCamera;

    [SerializeField]
    private SO_Input input;

    [SerializeField] private Vector3 offset;
    [Range(-6.0f, 2f)][SerializeField] private float zoom = 0.0f;
    [SerializeField] private float smoothSpeed = 0.1f;

    [SerializeField] private Vector3 targetPos;
    private Vector3 aimTargetPos;
    private Vector3 origin;

    [SerializeField] private Vector3 aimOffset;

    private bool flag = true;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        origin = target.transform.position;
        if(!input._isAimPressed)
        {
            SmoothFollow();
            LookTo(target.transform.position);
        }
        if(input._isAimPressed)
        {

        }
    }

    public void SmoothFollow()
    {
        targetPos = origin + offset;
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

    public void LookTo(Vector3 obj)
    {
        transform.LookAt(obj);
    }
}
