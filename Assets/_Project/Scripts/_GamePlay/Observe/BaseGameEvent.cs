using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameEvent<T> : ScriptableObject
{
    private List<BaseGameListener<T>> gameListeners = new List<BaseGameListener<T>>();
    public void Raise(T e)
    {
        for (int i = 0; i < gameListeners.Count; i++)
        {
            gameListeners[i].OnRaise(e);
        }
    }
    public void RegisterEvent(BaseGameListener<T> getGameEvenlistener )
    {
        if(!gameListeners.Contains(getGameEvenlistener))
        {
            gameListeners.Add(getGameEvenlistener);
        }
    }
    public void UnregisterEvent(BaseGameListener<T> getGameEvenlistener)
    {
        if (gameListeners.Contains(getGameEvenlistener))
        {
            gameListeners.Remove(getGameEvenlistener);
        }
    }
}