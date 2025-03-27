using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprint : MonoBehaviour
{
    [SerializeField]
    private SO_Player _player;
    [SerializeField]
    private SO_Input _input;
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
        HandleSprint();
    }

    void HandleSprint()
    {
        if(_input._isSprintPressed && _input._isMovementPressed && playerController._speed <= _player.sprintSpeed)
        {
            playerController._speed += _player.acceleration * Time.deltaTime;
        }

        if (!_input._isSprintPressed && playerController._speed >= _player.speed)
        {
            playerController._speed -= _player.deceleration * Time.deltaTime;
        }
    }
}
