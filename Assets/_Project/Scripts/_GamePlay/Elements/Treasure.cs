using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : BaseObject
{
    public PlaySoundEvent PlaySoundEvent;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        PlaySoundEvent.Raise();
    }
}