using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamagable
{
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

        // run e animação
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


        // animação
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
    public void Damage(float damage)
    {
        flashColors.ForEach(i => i.Flash());
    }

    public void Damage(float damage, Vector3 dir)
    {

    }
    #endregion
}