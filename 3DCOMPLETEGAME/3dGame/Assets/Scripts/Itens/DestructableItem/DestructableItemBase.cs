using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestructableItemBase : MonoBehaviour
{
    public HealthBase healthBase;

    public float shakeDuration = .1f;
    public int shakeForce = 1;

    private void OnValidate()
    {
        if(healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    private void Awake()
    {
        OnValidate();
        if (healthBase != null) 
        {
            healthBase.OnDamage += OnDamage;
        }
    }

    private void OnDamage(HealthBase h)
    {
        Debug.Log("On Damage");
        transform.DOShakeScale(shakeDuration, Vector3.up / 4, shakeForce);
    }
}
