using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointBase : MonoBehaviour
{

    public MeshRenderer meshRenderer;
    public int Key = 01;


    private bool checkPointActive = false; 
    private string checkPointKey = "CheckPointKey";


    public void Start()
    {
        TurnItOff();
    }

   private void OnTriggerEnter(Collider other)
   {   
        if(!checkPointActive && other.transform.tag == "Player")
        {
             CheckCheckPoint();
        } 
   }

   private void CheckCheckPoint()
   {    
        TurnItOn();
        SaveCheckpoints();
   }

    [NaughtyAttributes.Button]
   private void TurnItOn()
   {
        meshRenderer.material.SetColor("_EmissionColor", Color.white);
   }

    [NaughtyAttributes.Button]
   private void TurnItOff()
   {
        meshRenderer.material.SetColor("_EmissionColor", Color.black);
   }


   private void SaveCheckpoints()
   {
        /*if(PlayerPrefs.GetInt(checkPointKey,0) > Key)
            PlayerPrefs.SetInt(checkPointKey, Key);*/

        CheckPointManager.Instance.SaveCheckPoint(Key);
        
        checkPointActive = true;
   }

}
