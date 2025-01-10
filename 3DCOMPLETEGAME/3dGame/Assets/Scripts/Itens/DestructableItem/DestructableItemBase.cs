using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Enemy;

public class DestructableItemBase : MonoBehaviour
{
    public HealthBase healthBase;

    public float shakeDuration = .1f;
    public int shakeForce = 1;

    public MeshRenderer meshRenderer;

    [Header("Coins")]
    public int dropCoinsAmount = 10;
    public GameObject coinPrefab;
    public Transform posCoin;
    public int LimitOfDrop;
    public bool canDrop = true;

    [Header("VFX")]
    public ParticleSystem _damageVFX;

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

    public void Start()
    {   
        canDrop = LimitOfDrop > 0;
        TurnItOn();
    }

    private void OnDamage(HealthBase h)
    {
        //Debug.Log("On Damage");
        transform.DOShakeScale(shakeDuration, Vector3.up / 4, shakeForce);
        DropCoins();
        if(_damageVFX != null) _damageVFX.Play();
    }

    #region COIN

    [NaughtyAttributes.Button]
    public void DropCoins()
    {
        if(!canDrop) return;

        for (int i = 0; i < dropCoinsAmount; i++)
        {
            var coin = Instantiate(coinPrefab, posCoin.position, Quaternion.identity);
            
            Rigidbody rb = coin.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 randomForce = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(0.1f, 0.2f), Random.Range(-0.2f, 0.2f));
                rb.AddForce(randomForce, ForceMode.Impulse);
            }
            coin.transform.DOScale(0, .5f).SetEase(Ease.OutBack).From();
        }

        LimitOfCoinsDrop();

    }


    public void LimitOfCoinsDrop()
    {
        LimitOfDrop--;
        if (LimitOfDrop <= 0)
        {
            canDrop = false;
            TurnItOff();
        }
    }
    #endregion  

    #region MESH FEEDBACK
    private void TurnItOn()
   {
        meshRenderer.material.SetFloat("_Metallic", 0.902f);
   } 
    private void TurnItOff()
   {
        meshRenderer.material.SetFloat("_Metallic", 0.0f);
   } 


    #endregion
}
