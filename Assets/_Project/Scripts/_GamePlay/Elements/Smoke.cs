using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    private void OnEnable()
    {
        Destroy(gameObject, 0.5f);
    }
}