using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using LostWordls.Singleton;
using System.Drawing;

public class ItemCollectManagerLife : Singleton<ItemCollectManagerLife>
{
    public SOInt playerLife;

    private void Awake()
    {

        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

    }


    private void Reset()
    {
        AddLife();
    }

    public void AddLife(int amount = 1)
    {
        playerLife.value += amount;

        if(playerLife.value >= 20)
        {
            playerLife.value = 20;
        }
    }
}
