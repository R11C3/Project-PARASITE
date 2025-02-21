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

    [SerializeField] private float smoothSpeed = 0.1f;
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
        Vector3 targetPos = target.position + offset;
        Vector3 smoothFollow = Vector3.Lerp(transform.position, targetPos, smoothSpeed);

        transform.position = smoothFollow;
    }

    public void LookAt()
    {
        cameraTransform.LookAt(target);
    }
}
