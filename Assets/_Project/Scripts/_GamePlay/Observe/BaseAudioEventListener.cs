using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class BaseAudioEventListener<T> : MonoBehaviour
{
    public List<BaseAudioEvent<T>> GameEvent = new List<BaseAudioEvent<T>>();
    public UnityEvent<AudioClip> Event;

    private void OnEnable()
    {
        foreach (var gameevent in GameEvent)
        {
            gameevent.RegisterEvent(this);
        }
    }

    private void OnDisable()
    {
        foreach (var gameevent in GameEvent)
        {
            gameevent.UnregisterEvent(this);
        }
    }

    public void OnRaise(BaseAudioEvent<T> baseAudioEvent)
    {
        foreach (var gameevent in GameEvent)
        {
            if (gameevent == baseAudioEvent)
            {
                Event?.Invoke(gameevent.GetAudioClip);
            }
        }
    }
}