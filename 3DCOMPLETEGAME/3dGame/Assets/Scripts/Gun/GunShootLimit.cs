using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GunShootLimit : GunBase
{
    public List<UIGunUpdater> gunUpdaters;

    public float maxShoot = 5f;
    public float timeToRecharge = 1f;


    private float _currentShoots;
    private bool recharging = false;


    private void Awake()
    {
        GetAllUIs();
    }

    protected override IEnumerator StartShoot()
    {
        if(recharging) yield break;

        while (true) 
        {
            if (_currentShoots < maxShoot)
            {
                Shoot();
                _currentShoots++;
                CheckRecharge();
                UpdateUI();
                yield return new WaitForSeconds(timeBetweenShoot);
            }

        }
    }

    private void CheckRecharge()
    {
        if(_currentShoots >= maxShoot)
        {
            CancelShooting();
            StartRecharge();
        }
    }

    private void StartRecharge()
    {
        recharging = true;
        StartCoroutine(RechargeCoroutine());
    }

    IEnumerator RechargeCoroutine()
    {
        float time = 0;
        while (time < timeToRecharge) 
        {
            time += Time.deltaTime;
            gunUpdaters.ForEach(i => i.UpdateValue(time/timeToRecharge));
            yield return new WaitForEndOfFrame();
        }
        _currentShoots = 0;
        recharging = false; 
    }

    private void UpdateUI()
    {
        gunUpdaters.ForEach(i => i.UpdateValue(maxShoot,_currentShoots));
    }

    private void GetAllUIs()
    {
        gunUpdaters = new List<UIGunUpdater>();
    
        GameObject gunObject = GameObject.Find("Gun");
        if (gunObject != null)
        {
            UIGunUpdater gunUpdater = gunObject.GetComponent<UIGunUpdater>();
            if (gunUpdater != null)
            {
                gunUpdaters.Add(gunUpdater);
            }
        }

        //CODIGO ANTIGO
        //gunUpdaters = GameObject.FindObjectsOfType<UIGunUpdater>().ToList();
    }
}
