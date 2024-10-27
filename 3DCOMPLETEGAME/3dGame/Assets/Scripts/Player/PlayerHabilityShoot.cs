using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerHabilityShoot : PlayerHabilityBase
{
    public GunBase gunBase;

    protected override void Init()
    {
        base.Init();

        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();
    }


    private void StartShoot()
    {
        gunBase.StartShooting();
        Debug.Log("Shoot");
    }

    private void CancelShoot()
    {
        gunBase.CancelShooting();
        Debug.Log("Cancel Shoot");
    }
}
