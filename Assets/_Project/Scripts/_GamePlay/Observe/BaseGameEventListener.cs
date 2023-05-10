using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class BaseGameListener<T> : MonoBehaviour
{
    [SerializeField] private BaseGameEvent<T> GameEvent;
    [SerializeField] private UnityEvent<T> SetEvent;

    private void OnEnable()
    {
        GameEvent.RegisterEvent(this);
    }

    private void OnDisable()
    {
        GameEvent.UnregisterEvent(this);
    }

    public void OnRaise(T e)
    {
        SetEvent?.Invoke(e);
    }
}