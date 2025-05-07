using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Rendering;

public class PlayerUpperAnimationHandler : MonoBehaviour
{
    private Animator animator;
    private PlayerStats player;

    public GameObject gun;
    private BallisticGun gunData;
    public GameObject leftHandRig;
    public GameObject rightHandRig;
    public GameObject headAim;
    public GameObject bodyAim;

    private int holdingWeaponHash;
    private int holdingNothingHash;

    private int switchingWeaponHash;
    private int reloadingWeaponHash;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<PlayerStats>();
        gunData = gun.GetComponent<BallisticGun>();

        holdingWeaponHash = Animator.StringToHash("holdingWeapon");
        holdingNothingHash = Animator.StringToHash("holdingNothing");

        switchingWeaponHash = Animator.StringToHash("switchingWeapon");
        reloadingWeaponHash = Animator.StringToHash("reloadingWeapon");
    }

    // Update is called once per frame
    void Update()
    {
        Holding();
        ActionRestrictions();
    }

    void ActionRestrictions()
    {
        if(player.action == Action.Ladder)
        {
            headAim.GetComponent<MultiAimConstraint>().weight = 0.8f;
            bodyAim.GetComponent<MultiAimConstraint>().weight = 0.0f;
            animator.SetLayerWeight(1, 0f);
            rightHandRig.GetComponent<MultiAimConstraint>().weight = 0f;
            leftHandRig.GetComponent<TwoBoneIKConstraint>().weight = 0f;
            leftHandRig.GetComponent<TwoBoneIKConstraint>().data.hintWeight = 0f;
        }
        else if(player.equipmentInventory.equipped != WeaponSlot.None)
        {
            headAim.GetComponent<MultiAimConstraint>().weight = 1f;
            bodyAim.GetComponent<MultiAimConstraint>().weight = 0.625f;
            animator.SetLayerWeight(1, 1f);
            rightHandRig.GetComponent<MultiAimConstraint>().weight = 1f;
            leftHandRig.GetComponent<TwoBoneIKConstraint>().weight = 0.8f;
            leftHandRig.GetComponent<TwoBoneIKConstraint>().data.hintWeight = 0.8f;
        }
    }

    void Holding()
    {
        WeaponSlot currentWeapon = player.equipmentInventory.equipped;


        if(currentWeapon == WeaponSlot.None)
        {
            animator.SetBool(holdingWeaponHash, false);
            animator.SetBool(holdingNothingHash, true);
            animator.SetLayerWeight(1, 0f);
            rightHandRig.GetComponent<MultiAimConstraint>().weight = 0f;
            leftHandRig.GetComponent<TwoBoneIKConstraint>().weight = 0f;
            leftHandRig.GetComponent<TwoBoneIKConstraint>().data.hintWeight = 0f;
        }
        if(currentWeapon != WeaponSlot.None)
        {
            animator.SetBool(holdingWeaponHash, true);
            animator.SetBool(holdingNothingHash, false);
            animator.SetLayerWeight(1, 1f);
            rightHandRig.GetComponent<MultiAimConstraint>().weight = 1f;
            leftHandRig.GetComponent<TwoBoneIKConstraint>().weight = 0.8f;
            leftHandRig.GetComponent<TwoBoneIKConstraint>().data.hintWeight = 0.8f;
        }

        if(player.changingWeapons)
        {
            player.changingWeapons = false;
            rightHandRig.GetComponent<MultiAimConstraint>().weight = 0.3f;
            leftHandRig.GetComponent<TwoBoneIKConstraint>().weight = 0.3f;
            leftHandRig.GetComponent<TwoBoneIKConstraint>().data.hintWeight = 0.1f;
            animator.SetBool(switchingWeaponHash, true);
            StartCoroutine(SwitchWeapons(1));
        }
        if(player.reloading)
        {
            rightHandRig.GetComponent<MultiAimConstraint>().weight = 0.3f;
            leftHandRig.GetComponent<TwoBoneIKConstraint>().weight = 0.3f;
            leftHandRig.GetComponent<TwoBoneIKConstraint>().data.hintWeight = 0.1f;
            animator.SetBool(reloadingWeaponHash, true);
            StartCoroutine(Reload(gunData.gun.stats.reloadTime));
        }
    }

    IEnumerator SwitchWeapons(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        animator.SetBool(switchingWeaponHash, false);
        rightHandRig.GetComponent<MultiAimConstraint>().weight = 1f;
        leftHandRig.GetComponent<TwoBoneIKConstraint>().weight = 0.8f;
        leftHandRig.GetComponent<TwoBoneIKConstraint>().data.hintWeight = 0.8f;
    }

    IEnumerator Reload(float time)
    {
        yield return new WaitForSeconds(time);
        animator.SetBool(reloadingWeaponHash, false);
        rightHandRig.GetComponent<MultiAimConstraint>().weight = 1f;
        leftHandRig.GetComponent<TwoBoneIKConstraint>().weight = 0.8f;
        leftHandRig.GetComponent<TwoBoneIKConstraint>().data.hintWeight = 0.8f;
    }
}
