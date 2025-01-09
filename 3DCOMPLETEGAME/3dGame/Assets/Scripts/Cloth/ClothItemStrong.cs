using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothItemStrong : ClothItemBase
    {
        public float damageMultipliyer = 0f;

        public override void Collect()
        {
            base.Collect();
            playerController.healthBase.ChangeDamageMultiplay(damageMultipliyer, duration);

            Invoke("DestroyGO", 3f);
        }


        public void DestroyGO()
        {
            Destroy(gameObject, 0.5f);
        }
    }
}
