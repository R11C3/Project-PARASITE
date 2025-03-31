using UnityEngine;

public class ToMouseAim : MonoBehaviour
{

    [SerializeField]
    private PlayerAim playerAim;
    [SerializeField]
    private Transform aimTransform;
    [SerializeField]
    private Renderer rendererComponent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rendererComponent.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = playerAim.GetMousePosition();
        // position.y += 1.5f;
        aimTransform.position = position;
    }
}
