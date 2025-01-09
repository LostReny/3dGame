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
            if (gunObject != null && gunObject.GetComponent<GunShootLimit>() != null)
            {
                //gunLimit = gunObject.GetComponent<GunShootLimit>();
                // Altera o limite de disparos.
                gunLimit.ChangeShootLimit(_maxShoot, duration);
            }

            Invoke("DestroyGO", 3f);
        }
        public override void Start()
        {
                gunObject = Instantiate(gunPrefab, transform.position, transform.rotation);

                // Ativa o gunObject se não estiver ativo
                gunObject.SetActive(true); // Garantir que o gunObject esteja ativo

                gunLimit = gunObject.GetComponent<GunShootLimit>();
                
        }
           


        public void DestroyGO()
        {
            Destroy(gameObject, 0.5f);
        }
    }
}
