using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Itens;

public class ItemCollectCoin : ItemCollectBase
{
    protected override void OnCollect(){

        base.OnCollect();
        ItemCollectManager.Instance.AddByType(ItemType.COIN);
        //collider.enable = false;

    }
    
}
