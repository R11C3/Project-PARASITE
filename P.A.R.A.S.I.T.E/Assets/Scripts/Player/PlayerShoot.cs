using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private InputHandler input;
    [SerializeField]
    private BallisticGun ballisticGun;
    [SerializeField]
    private PlayerStats stats;

    private int ActiveSlot = 0;

    // Start is called before the first frame update
    void Awake()
    {
        ballisticGun.LoadStats();
    }

    // Update is called once per frame
    void Update()
    {
        ballisticGun.UpdateStats();
        if(input._isFirePressed)
        {
            ballisticGun.Shoot();
        }
        if(input._isReloadPressed)
        {
            ballisticGun.Reload();
        }

        SwitchWeapons();
        TrackAmmo();
    }

    void SwitchWeapons()
    {
        if(input._isOnePressed)
        {
            ActiveSlot = 0;
            ballisticGun.currentAmmo = stats.playerWeapons[0].currentAmmo;
            ballisticGun.gunData = stats.playerWeapons[0];
            ballisticGun.LoadStats();
        }
        if(input._isTwoPressed)
        {
            ActiveSlot = 1;
            ballisticGun.currentAmmo = stats.playerWeapons[1].currentAmmo;
            ballisticGun.gunData = stats.playerWeapons[1];
            ballisticGun.LoadStats();
        }
    }

    void TrackAmmo()
    {
        stats.playerWeapons[ActiveSlot].currentAmmo = ballisticGun.currentAmmo;
    }
}
