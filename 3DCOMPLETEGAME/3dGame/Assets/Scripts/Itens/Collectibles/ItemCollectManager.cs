using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using LostWordls.Singleton;
using JetBrains.Annotations;


namespace Itens
{
    public enum ItemType
    {
        COIN,
        LIFE_PACK
    }

    public class ItemCollectManager : Singleton<ItemCollectManager>
    {
        public List<ItensSetup> itensSetups;

        private void Awake()
        {

            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

        }

        private void Start()
        {
            /*if (SaveManager.Instance.loadItens = true)
            {
                LoadItemsFromSave();
            }
            else
            {
                Reset();
            }*/

            if(SaveManager.Instance.loadItens = false)
            {
                Reset();
            }
                   
        }

        public void LoadItemsFromSave()
        {
            Reset();
            AddByType(ItemType.COIN,(int) SaveManager.Instance.Setup.coins);
            AddByType(ItemType.LIFE_PACK,(int) SaveManager.Instance.Setup.lifePack);
        }

        public void Reset()
        {
            foreach(var i in itensSetups)
            {
                i.soInt.value = 0;
            }
            SaveManager.Instance.Setup.coins = 0;
            SaveManager.Instance.Setup.lifePack = 0;
            
            SaveManager.Instance.Save();
            
        }

        public ItensSetup GetItemByType(ItemType itemType, int amount = 1)
        {
            return itensSetups.Find(i => i.itemType == itemType);
        }
        
        public void AddByType(ItemType itemType, int amount = 1)
        {
            if (amount < 0) return;

            itensSetups.Find(i => i.itemType == itemType).soInt.value += amount;
        }

        public void RemoveByType(ItemType itemType, int amount = 1)
        {
            //if (amount > 0) return;

            var item = itensSetups.Find(i => i.itemType == itemType);
            item.soInt.value -= amount;

            if(item.soInt.value < 0) item.soInt.value = 0;
        }

        [NaughtyAttributes.Button]
        private void AddCoin()
        {
            AddByType(ItemType.COIN);
        }
        
        
        [NaughtyAttributes.Button]
        private void AddLife()
        {
            AddByType(ItemType.LIFE_PACK);
        }

    }

    [System.Serializable]
    public class ItensSetup
        {
            public ItemType itemType;
            public SOInt soInt;
            public Sprite icon;
        }
}
