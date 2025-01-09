using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Cloth;

public class GunShootLimit : GunBase
{
    public List<UIGunUpdater> gunUpdaters;

    [SerializeField] private float _maxShoot = 5f;

    public float maxShoot
    {
        get => _maxShoot;
        set => _maxShoot = value;
    }

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
            if (_currentShoots < _maxShoot)
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
        if(_currentShoots >= _maxShoot)
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
        gunUpdaters.ForEach(i => i.UpdateValue(_maxShoot,_currentShoots));
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

    #region VELOCIDADE SHOOT LIMIT
       
        public void ChangeShootLimit(float _maxShoot, float duration)
        {
            StartCoroutine(ChangeShootLimitCoroutine(_maxShoot, duration));
        }
        public IEnumerator DisableRechargingForDuration(float duration)
        {
            bool originalState = recharging;
            recharging = false;
            yield return new WaitForSeconds(duration);
            recharging = originalState;
        }

        public IEnumerator ChangeShootLimitCoroutine(float newLimitShoot, float duration)
        {
            StartCoroutine(DisableRechargingForDuration(duration));

            float originalLimitShoot = _maxShoot;
            _maxShoot = newLimitShoot;
            _currentShoots = Mathf.Min(_currentShoots, _maxShoot); // Ajusta o estado atual.

            yield return new WaitForSeconds(duration);

            _maxShoot = originalLimitShoot;
        }


    #endregion
}