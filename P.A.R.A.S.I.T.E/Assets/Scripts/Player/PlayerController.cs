using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private SO_Input input;
    public float speed = 0.0f;

    [SerializeField]
    private PlayerStats player;
    private CharacterController characterController;

    private Camera mainCamera;

    public bool isCrouchPressed, isSprintPressed;
    [HideInInspector]
    public Vector3 lastMovement, currentVelocity;
    [HideInInspector]
    public Quaternion currentRotation;
    [HideInInspector]
    public Vector3 forward, right, forwardMovement, rightMovement, initialMovement;
    public Vector3 currentMovement, inputMovement;

    [Header("Movement Dampening")]
    public float dampening = 5.0f;

    [HideInInspector]
    public Bounds ladderTop;
    [HideInInspector]
    public Bounds ladderBottom;
    public Vector3 ladderDirection;

    void Awake()
    {
        player = GetComponent<PlayerStats>();
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;

        lastMovement = new Vector3(0.0f, -9.8f, 0.0f);

        mainCamera = Camera.main;

        forward = mainCamera.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    void OnEnable()
    {
        input.MoveEvent += OnMovement;
        input.SprintEvent += OnSprint;
        input.SprintCanceledEvent += OnSprintCanceled;
        input.CrouchEvent += OnCrouch;
        input.CrouchCanceledEvent += OnCrouchCanceled;
    }

    void OnDisable()
    {
        input.MoveEvent -= OnMovement;
        input.SprintEvent -= OnSprint;
        input.SprintCanceledEvent -= OnSprintCanceled;
        input.CrouchEvent -= OnCrouch;
        input.CrouchCanceledEvent -= OnCrouchCanceled;
    }

    private void OnMovement(Vector2 inputMovement)
    {
        rightMovement = right * inputMovement.x;
        forwardMovement = forward * inputMovement.y;
        initialMovement = Vector3.Normalize(rightMovement + forwardMovement);
        currentMovement = Vector3.Lerp(currentMovement, initialMovement, dampening);
        this.inputMovement = inputMovement;
    }

    void OnSprint()
    {
        isSprintPressed = true;
    }

    void OnSprintCanceled()
    {
        isSprintPressed = false;
    }

    void OnCrouch()
    {
        isCrouchPressed = true;
    }

    void OnCrouchCanceled()
    {
        isCrouchPressed = false;
    }

    void Update()
    {
        HandleMovement();
        if (player.action == Action.Ladder)
        {
            HandleLadder();
        }
    }

    void HandleGravity()
    {
        if (characterController.isGrounded)
        {
            float _groundedGravity = -0.5f;
            currentMovement.y = _groundedGravity;
        }
        else
        {
            float _gravity = -9.8f;
            currentMovement.y = _gravity;
        }
    }

    void HandleMovement()
    {
        if (player.action == Action.None)
        {
            if ((inputMovement.x != 0 || inputMovement.y != 0) && speed <= player.speed)
            {
                speed += player.acceleration * Time.deltaTime;
            }

            if (inputMovement.x == 0 && inputMovement.y == 0 && speed > 0.0f)
            {
                speed -= player.deceleration * Time.deltaTime;
            }

            if (speed < 0.0f)
            {
                speed = 0.0f;
            }

            if (speed > player.speed)
            {
                speed -= player.deceleration * Time.deltaTime;
            }

            if (inputMovement.x == 0 && inputMovement.y == 0 && !isCrouchPressed && !isSprintPressed)
            {
                currentMovement = lastMovement;
                HandleGravity();
                characterController.Move(new Vector3(currentMovement.x * speed * Time.deltaTime, currentMovement.y * Time.deltaTime, currentMovement.z * speed * Time.deltaTime));
            }

            if (inputMovement.x != 0 || inputMovement.y != 0)
            {
                HandleGravity();
                characterController.Move(new Vector3(currentMovement.x * speed * Time.deltaTime, currentMovement.y * Time.deltaTime, currentMovement.z * speed * Time.deltaTime));

                lastMovement = currentMovement;
            }

            currentVelocity = characterController.velocity;
        }
    }

    void HandleLadder()
    {
        if (inputMovement.y > 0.2f || inputMovement.y < -0.2f)
            {
                Vector3 movement = new Vector3(0, inputMovement.y * 1.5f * Time.deltaTime, 0);

                if (ladderTop.Contains(transform.position) && inputMovement.y > 0.2f)
                {
                    player.action = Action.None;
                    movement.x = ladderDirection.x * 6f * Time.deltaTime;
                    movement.y = movement.y * 2f;
                    movement.z = ladderDirection.z * 6f * Time.deltaTime;
                }

                if (ladderBottom.Contains(transform.position) && inputMovement.y < -0.2f)
                {
                    player.action = Action.None;
                }

                characterController.Move(movement);
            }

            speed = inputMovement.y * 3f;
    }
}
