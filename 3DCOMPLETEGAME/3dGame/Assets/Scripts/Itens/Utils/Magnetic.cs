using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{
    public PlayerController playerController;
    public float dist = 0.2f;      
    public float initialSpeed = 5f; 
    public float maxSpeed = 9f;    
    public float acceleration = 3.5f;

    private float currentSpeed;

    public void Start()
    {
        if (playerController == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                playerController = playerObject.GetComponent<PlayerController>();
            }
        }

        currentSpeed = initialSpeed;

    }


   void Update()
    {
        MoveToPlayer();
    }

    void MoveToPlayer()
    {
        if (playerController != null)
        {
            float distance = Vector3.Distance(transform.position, playerController.transform.position);

            if (distance > dist)
            {
                currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxSpeed);
                transform.position = Vector3.Lerp(transform.position,playerController.transform.position,Time.deltaTime * currentSpeed / distance );
            }
        }
    }
}
