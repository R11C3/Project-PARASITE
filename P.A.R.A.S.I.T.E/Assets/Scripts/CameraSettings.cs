using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{

    float _FOV = 60.0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.fieldOfView = _FOV;
    }

    void OnGUI()
    {
        float max, min;

        max = 150.0f;
        min = 20.0f;
        _FOV = GUI.HorizontalSlider(new Rect(20,20,100,40), _FOV, min, max);
    }
}
