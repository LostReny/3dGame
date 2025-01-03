using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectLife : ItemCollectBase
{
    protected override void OnCollect()
    {
        base.OnCollect();
        ItemCollectManagerLife.Instance.AddLife();
    }
}
