using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float runSpeed;
    public float jumpForce = 7f;
    private float initialSpeed; 

    private Rigidbody rb;
    private bool isGrounded = true;

    private Vector3 direction;
    private bool isRunning;
    private bool isJumping;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        initialSpeed = speed;
        
    }

    void Update()
    {
        Inputs(); 
    }

    public void Inputs()
    {
       
        direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }

       
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Run();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopRunning();
        }
    }

    void FixedUpdate()
    {
        Walk();

        if (isJumping)
        {
            Jump();
            isJumping = false; 
        }
    }

    public void Walk()
    {
        Vector3 newPosition = rb.position + direction * speed * Time.fixedDeltaTime; 
        rb.MovePosition(newPosition);
    }

    public void Jump()
    {
        rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse); 
        isGrounded = false;
    }

    public void Run()
    {
        speed = runSpeed;
        isRunning = true;
    }

    public void StopRunning()
    {
        speed = initialSpeed; 
        isRunning = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; 
        }
    }
}
