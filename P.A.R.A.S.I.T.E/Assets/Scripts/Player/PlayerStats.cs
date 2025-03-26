using System.Dynamic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float speed;
    public float acceleration;
    public float deceleration;
    public float sprintSpeed;
    public float sprintAcceleration;
    public float sprintDeceleration;
    public float rollSpeed;
    public float rollTime;
    public float rollDelay;

    //will be moved to weapons class
    [SerializeField]
    private ScriptObj_GunData[] weapons = new ScriptObj_GunData[2];

    public ScriptObj_GunData[] playerWeapons = new ScriptObj_GunData[2];

    [SerializeField]
    private InputHandler input;
    [SerializeField]
    private ScriptObj_Mob statsTemplate;
    private ScriptObj_Mob stats;

    void Awake()
    {
        playerWeapons[0] = Instantiate(weapons[0]);
        playerWeapons[1] = Instantiate(weapons[1]);
        stats = Instantiate(statsTemplate);
        InitialStats();
    }

    // Update is called once per frame
    void Update()
    {
        if(input._isMovementPressed)
            MovementPressed();
        if(input._isCrouchPressed)
            CrouchPressed();
    }

    void InitialStats()
    {
        speed = stats.speed;
        acceleration = stats.acceleration;
        deceleration = stats.deceleration;
        sprintSpeed = stats.sprintSpeed;
        sprintAcceleration = stats.acceleration;
        sprintDeceleration = stats.deceleration;
        rollSpeed = stats.rollSpeed;
        rollTime = stats.rollTime;
        rollDelay = stats.rollDelay;
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
