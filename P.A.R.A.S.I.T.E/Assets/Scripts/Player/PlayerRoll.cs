using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRoll : MonoBehaviour
{

    [SerializeField] private float _rollSpeed = 15.0f;
    [SerializeField] public float _rollTime = 0.5f;
    [SerializeField] private float _rollDelay = 2.0f;

    private PlayerInput playerInput;
    private CharacterController characterController;
    private PlayerController playerController;
    private Transform characterTransform;
    private PlayerAim playerAim;

    public Vector3 _currentMovement;
    private Quaternion _currentRotation;

    public bool _isMovementPressed;
    public bool _isRollPressed;
    public bool _rolling = false;
    private bool _canRoll = true;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        playerController = GetComponent<PlayerController>();
        playerAim = GetComponent<PlayerAim>();
        characterTransform = GetComponent<Transform>();

        playerInput.CharacterControls.Roll.started += OnRoll;
        playerInput.CharacterControls.Roll.canceled += OnRoll;
    }

    void OnRoll(InputAction.CallbackContext context)
    {
        _isRollPressed = context.ReadValueAsButton();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateValues();
        handleRoll();
    }

    void UpdateValues()
    {
        _isMovementPressed = playerController._isMovementPressed;
        _currentMovement = playerController._currentMovement;
        _currentRotation = playerController._currentRotation;
    }

    void GetCurrentFacing()
    {
        Vector3 _forward = characterTransform.forward;
        _forward.y = 0;
        _forward = Vector3.Normalize(_forward);
        Vector3 _right = Quaternion.Euler(new Vector3(0,90,0)) * _forward;

        _currentMovement = Vector3.Normalize(_forward + _right);
        _currentMovement = Quaternion.Euler(0, -45, 0) * _currentMovement;
    }

    void handleRoll()
    {
        if(_canRoll && _isRollPressed && _isMovementPressed)
        {
            StartCoroutine(roll());
        }
        else if (_canRoll && _isRollPressed)
        {
            StartCoroutine(standingRoll());
        }
    }

    public IEnumerator roll()
    {
        _canRoll = false;
        _rolling = true;
        float elapsedTime = 0.0f;

        Vector3 _rollMovement = _currentMovement;

        StartCoroutine(rollDelay());

        while (elapsedTime < _rollTime)
        {
            characterController.Move(_rollMovement * _rollSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            
            yield return null;
        }

        _rolling = false;
    }

    public IEnumerator standingRoll()
    {
        _canRoll = false;
        _rolling = true;
        float elapsedTime = 0.0f;

        GetCurrentFacing();

        Vector3 _rollMovement = _currentMovement;

        StartCoroutine(rollDelay());

        while (elapsedTime < _rollTime)
        {
            characterController.Move(_rollMovement * _rollSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _currentMovement.z = 0;
        _rolling = false;
    }

    public IEnumerator rollDelay()
    {
        yield return new WaitForSecondsRealtime(_rollDelay);
        _canRoll = true;
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
