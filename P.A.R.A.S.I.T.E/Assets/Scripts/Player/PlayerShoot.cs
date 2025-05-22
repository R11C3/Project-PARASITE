using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    public BallisticGun gun;
    [SerializeField]
    private SO_Input input;
    private PlayerStats player;

    private bool fireSelectHeld = false;

    // Start is called before the first frame update
    void Awake()
    {
        player = GetComponent<PlayerStats>();
        SO_Gun current = player.equipmentInventory.EquippedGun();
        if (current != null)
        {
            gun.gun = current;
            gun.LoadStats();
        }
    }

    void OnEnable()
    {
        input.FireEvent += OnFire;
        input.FireCanceledEvent += OnFireCanceled;
        input.FireModeEvent += OnFireSelect;
        input.FireModeCanceledEvent += OnFireSelectCanceled;
        input.AimEvent += OnAim;
        input.AimCanceledEvent += OnAimCanceled;
        input.OneEvent += OnOne;
        input.OneCanceledEvent += OnOneCanceled;
        input.TwoEvent += OnTwo;
        input.TwoCanceledEvent += OnTwoCanceled;
        input.ThreeEvent += OnThree;
        input.ThreeCanceledEvent += OnThreeCanceled;
    }

    void OnDisable()
    {
        input.FireEvent -= OnFire;
        input.FireCanceledEvent -= OnFireCanceled;
        input.FireModeEvent -= OnFireSelect;
        input.FireModeCanceledEvent -= OnFireSelectCanceled;
        input.AimEvent -= OnAim;
        input.AimCanceledEvent -= OnAimCanceled;
        input.OneEvent -= OnOne;
        input.OneCanceledEvent -= OnOneCanceled;
        input.TwoEvent -= OnTwo;
        input.TwoCanceledEvent -= OnTwoCanceled;
        input.ThreeEvent -= OnThree;
        input.ThreeCanceledEvent -= OnThreeCanceled;
    }

    void OnFire()
    {
        player.equipmentInventory.CheckNone();
        if (player.action == Action.None && player.stance != Stance.Running)
        {
            if (gun.gun != null)
            {
                gun.fireHeld = true;
                gun.Shoot();
            }
        }
    }

    void OnFireCanceled()
    {
        gun.fireHeld = false;
    }

    void OnFireSelect()
    {
        player.equipmentInventory.CheckNone();
        if (player.action == Action.None && player.stance != Stance.Running && !fireSelectHeld)
        {
            gun.gun.ChangeFireMode();
            fireSelectHeld = true;
        }
    }

    void OnFireSelectCanceled()
    {
        fireSelectHeld = false;
    }

    void OnAim()
    {
        player.equipmentInventory.CheckNone();
        if (player.action == Action.None && player.stance != Stance.Running)
        {
            gun.aiming = true;
        }

    }

    void OnAimCanceled()
    {
        gun.aiming = false;
    }

    void OnOne()
    {
        SO_Gun swapTo;
        swapTo = player.equipmentInventory.GetGun(WeaponSlot.Primary);
        if (swapTo != null)
        {
            player.changingWeapons = true;
            StartCoroutine(DelaySwitch(WeaponSlot.Primary));
            gun.gun.CalculateWeaponStats();
        }
    }

    void OnOneCanceled()
    {

    }

    void OnTwo()
    {
        SO_Gun swapTo;
        swapTo = player.equipmentInventory.GetGun(WeaponSlot.Sling);
        if (swapTo != null)
        {
            player.changingWeapons = true;
            StartCoroutine(DelaySwitch(WeaponSlot.Sling));
            gun.gun.CalculateWeaponStats();
        }
    }

    void OnTwoCanceled()
    {

    }

    void OnThree()
    {
        SO_Gun swapTo;
        swapTo = player.equipmentInventory.GetGun(WeaponSlot.Holster);
        if (swapTo != null)
        {
            player.changingWeapons = true;
            StartCoroutine(DelaySwitch(WeaponSlot.Holster));
            gun.gun.CalculateWeaponStats();
        }
    }

    void OnThreeCanceled()
    {

    }

    IEnumerator DelaySwitch(WeaponSlot slot)
    {
        gun.switching = true;
        player.equipmentInventory.SwitchGun(slot);
        gun.gun = player.equipmentInventory.EquippedGun();
        yield return new WaitForSecondsRealtime(0.69f);
        GameObject.Find("Gun").GetComponent<Renderer>().enabled = false;
        yield return new WaitForSecondsRealtime(0.716f);
        GameObject.Find("Gun").GetComponent<Renderer>().enabled = true;
        gun.LoadStats();
        gun.switching = false;
    }
}
