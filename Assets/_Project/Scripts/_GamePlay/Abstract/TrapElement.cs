using System;
using System.Collections;
using System.Collections.Generic;

using Animancer;

using PlayFab.Internal;

using UnityEditor;

using UnityEngine;

public abstract class TrapElement : BaseObject
{
    [SerializeField] public BoxCollider TrapBoxCollider;
    [SerializeField] public AnimationClip TrapAnim;
    [SerializeField] public AnimancerComponent AnimancerComponent;
    private float setTimeAlive;
    private float setCoolDown;
    private bool isEndCoolDown;
    public bool IsStopSkill;
    public bool IsCoolDown;
     public float GetCoolDown;

    public bool IsTimeAlive;
    public float GetTimeAlive;
    public bool IsOneShot;

    public override void Initialzie()
    {
        IsStopSkill = false;
        setCoolDown = GetCoolDown;
        setTimeAlive = GetTimeAlive;
        TrapBoxCollider.gameObject.SetActive(false);
        base.Initialzie();
    }

    public override void DoUpdate()
    {
        if (IsStopSkill != true)
        {
            if (IsOneShot != true)
            {
                SetTimeAlive();
            }
            else
            {
                OneShotAction();
            }
        }
        else
        {
            StopAction();
        }
    }

    void SetCoolDown()
    {
        GetCoolDown -= Time.deltaTime;
        if (GetCoolDown <= 0)
        {
            GetTimeAlive = setTimeAlive;
        }
    }

    void SetTimeAlive()
    {
        if (GetTimeAlive >= 0)
        {
            GetTimeAlive -= Time.deltaTime;
            GetCoolDown = setCoolDown;
            ContinutyAction();
        }
        else
        {
            ContinutyDeAction();
            SetCoolDown();
        }
    }

    public virtual void ContinutyAction()
    {
    }

    public virtual void ContinutyDeAction()
    {
    }

    public virtual void OneShotAction()
    {
        Debug.Log("oneSHot");
    }

    public virtual void StopAction()
    {
        Debug.Log("Stopaction");
    }

    public virtual void OnTriggerEnter(Collider other)
    {
    }
}