using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LostWordls.Singleton {

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
     public static T Instance;


    // minha instancia é igual ao script
    protected virtual void Awake() {
        if(Instance == null)
            Instance = this as T;
        else
             Destroy(gameObject);
    }
}

}
