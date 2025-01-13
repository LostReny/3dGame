using Animation;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Cloth;

public class HealthBase : MonoBehaviour, IDamagable
{
    public float startLife = 10f;

    [Header("UI")]
    public UIGunUpdater uIUpdater;

    public bool destroyOnKill = false;

    [SerializeField] private float _currentLife;

    public float currentLife
    {
        get
        {
            return _currentLife;
        }
        set
        {
            _currentLife = value;
        }
    }

    public float damageMultipliyer = 1f;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;


    public void Awake()
    {
        Init();
    }

    public void Init()
    {
        ResetLife();
        SaveManager.Instance.LoadCurrentLife();
    }

    public void ResetLife()
    {
        _currentLife = startLife;
    }

    protected virtual void Kill()
    {
        if (destroyOnKill)
        {
            Destroy(gameObject, 3f);
        }

        OnKill?.Invoke(this);
    }

    [NaughtyAttributes.Button]
    public void Ddamage()
    {
        Damage(5f);
    }

    public void Damage(float f)
    {

        _currentLife -= f * damageMultipliyer;

        if (_currentLife <= 0)
        {
            Kill();
        }

        UpdateUi();
        OnDamage?.Invoke(this);
    }

    public void Damage(float damage, Vector3 dir)
        {
            Damage(damage);
        }

    #region UI
    public void UpdateUi()
    {
        if(uIUpdater != null)
        {
            uIUpdater.UpdateValue((float)_currentLife/startLife);
        }
    }
    #endregion


    #region MENOS DANO
       
        public void ChangeDamageMultiplay(float damageMultipliyer, float duration)
        {
            StartCoroutine(ChangeDamageCoroutine(damageMultipliyer, duration));
        }

        public IEnumerator ChangeDamageCoroutine(float damageMultipliyer, float duration)
        {
            this.damageMultipliyer = damageMultipliyer;
            yield return new WaitForSeconds(duration);    
            this.damageMultipliyer = 1;
        }


    #endregion
}
