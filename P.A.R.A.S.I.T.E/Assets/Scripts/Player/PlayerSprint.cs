using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprint : MonoBehaviour
{
    [SerializeField]
    private PlayerStats player;
    [SerializeField]
    private SO_Input _input;
    [SerializeField]
    private CharacterController characterController;
    [SerializeField]
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleSprint();
    }

    void HandleSprint()
    {
        if(_input._isSprintPressed && _input._isMovementPressed && playerController._speed <= player.sprintSpeed)
        {
            playerController._speed += player.acceleration * Time.deltaTime;
        }

        if (!_input._isSprintPressed && playerController._speed >= player.speed)
        {
            playerController._speed -= player.deceleration * Time.deltaTime;
        }
    }
}
