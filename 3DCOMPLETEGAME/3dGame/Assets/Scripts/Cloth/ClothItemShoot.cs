using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cloth
{

    public class ClothItemShoot : ClothItemBase
    {
        public GameObject gunObject;

        public float newMaxShoot = 10f;
        public float newTBwShoot = 0.01f;

        public GunShootLimit gunLimit;

        public override void Collect()
        {
            base.Collect();
            gunLimit.ChangeShootLimit(newMaxShoot, duration);
            gunLimit.ChangeShootSpeed(newTBwShoot, duration);

            Invoke("DestroyGO", 3f);
        }

        private void Start()
        {
            if (gunObject != null)
            {
                gunLimit = gunObject.GetComponent<GunShootLimit>();
            }
            else
            {
                Debug.LogError("Gun object is null. Ensure gunObject is assigned in the Inspector.");
            }
        }

        public void DestroyGO()
        {
            Destroy(gameObject, 0.5f);
        }
    }

}