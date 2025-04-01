using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    [SerializeField]
    private float shake;
    [SerializeField]
    private float time;
    [SerializeField]
    private Vector3 initialPosition;
    
    public void Shake()
    {
        StartCoroutine(CameraShakeRoutine());
    }

    private IEnumerator CameraShakeRoutine()
    {
        float elapsedTime = 0.0f;

        while(elapsedTime < time)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shake;
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = initialPosition;

        yield return null;
    }
}
