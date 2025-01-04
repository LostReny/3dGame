using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Itens { 
    public class ItemCollectBase : MonoBehaviour
    {
        public ItemType itemType;

        public string compareTag = "Player";

        public float timeToHide = 1;
        public GameObject graphicItem;

        public Collider _collider;

        public void OnTriggerEnter(Collider collision) {

            if(collision.transform.CompareTag(compareTag)){
                Collect();
            }
        
        }

        public void Start()
        {
            _collider = GetComponent<Collider>();
            _collider.enabled = true;
        }

        protected virtual void Collect()
        {
            //Destroy(gameObject, 0.5f);
            if (_collider != null) _collider.enabled = false;

            if (graphicItem != null) graphicItem.SetActive(true);
            Invoke("HideObject", timeToHide);
            OnCollect();
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        
        }

        protected virtual void OnCollect()
        {
            ItemCollectManager.Instance.AddByType(itemType);
        }

    }
}
