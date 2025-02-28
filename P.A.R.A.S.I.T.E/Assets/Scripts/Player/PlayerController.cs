using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [HideInInspector]
    public float _speed = 0.0f;
    [SerializeField]
    public float _maxWalkSpeed = 2.0f;
    [SerializeField]
    private float _acceleration = 6.0f;
    [SerializeField]
    private float _deceleration = 13.0f;

    [SerializeField]
    private InputHandler input;
    private CharacterController characterController;
    private PlayerRoll playerRoll;

    [HideInInspector]
    public Vector3 _lastMovement, _currentVelocity;
    [HideInInspector]
    public Quaternion _currentRotation;

    private bool _rolling;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerRoll = GetComponent<PlayerRoll>();

        _lastMovement = new Vector3(0.0f, -9.8f, 0.0f);
    }

    void UpdateValues()
    {
        _rolling = playerRoll._rolling;
    }

    void handleMovement()
    {
        if(input._isMovementPressed && _speed <= _maxWalkSpeed)
        {
            _speed += _acceleration * Time.deltaTime;
        }

        if(!input._isMovementPressed && _speed > 0.0f)
        {
            _speed -= _deceleration * Time.deltaTime;
        }

        if(_speed < 0.0f)
        {
            _speed = 0.0f;
        }

        if (!input._isMovementPressed)
        {
            input._currentMovement = _lastMovement;
            characterController.Move(new Vector3(input._currentMovement.x * _speed * Time.deltaTime, input._currentMovement.y * Time.deltaTime, input._currentMovement.z * _speed * Time.deltaTime));
        }

        if (!_rolling && input._isMovementPressed)
        {
            characterController.Move(new Vector3(input._currentMovement.x * _speed * Time.deltaTime, input._currentMovement.y * Time.deltaTime, input._currentMovement.z * _speed * Time.deltaTime));

            _lastMovement = input._currentMovement;
        }

        _currentVelocity = characterController.velocity;
    }

    void handleGravity()
    {
        if (characterController.isGrounded)
        {
            float _groundedGravity = -0.5f;
            input._currentMovement.y = _groundedGravity;
        }
        else
        {
            float _gravity = -9.8f;
            input._currentMovement.y = _gravity;
        }
    }

    void Update()
    {
        UpdateValues();
        handleGravity();
        handleMovement();
    }
}
