using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;
    public Transform positionProjectile;
    public float timeBetweenShoot = .3f;
    public float speed = 50f;

    private Coroutine _currentCoroutine;
    
    protected virtual IEnumerator StartShoot() {
        while(true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }

    public virtual void Shoot(){
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionProjectile.position;
        projectile.transform.rotation = positionProjectile.rotation;
        projectile.speed = speed;
    }

    public void StartShooting()
    {
        CancelShooting();
        Debug.Log("Starting Shooting...");
        _currentCoroutine = StartCoroutine(StartShoot());
    }

    public void CancelShooting()
    {
        Debug.Log("Cancel Shooting Called");
        if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
    }

}
