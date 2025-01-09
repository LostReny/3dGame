using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothItemBase : MonoBehaviour
    {
        public ClothType clothType;
        public float duration = 2f;
        public string compareTag = "Player";
        public PlayerController playerController;
        public float timeToHide = 1;
        public GameObject graphicItem;

        public Collider _collider;

        public void OnTriggerEnter(Collider collision) 
        {
            if(collision.transform.CompareTag(compareTag))
            { 
                Debug.Log("Collect");
                var setup = ClothManager.Instance.GetSetupByType(clothType);
                playerController.ChangeTexture(setup, duration);
                
                Collect();
            }
        }

         public virtual void Start()
        {
            if (playerController == null)
            {
                GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
                if (playerObject != null)
                {
                    playerController = playerObject.GetComponent<PlayerController>();
                }
            }
            _collider = GetComponent<Collider>();
            _collider.enabled = true;
        }

        public virtual void Collect()
        {
            if (_collider != null) _collider.enabled = false;

            if (graphicItem != null) graphicItem.SetActive(true);
            Invoke("HideObject", timeToHide);
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }
    }
}
