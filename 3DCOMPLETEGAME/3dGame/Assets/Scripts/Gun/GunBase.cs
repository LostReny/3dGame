using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;
    public Transform positionProjectile;
    public float timeBetweenShoot = .3f;


    private Coroutine _currentCoroutine;

    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            _currentCoroutine = StartCoroutine(StartShoot());
        }
        else if(Input.GetMouseButtonUp(0)){
            if(_currentCoroutine != null) StopCoroutine(_currentCoroutine);
        }
    }
    
    IEnumerator StartShoot() {
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


}
