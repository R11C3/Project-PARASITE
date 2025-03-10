using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprint : MonoBehaviour
{
    [SerializeField]
    private InputHandler input;
    [SerializeField]
    private PlayerStats playerStats;
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
        if(input._isSprintPressed && input._isMovementPressed && playerController._speed <= playerStats.sprintSpeed)
        {
            playerController._speed += playerStats.sprintAcceleration * Time.deltaTime;
        }

        if (!input._isSprintPressed && playerController._speed >= playerStats.speed)
        {
            playerController._speed -= playerStats.sprintDeceleration * Time.deltaTime;
        }
    }
}
