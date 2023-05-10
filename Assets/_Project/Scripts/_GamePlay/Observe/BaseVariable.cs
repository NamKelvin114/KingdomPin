using System.Collections;
using System.Collections.Generic;
using Namhale.Attributes;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class BaseVariable<T> : ScriptableObject
{
    [SerializeField, NamedId] private string id;
    [SerializeField] public T InintializeValue;
    public bool IsSave;
    public string ID => id;
    private List<BaseVariableListener<T>> gameVariableListeners = new List<BaseVariableListener<T>>();
    public T Value
    {
        get
        {
            if (IsSave)
            {
                return DataVariables<T>.Load(ID, InintializeValue);
            }
            else
            {
                return InintializeValue;
            }
        }
        set
        {
            if (IsSave)
            {
                DataVariables<T>.Save(ID, value == null ? InintializeValue : value);
            }
            else
            {
                InintializeValue = value;
            }
        }
    }
    public void Raise()
    {
        for (int i = 0; i < gameVariableListeners.Count; i++)
        {
            gameVariableListeners[i].OnRaise();
        }
    }

    public void RegisterEvent(BaseVariableListener<T> getGameVariablelistener)
    {
        if (!gameVariableListeners.Contains(getGameVariablelistener))
        {
            gameVariableListeners.Add(getGameVariablelistener);
        }
    }

    public void UnregisterEvent(BaseVariableListener<T> getGameVariablelistener)
    {
        if (gameVariableListeners.Contains(getGameVariablelistener))
        {
            gameVariableListeners.Remove(getGameVariablelistener);
        }
    }
}