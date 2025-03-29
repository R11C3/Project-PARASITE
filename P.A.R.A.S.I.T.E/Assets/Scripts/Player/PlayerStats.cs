using System.Dynamic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    public SO_Player _player;
    [SerializeField]
    private SO_Input _input;

    void Start()
    {
        _player.ClearInventory();
    }

    // Update is called once per frame
    void Update()
    {
        if(_input._isMovementPressed)
            MovementPressed();
        if(_input._isCrouchPressed)
            CrouchPressed();
    }

    private void MovementPressed()
    {
        _player.speed = 2.0f;
    }
    private void CrouchPressed()
    {
        _player.speed = 1.0f;
    }
}
