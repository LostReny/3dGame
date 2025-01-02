using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private float vSpeed = 0f;

    [Header("Flash")]
    public List<FlashColor> flashColors;

    [Header("Health")]
    public HealthBase healthBase;
    private bool _alive = true;



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
        healthBase.OnDamage += OnKill;
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
            colliders.ForEach(i => i.enabled = false);
        }
        
    }
    #endregion

}