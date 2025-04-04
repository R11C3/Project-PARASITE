using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private BallisticGun gun;
    [SerializeField]
    private SO_Input _input;
    private PlayerStats player;

    // Start is called before the first frame update
    void Awake()
    {
        player = GetComponent<PlayerStats>();
        player.activeSlot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(_input._isFirePressed && gun.gunData != null)
        {
            gun.Shoot();
        }
        if(_input._isReloadPressed && gun.gunData != null)
        {
            gun.Reload();
        }

        SwitchWeapons();
    }

    void SwitchWeapons()
    {
        SO_Gun swapTo;
        if(_input._isOnePressed)
        {
            swapTo = player.weaponInventory.Get(0);
            if(swapTo != null)
            {
                gun.gunData = swapTo;
                gun.LoadStats();
            }
            player.activeSlot = 0;
        }
        if(_input._isTwoPressed)
        {
            swapTo = player.weaponInventory.Get(1);
            if(swapTo != null)
            {
                gun.gunData = swapTo;
                gun.LoadStats();
            }
            player.activeSlot = 1;
        }
    }
}
