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

    // Start is called before the first frame update
    void Awake()
    {
        ballisticGun.Reload();
    }

    // Update is called once per frame
    void Update()
    {
        if(input._isFirePressed)
        {
            ballisticGun.Shoot();
        }
        if(input._isReloadPressed)
        {
            ballisticGun.Reload();
        }
    }
}
