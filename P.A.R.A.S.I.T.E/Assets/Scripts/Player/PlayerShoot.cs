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
    private Transform characterTransform;
    [SerializeField]
    private float speed = 100.0f;

    [SerializeField]
    private InputHandler input;

    private PlayerInput playerInput;
    
    public bool _isFirePressed;
    // Start is called before the first frame update
    void Awake()
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

            Vector3 target = playerAim.GetMousePosition();

            Vector3 direction = target - characterTransform.position;

            direction.y = 0;

            TrailRenderer tempTrail = Instantiate(trail, EndOfBarrel, Quaternion.identity);

            if (Physics.Raycast(EndOfBarrel, direction, out RaycastHit hit, float.MaxValue, mask))
            {
                // Debug.Log("pew");

                StartCoroutine(SpawnTrail(tempTrail, hit.point, true));
            }
            else
            {
                StartCoroutine(SpawnTrail(tempTrail, direction * 100, false));
            }
        }
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 hit, bool impact)
    {
        float distance = Vector3.Distance(trail.transform.position, hit);
        float startingDistance = distance;
        Vector3 startPosition = trail.transform.position;

        while (distance > 0)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hit, 1 - (distance / startingDistance));
            distance -= Time.deltaTime * speed;

            yield return null;
        }

        trail.transform.position = hit;

        if (impact)
        {
            //play particle system
            //Instantiate(ImpactParticleSystem, hit, Quaternion.LookRotation(hitNormal))
        }

        Destroy(trail.gameObject, trail.time);
    }

    void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
}
