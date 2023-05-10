using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObject : MonoBehaviour
{
    public virtual void DoUpdate()
    {
        
    }

    public virtual void Initialzie()
    {
        
    }

    public virtual void DoEnable()
    {
        
    }

    public virtual void DoDisable()
    {
        
    }

    private void Start()
    {
        Initialzie();
    }

    private void OnEnable()
    {
        DoEnable();
    }

    private void OnDisable()
    {
       DoEnable();
    }

    private void Update()
    {
        DoUpdate();
    }
}
