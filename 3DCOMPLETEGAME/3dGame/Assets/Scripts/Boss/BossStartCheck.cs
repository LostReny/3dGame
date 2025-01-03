using System.Collections;
using System.Collections.Generic;
using Boss;
using UnityEngine;

public class BossStartCheck : MonoBehaviour
{
    public BossBase bossBase;

    [Header("Trigger")]
        public bool useTrigger = true;
        private bool isActive = false;
    
    [Header("Camera")]
        public GameObject bossCamera;


    private void Awake()
    {
        if (!useTrigger) bossBase.Init();
        bossCamera.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!useTrigger || isActive) return; // Ignora se o uso de trigger est� desativado ou j� est� ativo

        if (other.gameObject.CompareTag("Player"))
        {
            bossBase.Init(); // Inicializa o Boss quando o Player entra no trigger
            TurnCameraOn();
            bossBase.SwitchState(BossAction.WALK); // Troca para o estado WALK
        }
    }
    

    private void TurnCameraOn()
    {
        bossCamera.SetActive(true);
    }

}
