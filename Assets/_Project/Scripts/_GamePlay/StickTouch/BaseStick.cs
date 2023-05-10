using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStick : MonoBehaviour,IMove
{
    [SerializeField, Range(0, 10f)] public float StickMoveSpeed = 2.0f;
    public PlaySoundEvent PlaySoundEvent;
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        DoUpdate();
    }

    public virtual void Initialize()
    {
        
    }

    public virtual void DoUpdate()
    {
        
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag(NameTag.Stick_Touch))
        {
            PlaySoundEvent.Raise();
            Move();
        }
    }

    public virtual void Move()
    {
        
    }
}
