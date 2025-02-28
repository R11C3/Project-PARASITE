using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject gunBarrel;
    private Vector3 EndOfBarrel;
    [SerializeField]
    private PlayerAim playerAim;
    [SerializeField]
    private TrailRenderer trail;
    [SerializeField]
    private LayerMask mask;

    [SerializeField]
    private InputHandler input;

    private PlayerInput playerInput;
    
    public bool _isFirePressed;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = new PlayerInput();

        playerInput.CharacterControls.Fire.started += onFireInput;
        playerInput.CharacterControls.Fire.canceled += onFireInput;
    }

    void onFireInput (InputAction.CallbackContext context)
    {
        _isFirePressed = context.ReadValueAsButton();
    }

    // Update is called once per frame
    void Update()
    {
        EndOfBarrel = gunBarrel.transform.position;
        if(_isFirePressed == true)
        {
            _isFirePressed = false;

            Vector3 target = playerAim.mousePosition;

            if (Physics.Raycast(EndOfBarrel, target, out RaycastHit hit, float.MaxValue, mask))
            {
                trail = Instantiate(trail, EndOfBarrel, Quaternion.identity);

                Debug.Log("pew");

                StartCoroutine(SpawnTrail(trail, hit));
            }
        }
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
    {
        float time = 0;
        Vector3 startPosition = trail.transform.position;

        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime / trail.time;

            yield return null;
        }

        Destroy(trail.gameObject, trail.time);
    }
}
