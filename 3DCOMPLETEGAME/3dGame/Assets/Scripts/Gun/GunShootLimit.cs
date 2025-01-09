using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootLimit : GunBase
{
    public List<UIGunUpdater> gunUpdaters;

    [SerializeField] 
    private float _maxShoot = 5f; // Valor inicial do limite de disparos

    public float MaxShoot
    {
        get => _maxShoot;
        set
        {
            _maxShoot = value;
            Debug.Log($"MaxShoot updated to: {_maxShoot}");
            CheckRecharge(); // Verifica se o limite foi atingido após a mudança
        }
    }

    public float timeToRecharge = 1f;

    private float _currentShoots;
    private bool recharging = false;

    private void Awake()
    {
        GetAllUIs();
    }

    // O método OnValidate será chamado sempre que o valor for alterado no Inspector
    private void OnValidate()
    {
        Debug.Log("OnValidate called. MaxShoot is: " + _maxShoot);
        MaxShoot = _maxShoot;  // Certifica-se de que o valor em MaxShoot será atualizado no editor
    }

    protected override IEnumerator StartShoot()
    {
        if (recharging) yield break;

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
        if (_currentShoots >= _maxShoot)
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
            gunUpdaters.ForEach(i => i.UpdateValue(time / timeToRecharge));
            yield return new WaitForEndOfFrame();
        }
        _currentShoots = 0;
        recharging = false; 
    }

    private void UpdateUI()
    {
        gunUpdaters.ForEach(i => i.UpdateValue(_maxShoot, _currentShoots));
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
    }

    #region VELOCIDADE SHOOT LIMIT
    public void ChangeShootLimit(float newMaxShoot, float duration)
    {
        MaxShoot = newMaxShoot;  // Atualiza o valor de _maxShoot de maneira dinâmica
        StartCoroutine(ChangeShootLimitCoroutine(newMaxShoot, duration));
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
        MaxShoot = newLimitShoot;
        _currentShoots = Mathf.Min(_currentShoots, _maxShoot); // Ajusta o estado atual.

        yield return new WaitForSeconds(duration);

        MaxShoot = originalLimitShoot;  // Restaura o valor original após o tempo
    }
    #endregion
}
