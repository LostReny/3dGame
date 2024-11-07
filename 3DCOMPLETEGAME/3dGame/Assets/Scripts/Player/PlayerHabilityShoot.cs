using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerHabilityShoot : PlayerHabilityBase
{

    public GunBase gunBase;
    public Transform gunPosition;

    // pega o prefab da arma?
    public GunShootLimit weapon1;
    public GunShootAngle weapon2;

    private GunBase _currentGun;

    protected override void Init()
    {
        base.Init();

        CreateGun();

        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();
        inputs.Gameplay.ChooseWeapon1.performed += ctx => ChoseGun1();
        inputs.Gameplay.ChooseWeapon2.performed += ctx => ChoseGun2();
    }

    private void CreateGun()
    {
        _currentGun = Instantiate(gunBase,gunPosition);
        _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;

    }

    private void ChoseGun1()
    {
        if (_currentGun != null)
        {
            Destroy(_currentGun.gameObject);
        }
        _currentGun = Instantiate(weapon1, gunPosition).GetComponent<GunBase>();
        _currentGun.transform.localPosition = Vector3.zero;
        _currentGun.transform.localEulerAngles = Vector3.zero;
        Debug.Log("Weapon 1 Chosen");
    }

    private void ChoseGun2()
    {
        if (_currentGun != null)
        {
            Destroy(_currentGun.gameObject);
        }
        _currentGun = Instantiate(weapon2, gunPosition).GetComponent<GunBase>();
        _currentGun.transform.localPosition = Vector3.zero;
        _currentGun.transform.localEulerAngles = Vector3.zero;
        Debug.Log("Weapon 2 Chosen");
    }

    private void StartShoot()
    {
        _currentGun.StartShooting();
        Debug.Log("Shoot");
    }

    private void CancelShoot()
    {
        _currentGun.CancelShooting();
        Debug.Log("Cancel Shoot");
    }
}
