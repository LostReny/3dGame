using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Itens;
using TMPro;
using DG.Tweening;

public class ActionLifePack : MonoBehaviour
{
    public SOInt soInt;
    public PlayerController playerController;
    public KeyCode _KeyCode = KeyCode.E;

    public TMP_Text lifeLowtText;
    public bool usedItemOnce = false;


    public void Start()
    {   
        soInt = ItemCollectManager.Instance.GetItemByType(ItemType.LIFE_PACK).soInt;
        usedItemOnce = false;
    }

    private void RecoverLife()
    {
        if(soInt.value > 0)
        {
            ItemCollectManager.Instance.RemoveByType(ItemType.LIFE_PACK);
            playerController.healthBase.ResetLife();
            playerController.healthBase.UpdateUi();
        }
    }

    private void LifeLow()
    {
        var life = playerController.healthBase.currentLife;
        if(life <= 4 && !usedItemOnce)
        {   
            lifeLowtText.text = "Aperte E para recuperar vida";
            usedItemOnce = true;
            StartCoroutine(HideMessage(4f));
        }
    }

    private IEnumerator HideMessage(float delay)
    {
        yield return new WaitForSeconds(delay);
        lifeLowtText.text = "";
    }


    private void Update()
    {
        LifeLow();

        if(Input.GetKeyDown(_KeyCode))
        {
            RecoverLife();
        }
    }
}
