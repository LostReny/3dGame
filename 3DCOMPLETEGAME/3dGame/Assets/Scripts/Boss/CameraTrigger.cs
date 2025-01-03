using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
   public string tagToCheck = "Player";

   public GameObject cameraTrigger;
   public Color gizmoColor = Color.yellow;



   private void Awake()
   {
        cameraTrigger.SetActive(false);
   }

   private void OnTriggerEnter(Collider other)
   {
        if(other.transform.tag == tagToCheck)
        {
            TurnCameraTriggerOn();
        }
   }

   private void TurnCameraTriggerOn()
   {
        cameraTrigger.SetActive(true);
   }

   private void OnDrawGizmos()
   {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, transform.localScale.y);
   }
}
