using System.Collections;
using UnityEngine;

public class PlayerUpperAnimationHandler : MonoBehaviour
{
    private Animator animator;
    private PlayerStats player;

    private int holdingRifleHash;
    private int holdingPistolHash;
    private int holdingNothingHash;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<PlayerStats>();

        holdingRifleHash = Animator.StringToHash("holdingRifle");
        holdingPistolHash = Animator.StringToHash("holdingPistol");
        holdingNothingHash = Animator.StringToHash("holdingNothing");
    }

    // Update is called once per frame
    void Update()
    {
        Holding();
    }

    void Holding()
    {
        SO_Gun currentWeapon = player.equipmentInventory.EquippedGun();

        if(currentWeapon == null)
        {
            animator.SetBool(holdingPistolHash, false);
            animator.SetBool(holdingRifleHash, false);
            animator.SetBool(holdingNothingHash, true);
        }
        else if(currentWeapon.gunType == GunType.Rifle)
        {
            animator.SetBool(holdingPistolHash, false);
            animator.SetBool(holdingRifleHash, true);
            animator.SetBool(holdingNothingHash, false);
        }
        else if(currentWeapon.gunType == GunType.Pistol)
        {
            animator.SetBool(holdingPistolHash, true);
            animator.SetBool(holdingRifleHash, false);
            animator.SetBool(holdingNothingHash, false);
        }

        if(player.changingWeapons)
        {
            player.changingWeapons = false;
            StartCoroutine(DisableKinematics(2));
        }
    }

    IEnumerator DisableKinematics(float time)
    {
        yield return new WaitForSecondsRealtime(time);
    }
}
