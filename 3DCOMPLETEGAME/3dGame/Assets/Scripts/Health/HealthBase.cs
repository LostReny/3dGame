using Animation;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamagable
{
    public float startLife = 10f;

    [Header("UI")]
    public UIGunUpdater uIUpdater;

    public bool destroyOnKill = false;

    [SerializeField] private float _currentLife;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;


    public void Awake()
    {
        Init();
    }

    public void Init()
    {
        ResetLife();
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

        _currentLife -= f;

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

}
