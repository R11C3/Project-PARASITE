using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private BallisticGun gun;
    [SerializeField]
    private SO_Input _input;
    private PlayerStats player;

    private bool fireSelectHeld = false;

    // Start is called before the first frame update
    void Awake()
    {
        player = GetComponent<PlayerStats>();
        SO_Gun current = player.equipmentInventory.EquippedGun();
        if(current != null)
        {
            gun.gunData = current;
            gun.LoadStats();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_input._isFirePressed && gun.gunData != null)
        {
            gun.Shoot();
            gun.fireHeld = true;
        }
        if(_input._isReloadPressed && gun.gunData != null)
        {
            player.reloading = true;
            gun.Reload();
        }
        if(!_input._isFirePressed)
        {
            gun.fireHeld = false;
        }

        if(_input._isFireModePressed && gun.gunData != null && !fireSelectHeld)
        {
            gun.gunData.ChangeFireMode();
            fireSelectHeld = true;
        }
        if(!_input._isFireModePressed)
        {
            fireSelectHeld = false;
        }

        SwitchWeapons();
    }

    void SwitchWeapons()
    {
        SO_Gun swapTo;
        if(_input._isOnePressed)
        {
            swapTo = player.equipmentInventory.GetGun(WeaponSlot.Primary);
            if(swapTo != null)
            {
                player.changingWeapons = true;
                StartCoroutine(DelaySwitch(0));
            }
        }
        if(_input._isTwoPressed)
        {
            swapTo = player.equipmentInventory.GetGun(WeaponSlot.Sling);
            if(swapTo != null)
            {
                player.changingWeapons = true;
                StartCoroutine(DelaySwitch(WeaponSlot.Sling));
            }
        }
        if(_input._isThreePressed)
        {
            swapTo = player.equipmentInventory.GetGun(WeaponSlot.Holster);
            if(swapTo != null)
            {
                player.changingWeapons = true;
                StartCoroutine(DelaySwitch(WeaponSlot.Holster));
            }
        }
    }

    IEnumerator DelaySwitch(WeaponSlot slot)
    {
        gun.switching = true;
        player.equipmentInventory.SwitchGun(slot);
        gun.gunData = player.equipmentInventory.EquippedGun();
        yield return new WaitForSecondsRealtime(0.69f);
        GameObject.Find("Gun").GetComponent<Renderer>().enabled = false;
        yield return new WaitForSecondsRealtime(0.716f);
        GameObject.Find("Gun").GetComponent<Renderer>().enabled = true;
        gun.LoadStats();
        gun.switching = false;
    }
}
