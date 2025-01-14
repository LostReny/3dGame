using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckPointBase : MonoBehaviour
{

    public MeshRenderer meshRenderer;
    public int Key = 01;

    public TMP_Text checkPointText;


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
        checkPointText.text = "Checkpoint Ativo";

        StartCoroutine(HideCheckpointMessage(3f));
   }

    [NaughtyAttributes.Button]
   public void TurnItOff()
   {
        meshRenderer.material.SetColor("_EmissionColor", Color.black);
   }


   public void SaveCheckpoints()
   {
        /*if(PlayerPrefs.GetInt(checkPointKey,0) > Key)
            PlayerPrefs.SetInt(checkPointKey, Key);*/
        
        int savedCheckpoint = CheckPointManager.Instance.SaveCheckPoint(Key);
     
        SaveManager.Instance.SaveCheckpoint(savedCheckpoint);
        
        checkPointActive = true;
   }

   private IEnumerator HideCheckpointMessage(float delay)
    {
        yield return new WaitForSeconds(delay);
        checkPointText.text = ""; 
    }

}
