using System.Collections;
using System.Runtime.InteropServices;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.Splines.Interpolators;

public class HingeDoor : Interactable
{

    [SerializeField]
    private bool open;

    [SerializeField]
    private GameObject door;
    [SerializeField]
    private GameObject hinge;
    [SerializeField]
    private float openAngle = 90f;
    [SerializeField]
    private float closeAngle = -90f;
    [SerializeField]
    private float speed = 0.25f;

    public override void Interact()
    {
        if(open)
        {
            StartCoroutine(Rotate(openAngle));
            open = false;
        }
        else
        {
            StartCoroutine(Rotate(closeAngle));
            open = true;
        }
    }

    public IEnumerator Rotate(float angle)
    {
        Vector3 currentRotation = door.transform.localEulerAngles;

        float elapsedTime = 0f;

        angle = angle / speed;

        while(elapsedTime < speed)
        {
            door.transform.RotateAround(hinge.transform.position, Vector3.up, angle * Time.deltaTime);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        yield return null;
    }
}