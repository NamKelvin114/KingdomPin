using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class BaseVariableListener<T> : MonoBehaviour
{
    [SerializeField] private BaseVariable<T> GameVariable;
    [SerializeField] private UnityEvent<T> SetEvent;

    private void OnEnable()
    {
        GameVariable.RegisterEvent(this);
    }

    private void OnDisable()
    {
        GameVariable.UnregisterEvent(this);
    }

    public void OnRaise()
    {
        SetEvent?.Invoke(GameVariable.Value);
    }
}