using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using NaughtyAttributes;

namespace LostWordls.StateMachine
{

    public class StateMachine<T> where T : System.Enum
    {

        public Dictionary<T, StateBase> dictionaryState;
        private StateBase _currentState;
        public float timeToStartGame = 1f;

        public StateBase CurrentState
        {
            get { return _currentState; }
        }

        public void Init()
        {
            dictionaryState = new Dictionary<T, StateBase>();
        }

        public void RegisterStates(T typeEnum, StateBase state)
        {
            if (!dictionaryState.ContainsKey(typeEnum))
            {
                dictionaryState.Add(typeEnum, state);
                Debug.Log($"State {typeEnum} registered successfully.");
            }
        }


        public void SwitchStates(T state)
        {
            if (dictionaryState.ContainsKey(state))
            {
                if (_currentState != null)
                {
                    _currentState.OnStateExit();
                }

                _currentState = dictionaryState[state];
                if (_currentState != null)
                {
                    _currentState.OnStateEnter();
                }
                Debug.Log($"Switched to state: {state}");
            }
            else
            {
                Debug.LogError($"State {state} not found in dictionary.");
            }
        }


        public void Update()
        {
            if (_currentState != null)
            {
                _currentState.OnStateStay();
            }
        }

    }

}
