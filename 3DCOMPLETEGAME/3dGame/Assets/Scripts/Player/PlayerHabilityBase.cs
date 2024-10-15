using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHabilityBase : MonoBehaviour
{
    protected PlayerController player;

    private void OnValidate()
    {
        if(player != null) player = GetComponent<PlayerController>();
    }

    private void Start()
    {
        Init();
        OnValidate();
        RegisterListeners();
    }

    private void OnDestroy()
    {

        UnregisterListeners();
    }

    protected virtual void Init()
    {

    }

    protected virtual void RegisterListeners()
    {

    }

    protected void UnregisterListeners() 
    { 

    }

}
