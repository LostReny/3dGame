using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{
    public PlayerController playerController;
    public float dist = .2f;
    public float coinSpeed = .5f;

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
    }


    void Update()
    {
        if(Vector3.Distance(transform.position, playerController.transform.position) > dist)
        {
            coinSpeed++;
            transform.position = Vector3.MoveTowards(transform.position, playerController.transform.position, Time.deltaTime * coinSpeed);
        }  
    }
}
