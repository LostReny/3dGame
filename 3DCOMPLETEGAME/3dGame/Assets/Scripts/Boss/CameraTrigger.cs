using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
   public string tagToCheck = "Player";

   public PlayerController playerController;

   public GameObject cameraTrigger;
    public GameObject cameraIdlePlayer; 
   public Color gizmoColor = Color.yellow;

   public float duration = 1f;

   public Collider _collider;


   private void Awake()
   {
        cameraTrigger.SetActive(false);
        _collider.enabled = true;
        cameraIdlePlayer.SetActive(true);
   }

   private void OnTriggerEnter(Collider other)
   {
        if(other.transform.tag == tagToCheck)
        {
            StartCoroutine(CameraCoroutine());
        }

   }

   private void TurnCameraTriggerOn(bool triggerState, bool idleState)
   {
        cameraTrigger.SetActive(triggerState);
        cameraIdlePlayer.SetActive(idleState);
    }

   private void OnDrawGizmos()
   {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, transform.localScale.y);
   }

   IEnumerator CameraCoroutine()
    {
        if (cameraTrigger != null && cameraIdlePlayer != null)
        {
            TurnCameraTriggerOn(true, false);
            _collider.enabled = false;
            playerController.TurnOffColliders();

            yield return new WaitForSeconds(duration);
            TurnCameraTriggerOn(false, true);
            playerController.TurnOnColliders();

        }
    }
}
