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
    [Header("Lower time faster door")]
    private float time = 0.25f;

    public override void Interact(GameObject source)
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

        angle = angle / time;

        while(elapsedTime < time)
        {
            door.transform.RotateAround(hinge.transform.position, Vector3.up, angle * Time.deltaTime);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        yield return null;
    }
}