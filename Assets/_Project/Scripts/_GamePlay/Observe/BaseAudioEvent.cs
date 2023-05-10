using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class BaseAudioEvent<T> : ScriptableObject
{
    public AudioClip GetAudioClip;
    private List<BaseAudioEventListener<T>> audioEventListeners = new List<BaseAudioEventListener<T>>();

    public void Raise()
    {
        for (int i = 0; i < audioEventListeners.Count; i++)
        {
            audioEventListeners[i].OnRaise(this);
        }
    }

    public void RegisterEvent(BaseAudioEventListener<T> getBaseAudioEventListener)
    {
        if (!audioEventListeners.Contains(getBaseAudioEventListener))
        {
            audioEventListeners.Add(getBaseAudioEventListener);
        }
    }

    public void UnregisterEvent(BaseAudioEventListener<T> getBaseAudioEventListener)
    {
        if (audioEventListeners.Contains(getBaseAudioEventListener))
        {
            audioEventListeners.Remove(getBaseAudioEventListener);
        }
    }
}