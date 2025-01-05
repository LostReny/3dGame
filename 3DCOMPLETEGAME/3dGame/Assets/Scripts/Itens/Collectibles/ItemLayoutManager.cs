using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Itens { 
    public class ItemLayoutManager : MonoBehaviour
    {
        public ItemLayout prefabLayout;
        public Transform container;

        public List<ItemLayout> itemLayout;


        private void Start()
        {
            CreateItems();
        }

        private void CreateItems()
        {
            foreach(var setup in ItemCollectManager.Instance.itensSetups)
            {
                var item = Instantiate(prefabLayout, container);
                item.Load(setup);
                itemLayout.Add(item);
                if(item != null) item.gameObject.SetActive(true);
            }
        }
    }
}
