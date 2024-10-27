using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;
    public Transform positionProjectile;
    public float timeBetweenShoot = .3f;


    private Coroutine _currentCoroutine;
    
    protected virtual IEnumerator StartShoot() {
        while(true){
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }

    public void Shoot(){
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionProjectile.position;
        projectile.transform.rotation = positionProjectile.rotation;
    }

    public void StartShooting()
    {
        CancelShooting();
        _currentCoroutine = StartCoroutine(StartShoot());
    }

    public void CancelShooting()
    {
        if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
    }
}
