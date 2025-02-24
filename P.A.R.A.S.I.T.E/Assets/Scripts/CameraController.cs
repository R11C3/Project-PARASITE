using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    private Transform cameraTransform;
    private CharacterController characterController;

    [SerializeField] private Vector3 offset;
    [SerializeField] private Quaternion rotation;
    [Range(-10.0f, 2f)][SerializeField] private float zoom = 0.0f;
    [SerializeField] private float smoothSpeed = 0.1f;

    [SerializeField] private Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = GetComponent<Transform>();
        characterController = GetComponent<CharacterController>();
        cameraTransform.rotation = rotation;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        SmoothFollow();
        LookAt();
    }

    public void SmoothFollow()
    {
        targetPos = target.position + offset;
        zoomChange();
        Vector3 smoothFollow = Vector3.Lerp(transform.position, targetPos, smoothSpeed);

        transform.position = smoothFollow;
    }

    public void zoomChange()
    {
        targetPos.x = targetPos.x - zoom;
        targetPos.y = targetPos.y + zoom * 3;
        targetPos.z = targetPos.z - zoom;

        if (targetPos.y < target.position.y + 1.5f)
        {
            targetPos.y = target.position.y + 1.5f;
        }
    }

    public void LookAt()
    {
        cameraTransform.LookAt(target);
    }
}
