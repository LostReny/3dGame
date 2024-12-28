using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LostWordls.StateMachine;
using DG.Tweening;
using Enemy;


namespace Boss
{
    public enum BossAction
    {
        INIT,
        IDLE, 
        WALK,
        ATTACK
    }

    public class BossBase : MonoBehaviour
    {

        [Header("Animation")]
        public float startAnimationDuration = .5f;
        public Ease startAnimationEase = Ease.OutBack;

        [Header("Attack")]
        public int attackAmount = 5;
        public float timeBtwAttacks = .5f;


        public float speed = 5f;
        public List<Transform> waypoints;

        private StateMachine<BossAction> stateMachine;


        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            stateMachine = new StateMachine<BossAction>();
            stateMachine.Init();

            stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
            stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());
        }


        #region COROUTINE ATTACK

        public void StartAttack(Action endCallback = null)
        {
            StartCoroutine(AttackCoroutine(endCallback));
        }

        IEnumerator AttackCoroutine(Action endCallBack)
        {
            int attacks = 0;
            while(attacks < attackAmount)
            {
                attacks++;
                transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo);
                yield return new WaitForSeconds(timeBtwAttacks);
            }

            if(endCallBack != null) endCallBack.Invoke();
        }

        #endregion


        #region COROUTINE WALK

        public void GoToRandomPoint(Action onArrive = null)
        {
            StartCoroutine(GoToPointCoroutine(waypoints[UnityEngine.Random.Range(0, waypoints.Count)], onArrive));
        }

        IEnumerator GoToPointCoroutine(Transform t, Action onArrive = null)
        {
            while(Vector3.Distance(transform.position, t.position) > 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }

            if(onArrive !=  null) onArrive.Invoke();
        }

        #endregion


        #region ANIMATION 
        public void StartInitAnimaition()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        #endregion


        #region DEBUG

        [NaughtyAttributes.Button]
        private void SwitchInit()
        {
            SwitchState(BossAction.INIT);
        }

        [NaughtyAttributes.Button]
        private void SwitchWalk()
        {
            SwitchState(BossAction.WALK);
        }
        
        [NaughtyAttributes.Button]
        private void SwitchAttack()
        {
            SwitchState(BossAction.ATTACK);
        }

        #endregion   


        #region STATE MACHINE

        public void SwitchState(BossAction state)
        {
            stateMachine.SwitchStates(state, this);
        }

        #endregion


    }

}
