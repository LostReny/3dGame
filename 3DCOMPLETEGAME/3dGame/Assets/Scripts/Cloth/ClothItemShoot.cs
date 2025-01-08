using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cloth
{

    public class ClothItemShoot : ClothItemBase
    {
        public GunBase gunBase;
        public GameObject gunObject;

        public override void Collect()
        {
            base.Collect();
            gunBase.ChangeShootSpeed(0.1f, 5f);
        }

        private void Start()
        {
            if (gunBase != null)
            {
                gunBase = gunObject.GetComponent<GunBase>();
            }
        }
    }

}