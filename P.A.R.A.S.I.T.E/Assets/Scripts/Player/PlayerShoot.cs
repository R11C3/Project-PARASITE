using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private BallisticGun ballisticGun;
    [SerializeField]
    private SO_Player _player;
    [SerializeField]
    private SO_Input _input;

    // Start is called before the first frame update
    void Awake()
    {
        ballisticGun.gunData = _player.weaponInventory[0];
        ballisticGun.LoadStats();
        _player.activeSlot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(_input._isFirePressed)
        {
            ballisticGun.Shoot();
        }
        if(_input._isReloadPressed)
        {
            ballisticGun.Reload();
        }

        SwitchWeapons();
    }

    void SwitchWeapons()
    {
        if(_input._isOnePressed)
        {
            ballisticGun.gunData = _player.weaponInventory[0];
            ballisticGun.LoadStats();
            _player.activeSlot = 0;
        }
        if(_input._isTwoPressed)
        {
            ballisticGun.gunData = _player.weaponInventory[1];
            ballisticGun.LoadStats();
            _player.activeSlot = 1;
        }
    }
}
