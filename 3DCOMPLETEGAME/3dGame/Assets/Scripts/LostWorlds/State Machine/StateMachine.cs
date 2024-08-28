using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using NaughtyAttributes;

public class StateMachine<T> where T : System.Enum
{
    
    public Dictionary<T, StateBase> dictionaryState;
    private StateBase _currentState;
    public float timeToStartGame = 1f;


   public void Init()
   {
      dictionaryState = new Dictionary<T, StateBase>();
   }


    public void RegisterStates(T typeEnum, StateBase state) 
    {

      dictionaryState.Add(typeEnum, state);
       
    }
    
    public void SwitchStates(T state)
   {
      if (_currentState != null)
      {
         _currentState.OnStateExit();
      }

      _currentState = dictionaryState[state];
      if(_currentState != null){ _currentState.OnStateEnter();}
   }

   public void Update()
   {
      if (_currentState != null)
      {
         _currentState.OnStateStay();
      }
   }


}
