using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectCoin : ItemCollectBase
{
    protected override void OnCollect(){

        base.OnCollect();
        ItemCollectManager.Instance.AddCoins();

    }
    
}
