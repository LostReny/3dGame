using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectBase : MonoBehaviour
{
    public string compareTag = "Player";

    public float timeToHide = 1;
    public GameObject graphicItem;

    /*[Header("Sounds")]
    public AudioSource audioSource;*/


    public void OnTriggerEnter(Collider collision) {

        if(collision.transform.CompareTag(compareTag)){
            Collect();
        }
        
    }

    protected virtual void Collect()
    {
        Destroy(gameObject, 0.5f);

        if (graphicItem != null) graphicItem.SetActive(true);
        Invoke("HideObject", timeToHide);
        OnCollect();
    }

    private void HideObject()
    {
        gameObject.SetActive(false);
        
    }

    protected virtual void OnCollect()
    {

            /*if (!audioSource.isPlaying) {
                audioSource.transform.SetParent(null);
                audioSource.Play();
                //Debug.Log("Esta tocando");
            }

            VFXManager.Instance.PlayVFXByType(VFXManager.VFXType.COIN, transform.position);*/
    }

}
