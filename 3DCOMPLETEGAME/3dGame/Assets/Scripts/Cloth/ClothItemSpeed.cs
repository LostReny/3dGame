using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothItemSpeed : ClothItemBase
    {
        private PlayerController playerController;

        public override void Collect()
        {
            base.Collect();

            if (playerController == null)
            {
                FindPlayerController();
            }

            if (playerController != null)
            {
                Debug.Log("Item coletado: Aumentando velocidade do jogador.");
                playerController.ChangeRunSpeedTemporarily(3.0f, 5.0f);
            }
            else
            {
                Debug.LogError("PlayerController não encontrado!");
            }
        }

        private void FindPlayerController()
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                playerController = playerObject.GetComponent<PlayerController>();
            }
        }
    }
}
