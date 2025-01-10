using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animation;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamagable
    {
        public Collider _collider;
        public float startLife = 10f;
        public bool lookAtPlayer = false;
        public bool useTrigger = true; // Define se o inimigo depende de trigger para ativar

        public FlashColor _flashColor;
        public ParticleSystem _particleSystem;
        public int particleValue = 15;

        [SerializeField] private float _currentLife;

        [Header("Animation")]
        [SerializeField] private AnimationBase _animationBase;

        [Header("Start Animation")]
        public float startAnimationDuration = .2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

        private bool isActive = false; // Estado de ativa��o do inimigo
        private PlayerController _playerController;


        private void Awake()
        {
            if (!useTrigger) Init(); // Ativa automaticamente se useTrigger for falso
        }

        private void Start()
        {
            _playerController = GameObject.FindObjectOfType<PlayerController>();
        }

        protected virtual void ResetLife()
        {
            _currentLife = startLife;
        }

        protected virtual void Init()
        {
            ResetLife();
            isActive = true; // Define o inimigo como ativo

            if (startWithBornAnimation)
            {
                BornAnimation();
            }
        }

        protected virtual void Kill()
        {
            OnKill();
        }

        protected virtual void OnKill()
        {
            if (_collider != null) _collider.enabled = false;
            Destroy(gameObject, 3f);
            PlayAnimationByTrigger(AnimationType.DEATH);
        }

        public void OnDamage(float f)
        {
            if (_flashColor != null) _flashColor.Flash();
            if (_particleSystem != null) _particleSystem.Emit(particleValue);

            transform.position -= transform.forward;

            _currentLife -= f;

            if (_currentLife <= 0)
            {
                Kill();
            }
        }


        private void OnCollisionEnter(Collision collision)
        {
            PlayerController p = collision.transform.GetComponent<PlayerController>();
            if(p != null)
            {
                p.healthBase.Damage(1);
            }
        }

        public virtual void Update()
        {
            if (!isActive) return; // N�o faz nada se o inimigo n�o estiver ativo

            if (lookAtPlayer)
            {
                Vector3 targetPosition = new Vector3(_playerController.transform.position.x, transform.position.y, _playerController.transform.position.z);
                transform.LookAt(targetPosition);
            }

        }

        #region Animations

        private void BornAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger(animationType);
        }

        #endregion

        public void Damage(float damage)
        {
            OnDamage(damage);
        }

        public void Damage(float damage, Vector3 dir)
        {
            OnDamage(damage);
            transform.DOMove(transform.position - dir, .1f);
        }


        #region TRIGGER
        private void OnTriggerEnter(Collider other)
        {
            if (!useTrigger || isActive) return; // Ignora se o uso de trigger est� desativado ou j� est� ativo

            if (other.gameObject.CompareTag("Player"))
            {
                Init(); // Ativa o inimigo ao detectar o player
            }
        }

        #endregion
    }
}
