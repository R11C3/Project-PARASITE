using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{

    [Range(15.0f, 120.0f)]
    [SerializeField] private float _FOV = 30.0f;
    [SerializeField] private bool _isOrtho = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.fieldOfView = _FOV;
        Camera.main.orthographic = _isOrtho;
    }
}
