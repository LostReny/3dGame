using Itens;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagneticTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ItemCollectBase item = other.transform.GetComponent<ItemCollectBase>();
        if (item != null && other.gameObject.GetComponent<Magnetic>() == null)
        {
            other.gameObject.AddComponent<Magnetic>();
        }
    }
}
