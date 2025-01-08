using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Itens;

public class ChestItemCoin : ChestItemBase
{
    public int coinNumber = 5;
    public GameObject coinObject;

    private List<GameObject> _items = new List<GameObject>();

    public Vector2 randomRange = new Vector2(-2f, 2f);

    public float tweenEndTime = .5f;

    public override void ShowItem()
    {
        base.ShowItem();
        CreateItems();
    }




    [NaughtyAttributes.Button]
    private void CreateItems()
    {
        for (int i = 0; i < coinNumber; i++) 
        {
            var item = Instantiate(coinObject);
            item.transform.position = transform.position + Vector3.forward * Random.Range(randomRange.x, randomRange.y) + Vector3.right * Random.Range(randomRange.x, randomRange.y);
            item.transform.DOScale(0, .2f).SetEase(Ease.OutBack).From();
            item.transform.DORotate(new Vector3(0, 360, 0), 2f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
            _items.Add(item);
        }
    }

    [NaughtyAttributes.Button]
    public override void Collect()
    {
        base.Collect();
        foreach(var i in _items)
        {
            i.transform.DOKill();
            i.transform.DOMoveY(2f, tweenEndTime).SetRelative();
            i.transform.DOScale(0, tweenEndTime / 2 ).SetDelay(tweenEndTime / 2);
            ItemCollectManager.Instance.AddByType(ItemType.COIN);
        }

        Invoke("DestroyGO", 3.5f);
    }

    
     private void DestroyGO()
    {
        foreach (var item in _items)
        {
            if (item != null) 
            {
                Destroy(item);
            }
        
        }

        _items.Clear();
    }
    

}
