using System.Dynamic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float speed = 2.0f;
    public float acceleration = 16.0f;
    public float deceleration = 16.0f;
    public float sprintSpeed = 5.0f;
    public float sprintAcceleration = 16.0f;
    public float sprintDeceleration = 16.0f;
    public float rollSpeed = 15.0f;
    public float rollTime = 0.5f;
    public float rollDelay = 2.0f;

    //will be moved to weapons class
    [SerializeField]
    private ScriptObj_GunData[] weapons = new ScriptObj_GunData[2];

    public ScriptObj_GunData[] playerWeapons = new ScriptObj_GunData[2];

    [SerializeField]
    private InputHandler input;

    void Awake()
    {
        playerWeapons[0] = Instantiate(weapons[0]);
        playerWeapons[1] = Instantiate(weapons[1]);
    }

    // Update is called once per frame
    void Update()
    {
        if(input._isMovementPressed)
            MovementPressed();
        if(input._isCrouchPressed)
            CrouchPressed();
    }

    void MovementPressed()
    {
        speed = 2.0f;
    }
    void CrouchPressed()
    {
        speed = 1.0f;
    }
}
