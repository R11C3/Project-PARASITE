using System.Collections;
using UnityEngine;

public class SlidingDoor : Interactable
{
    [SerializeField]
    private GameObject door;
    [SerializeField]
    private float openDistance = 1.5f;
    [SerializeField]
    private float closeDistance = -1.5f;
    [SerializeField]
    private bool open = false;
    [SerializeField]
    [Header("Higher Speed Faster Door")]
    private float speed = 4f;

    public override void Interact()
    {
        if(open)
        {
            StartCoroutine(Open(openDistance));
            open = false;
        }
        else
        {
            StartCoroutine(Open(closeDistance));
            open = true;
        }
    }

    public IEnumerator Open(float distance)
    {
        Vector3 currentPosition = door.transform.localPosition;
        Vector3 goalPosition = new Vector3(currentPosition.x + distance, currentPosition.y, currentPosition.z);

        while(door.transform.localPosition != goalPosition)
        {
            Vector3 smoothMove = Vector3.MoveTowards(door.transform.localPosition, goalPosition, speed * Time.deltaTime);
            door.transform.localPosition = smoothMove;

            yield return null;
        }

        door.transform.localPosition = goalPosition;

        yield return null;
    }
}
