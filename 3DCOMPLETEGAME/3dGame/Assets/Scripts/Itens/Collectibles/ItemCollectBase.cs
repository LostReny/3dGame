using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectBase : MonoBehaviour
{
    //override - usado para escrever em cima de uma classe pai(virtual), dando função diferente para classe filha(override)

    public string compareTag = "Player";

    public float timeToHide = 1;
    public GameObject graphicItem;

    //referencia para som 
    [Header("Sounds")]
    public AudioSource audioSource;


    public void OnTriggerEnter2D(Collider2D collision) {

        if(collision.transform.CompareTag(compareTag)){
            Collect();
        }
        
    }


    //o que vai acontecer quando coletar ??
    // quando coletar ele destroi e chama a ação ao coletar 
    protected virtual void Collect(){
        OnCollect();

        Destroy(gameObject, 0.5f);

        if (graphicItem != null) graphicItem.SetActive(true);
        Invoke("HideObject", timeToHide);
    }

    private void HideObject()
    {
        gameObject.SetActive(false);
        
    }



    //quando coletar que ação será realizada ?
    protected virtual void OnCollect()
{

        if (!audioSource.isPlaying) {
            audioSource.transform.SetParent(null);
            audioSource.Play();
            //Debug.Log("Esta tocando");
        }

        //colocar esse vfs somente para moedas 
        //criar outro para a vida
        //VFXManager.Instance.PlayVFXByType(VFXManager.VFXType.COIN, transform.position);
}

}
