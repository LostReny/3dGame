using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LostWordls.StateMachine;

public class GMStateJump : StateBase
{
    private PlayerController player;

     public override void OnStateEnter(object o = null)
        {
             player = o as PlayerController;

            if (player == null)
            {
                Debug.LogError("PlayerController not found or not passed to Jump State.");
                return;
            }

            player.Jump(); // Executa o pulo ao entrar no estado de pulo
            Debug.Log("Entered Jump State");
        }

        public override void OnStateStay()
        {
            // Lógica para pular
            Debug.Log("Jumping...");
        }

        public override void OnStateExit()
        {
            Debug.Log("Exited Jump State");
        }
}
