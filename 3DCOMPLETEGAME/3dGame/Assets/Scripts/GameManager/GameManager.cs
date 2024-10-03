using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LostWordls.Singleton;
using LostWordls.StateMachine;

public class GameManager : Singleton<GameManager>
{
   public enum GameStates
   {
        WALK,
        RUN,
        JUMP
   }

   public StateMachine<GameStates> stateMachine;
   


   private void Start()
   {
        Init();
   }

   public void Init()
   {
        stateMachine = new StateMachine<GameStates>();

        stateMachine.Init();
        stateMachine.RegisterStates(GameStates.WALK, new GMStateWalk());
        stateMachine.RegisterStates(GameStates.RUN, new GMStateRun());
        stateMachine.RegisterStates(GameStates.JUMP, new GMStateJump());
        
        stateMachine.SwitchStates(GameStates.WALK);

   }

     private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            stateMachine.SwitchStates(GameStates.WALK);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.SwitchStates(GameStates.JUMP);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            stateMachine.SwitchStates(GameStates.RUN);
        }

        stateMachine.Update();
    }
}
