using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothItemSpeed : ClothItemBase
    {

        public float targetSpeed = 30f;

        public override void Collect()
        {
            base.Collect();

            if (playerController != null)
            {
                playerController.ChangeRunSpeedTemporarily(targetSpeed, duration);
            }

            Invoke("DestroyGO", 3f);
        }

        public void DestroyGO()
        {
            Destroy(gameObject, 0.5f);
        }
    }
 }
