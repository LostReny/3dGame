using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LostWordls.StateMachine;

public class GMStateWalk : StateBase
{
    private PlayerController player; 

     public override void OnStateEnter(object o = null)
        {
             player = o as PlayerController;

            if (player == null)
            {
                Debug.LogError("PlayerController not found or not passed to Walk State.");
                return;
            }

            Debug.Log("Entered Walk State");
        }

        public override void OnStateStay()
        {
            player?.Walk();
        }

        public override void OnStateExit()
        {
            Debug.Log("Exited Walk State");
        }
}
