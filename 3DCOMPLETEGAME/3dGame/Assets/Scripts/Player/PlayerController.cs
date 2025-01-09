using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cloth;

public class PlayerController : MonoBehaviour//IDamagable
{
    
    public List<Collider> colliders;
    
    [Header("Animator")]
    public Animator animator;

    [Header("Movement")]
    public CharacterController characterController;
    public float speed = 1.0f;
    public float turnSpeed = 1.0f;
    public float gravity = -9f;

    [Header("Jump")]
    public float jumpSpeed = 15f;
    public KeyCode jumpKeyCode = KeyCode.Space;

    [Header("Run")]
    public float runSpeed = 1.5f;
    public KeyCode runKeyCode = KeyCode.LeftShift;

    private Coroutine speedChangeCoroutine;

    private float vSpeed = 0f;

    [Header("Flash")]
    public List<FlashColor> flashColors;

    [Header("Health")]
    public HealthBase healthBase;
    private bool _alive = true;

    [Space]
    [SerializeField] private ClothChanger _clothChanger;

    private void OnValidate()
    {
        if(healthBase == null)
        {
            healthBase = GetComponent<HealthBase>();
        }
    }

    private void Awake()
    {
        OnValidate();
        healthBase.OnDamage += Damage;
        healthBase.OnKill += OnKill;
    }


    private void Update()
    {
        OnMove();
    }

    #region movement
    public void OnMove()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;

        OnJump();

        vSpeed -= gravity * Time.deltaTime;
        speedVector.y = vSpeed;

        characterController.Move(speedVector * Time.deltaTime);

        OnRun();
    }


    public void OnRun()
    {   
        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;
         // run e anima��o
        var isWalking = inputAxisVertical != 0;
        if (isWalking)
        {
            if (Input.GetKey(runKeyCode))
            {
                speedVector *= runSpeed;
                animator.speed = runSpeed;
            }
            else
            {
                animator.speed = 1;
            }
        }


        // anima��o
        if (inputAxisVertical != 0)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

    }
    #endregion

    #region VELOCIDADE AO PEGAR ITEM
        public void ChangeRunSpeedTemporarily(float speed, float duration)
        {
            StartCoroutine(ChangeRunSpeedCoroutine(speed,duration));
        }

        IEnumerator ChangeRunSpeedCoroutine(float newSpeed, float duration)
        {
            float originalSpeed = speed;
            speed = newSpeed;          
            yield return new WaitForSeconds(duration);
            speed = originalSpeed;       
        }

    #endregion

    #region TEXTURE
        public void ChangeTexture(ClothSetup setup, float duration)
        {
            StartCoroutine(ChangeTextureCoroutine(setup, duration));
        }

        private IEnumerator ChangeTextureCoroutine(ClothSetup setup, float duration)
        {
            _clothChanger.ChangeTexture(setup);
            yield return new WaitForSeconds(duration);
            _clothChanger.ResetTexture();
        }

    #endregion


    #region jump
    public void OnJump()
    {
        if (characterController.isGrounded)
        {
            vSpeed = 0;
            if (Input.GetKeyDown(jumpKeyCode))
            {
                vSpeed = jumpSpeed;
            }
        }
    }
    #endregion

    #region Life
    public void Damage(HealthBase h)
    {
        flashColors.ForEach(i => i.Flash());
        EffectsManager.Instance.ChangeVignette();
    }

    public void Damage(float damage, Vector3 dir)
    {
        
    }
    private void OnKill(HealthBase h)
    {
        
        if(_alive)
        {
            _alive = false;
            animator.SetTrigger("Death");
            //colliders.ForEach(i => i.enabled = false);
            Invoke(nameof(TurnOffColliders), .05f);

            Invoke(nameof(Revive), 3f);
        }
        
    }

    private void Revive()
    {
        _alive = true;
        healthBase.ResetLife();
        animator.SetTrigger("Revive");
        Respawn();
        healthBase.UpdateUi();
        Invoke(nameof(TurnOnColliders), .1f);
    }

    public void TurnOnColliders()
    {
        colliders.ForEach(i => i.enabled = true);
    }

    public void TurnOffColliders()
    {
        colliders.ForEach(i => i.enabled = false);
    }

    #endregion

    #region CHECKPOINTS


    [NaughtyAttributes.Button]
    public void Respawn()
    {
        if(CheckPointManager.Instance.HasCheckPoint())
        {
            transform.position = CheckPointManager.Instance.GetPositionFromLastCheckPoint();
        }
    }


    #endregion

}