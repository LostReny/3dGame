using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothItemShoot : ClothItemBase
    {
        public GameObject gunPrefab; // Armazena o prefab da arma
        public GameObject gunObject;

        public float _maxShoot = 10f;
        public GunShootLimit gunLimit;

        public override void Collect()
        {
            base.Collect();

            // Chama o método para mudar o limite de disparos dinamicamente
            if (gunLimit != null)
            {
                gunLimit.ChangeShootLimit(_maxShoot, duration);
                Debug.Log($"MaxShoot updated to {_maxShoot} in Collect.");
            }

            Invoke("DestroyGO", 3f);
        }

        public override void Start()
        {
            if (gunPrefab != null)
            {
                gunObject = Instantiate(gunPrefab, transform.position, transform.rotation);
                gunLimit = gunObject.GetComponent<GunShootLimit>();

                if (gunLimit == null)
                {
                    Debug.LogError("GunShootLimit component not found in the instantiated gun.");
                }
            }
            else
            {
                Debug.LogError("Gun prefab is null. Ensure gunPrefab is assigned in the Inspector.");
            }
        }

        public void DestroyGO()
        {
            Destroy(gameObject, 0.5f);
        }
    }
}
