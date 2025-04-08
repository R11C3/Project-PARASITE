using System.Collections;
using UnityEngine;

public class PlayerUpperAnimationHandler : MonoBehaviour
{
    private Animator animator;
    private PlayerStats player;

    private int holdingWeaponHash;
    private int holdingNothingHash;

    private int switchingWeaponHash;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<PlayerStats>();

        holdingWeaponHash = Animator.StringToHash("holdingWeapon");
        holdingNothingHash = Animator.StringToHash("holdingNothing");

        switchingWeaponHash = Animator.StringToHash("switchingWeapon");
    }

    // Update is called once per frame
    void Update()
    {
        Holding();
    }

    void Holding()
    {
        WeaponSlot currentWeapon = player.equipmentInventory.equipped;


        if(currentWeapon == WeaponSlot.None)
        {
            animator.SetBool(holdingWeaponHash, false);
            animator.SetBool(holdingNothingHash, true);
        }
        if(currentWeapon != WeaponSlot.None)
        {
            animator.SetBool(holdingWeaponHash, true);
            animator.SetBool(holdingNothingHash, false);
        }

        if(player.changingWeapons)
        {
            player.changingWeapons = false;
            animator.SetBool(switchingWeaponHash, true);
            StartCoroutine(DisableKinematics(1));
        }
    }

    IEnumerator DisableKinematics(float time)
    {
        yield return new WaitForSecondsRealtime(time);
            animator.SetBool(switchingWeaponHash, false);
    }
}
