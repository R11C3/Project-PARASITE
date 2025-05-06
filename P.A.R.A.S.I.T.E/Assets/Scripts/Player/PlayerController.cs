using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float _speed = 0.0f;

    [SerializeField]
    private PlayerStats player;
    [SerializeField]
    private SO_Input _input;
    private CharacterController characterController;

    private Camera _camera;

    [HideInInspector]
    public Vector3 _lastMovement, _currentVelocity;
    [HideInInspector]
    public Quaternion _currentRotation;
    [HideInInspector]
    public Vector3 _forward, _right, _forwardMovement, _rightMovement, _initialMovement;

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
        _camera = Camera.main;

        _lastMovement = new Vector3(0.0f, -9.8f, 0.0f);

        _camera = Camera.main;

        _forward = _camera.transform.forward;
        _forward.y = 0;
        _forward = Vector3.Normalize(_forward);
        _right = Quaternion.Euler(new Vector3(0, 90, 0)) * _forward;
    }

    void HandleMovement()
    {
        _rightMovement = _right * _input._currentMovementInput.x;
        _forwardMovement = _forward * _input._currentMovementInput.y;
        _initialMovement = Vector3.Normalize(_rightMovement + _forwardMovement);
        _input._currentMovement = Vector3.Lerp(_input._currentMovement, _initialMovement, dampening);

        if (_input._isMovementPressed && _speed <= player.speed)
        {
            _speed += player.acceleration * Time.deltaTime;
        }

        if (!_input._isMovementPressed && _speed > 0.0f)
        {
            _speed -= player.deceleration * Time.deltaTime;
        }

        if (_speed < 0.0f)
        {
            _speed = 0.0f;
        }

        if (_speed > player.speed)
        {
            _speed -= player.deceleration * Time.deltaTime;
        }

        if (!_input._isMovementPressed && !_input._isCrouchPressed && !_input._isSprintPressed)
        {
            _input._currentMovement = _lastMovement;
            HandleGravity();
            characterController.Move(new Vector3(_input._currentMovement.x * _speed * Time.deltaTime, _input._currentMovement.y * Time.deltaTime, _input._currentMovement.z * _speed * Time.deltaTime));
        }

        if (_input._isMovementPressed)
        {
            HandleGravity();
            characterController.Move(new Vector3(_input._currentMovement.x * _speed * Time.deltaTime, _input._currentMovement.y * Time.deltaTime, _input._currentMovement.z * _speed * Time.deltaTime));

            _lastMovement = _input._currentMovement;
        }

        _currentVelocity = characterController.velocity;
    }

    void HandleGravity()
    {
        if (characterController.isGrounded)
        {
            float _groundedGravity = -0.5f;
            _input._currentMovement.y = _groundedGravity;
        }
        else
        {
            float _gravity = -9.8f;
            _input._currentMovement.y = _gravity;
        }
    }

    void HandleLadder()
    {
        if (_input._currentMovementInput.y > 0.2f && _input._isMovementPressed)
        {
            Vector3 movement = new Vector3(0, _input._currentMovementInput.y * 3f * Time.deltaTime, 0);

            if (ladderTop.Contains(gameObject.transform.position))
            {
                player.action = Action.None;
                movement.x = ladderDirection.x * 6f * Time.deltaTime;
                movement.y = movement.y * 2f;
                movement.z = ladderDirection.z * 6f * Time.deltaTime;
            }

            characterController.Move(movement);

            
        }
        else if (_input._currentMovementInput.y < 0.2f && _input._isMovementPressed)
        {
            Vector3 movement = new Vector3(0, _input._currentMovementInput.y * 3f * Time.deltaTime, 0);

            if (ladderBottom.Contains(gameObject.transform.position))
            {
                player.action = Action.None;
            }

            characterController.Move(movement);
        }
    }

    void Update()
    {
        if (player.action == Action.None)
        {
            HandleMovement();
        }
        if (player.action == Action.Ladder)
        {
            HandleLadder();
        }
    }
}
