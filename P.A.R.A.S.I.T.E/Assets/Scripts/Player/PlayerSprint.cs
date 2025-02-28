using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprint : MonoBehaviour
{
    [SerializeField]
    private float _maxSprintSpeed = 6.5f;
    [SerializeField]
    private float _sprintAcceleration = 13f;
    [SerializeField]
    private float _sprintDeceleration = 6.5f;

    [SerializeField]
    private InputHandler input;
    [SerializeField]
    private CharacterController characterController;
    [SerializeField]
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        handleSprint();
    }

    void handleSprint()
    {
        if(input._isSprintPressed && input._isMovementPressed && playerController._speed <= _maxSprintSpeed)
        {
            playerController._speed += _sprintAcceleration * Time.deltaTime;
        }

        if (!input._isSprintPressed && playerController._speed >= playerController._maxWalkSpeed)
        {
            playerController._speed -= _sprintDeceleration * Time.deltaTime;
        }
    }
}
