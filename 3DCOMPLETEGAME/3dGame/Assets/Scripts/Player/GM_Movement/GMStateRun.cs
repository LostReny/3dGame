using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LostWordls.StateMachine;

public class GMStateRun : StateBase
{
    private PlayerController player;

     public override void OnStateEnter(object o = null)
        {
           player = o as PlayerController;

            if (player == null)
            {
                Debug.LogError("PlayerController not found or not passed to Idle State.");
                return;
            }
        }

        public override void OnStateStay()
        {
            
        }

        public override void OnStateExit()
        {
            
        }
}
