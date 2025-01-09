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
    public GameObject weapon1Prefab;
    public GameObject weapon2Prefab;

    private GunBase _currentGun;

    private bool _isShooting;

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
        if (_currentGun != null) Destroy(_currentGun.gameObject); 
        _currentGun = Instantiate(weapon1Prefab, gunPosition).GetComponent<GunBase>();
        _currentGun.transform.localPosition = Vector3.zero;
        _currentGun.transform.localEulerAngles = Vector3.zero;
    }

    private void ChoseGun1()
    {
        if (_currentGun != null) Destroy(_currentGun.gameObject); 
        _currentGun = Instantiate(weapon1Prefab, gunPosition).GetComponent<GunBase>();
        _currentGun.transform.localPosition = Vector3.zero;
        _currentGun.transform.localEulerAngles = Vector3.zero;
        Debug.Log("Weapon 1 Chosen");
    }

    private void ChoseGun2()
    {
        if (_currentGun != null) Destroy(_currentGun.gameObject); 
        _currentGun = Instantiate(weapon2Prefab, gunPosition).GetComponent<GunBase>();
        _currentGun.transform.localPosition = Vector3.zero;
        _currentGun.transform.localEulerAngles = Vector3.zero;
        Debug.Log("Weapon 2 Chosen");
    }


        private void StartShoot()
    {
        if (_isShooting || _currentGun == null) return;
        _isShooting = true;
        _currentGun.StartShooting();
    }

    private void CancelShoot()
    {
        if (!_isShooting || _currentGun == null) return;
        _isShooting = false;
        _currentGun.CancelShooting();
    }


}
