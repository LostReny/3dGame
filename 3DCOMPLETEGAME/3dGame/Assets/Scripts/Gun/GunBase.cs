using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//using Cloth;

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

    public virtual void StartShooting()
    {
        CancelShooting();
        _currentCoroutine = StartCoroutine(StartShoot());
    }

    public void CancelShooting()
    {
        if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
    }

    /*#region VELOCIDADE AO PEGAR ITEM
       
        public void ChangeShootSpeed(float timeBetweenShoot, float duration)
        {
            StartCoroutine(ChangeShootCoroutine(timeBetweenShoot, duration));
        }

        IEnumerator ChangeShootCoroutine(float newTimeBetweenShoot, float duration)
        {
            float originalTimeBetweenShoot = timeBetweenShoot;
            timeBetweenShoot = newTimeBetweenShoot;       

            yield return new WaitForSeconds(duration);    

            timeBetweenShoot = originalTimeBetweenShoot;
        }
    */

        //#endregion

}