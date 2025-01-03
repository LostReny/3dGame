using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
   public string tagToCheck = "Player";

   public PlayerController playerController;

   public GameObject cameraTrigger;
   public Color gizmoColor = Color.yellow;

   public float duration = 1f;

   public Collider _collider;


   private void Awake()
   {
        cameraTrigger.SetActive(false);
        _collider.enabled = true;
   }

   private void OnTriggerEnter(Collider other)
   {
        if(other.transform.tag == tagToCheck)
        {
            StartCoroutine(CameraCoroutine());
        }

   }

   private void TurnCameraTriggerOn()
   {
        cameraTrigger.SetActive(true);
        playerController.TurnOffColliders();
   }

   private void TurnCameraTriggerOff()
   {
        cameraTrigger.SetActive(false);
        playerController.TurnOnColliders();
   }

   private void OnDrawGizmos()
   {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, transform.localScale.y);
   }

   IEnumerator CameraCoroutine()
   {
       if(cameraTrigger != null)
       {
          TurnCameraTriggerOn();

          yield return new WaitForSeconds(duration);
          
          _collider.enabled = false;
          TurnCameraTriggerOff();
       }

   }
}
