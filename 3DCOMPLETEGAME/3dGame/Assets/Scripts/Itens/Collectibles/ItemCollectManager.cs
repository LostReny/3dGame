using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LostWordls.Singleton;

public class ItemCollectManager : Singleton<ItemCollectManager>
{
    public SOInt coins;

    private void Awake() {

        if(Instance == null)
        Instance = this;
        else 
        Destroy(gameObject);
       
    }

    private void Update() {
        
    }

    private void Start() {
        Reset();    
    }

    private void Reset() {
        AddCoins();
        coins.value = 0;
    }

    public void AddCoins(int amount = 1) {
        coins.value += amount;
    }

}
