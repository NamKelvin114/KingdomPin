using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Threading.Tasks;

using Animancer;

using DG.Tweening;

using Pancake.Threading.Tasks.Triggers;

using UnityEngine;

public abstract class Character : MonoBehaviour, Imove
{
    [SerializeField] public Transform RaycastPosi;
    [SerializeField, Range(0, 10f)] public float RaycastTargetDistance;
    [SerializeField, Range(0, 10f)] public float RaycastGroundDistance;
    [SerializeField] private DoneDoAction doneDie;
    [SerializeField] public LayerMask TargetLayer;
    [SerializeField] public LayerMask GroundLayer;
    [SerializeField] public RaycastHit HitTarget;
    [SerializeField] public Transform Target;
    [SerializeField] public StateAnim CurrentState;
    [SerializeField] public AnimancerComponent AnimancerComponent;
    [SerializeField] public AnimationClip LootAnim;
    [SerializeField] public AnimationClip IdleAnim;
    [SerializeField] public AnimationClip RunAnim;
    [SerializeField] public AnimationClip RescueAnim;
    [SerializeField] public AnimationClip GetRescueAnim;
    [SerializeField] public AnimationClip JumpAnim;
    [SerializeField] public AnimationClip DanceWin;
    [SerializeField] public AnimationClip DanceWin2;
    [SerializeField] public AnimationClip AttackAnim;
    [SerializeField] public AnimationClip AttackWeaponAnim2;
    [SerializeField] public AnimationClip DieAnim;
    [SerializeField] public AnimationClip IdleWeaponAnim;
    [SerializeField] public AnimationClip AttackWeaponAnim;
    [SerializeField] public AnimationClip PickupWeapon;
    [SerializeField] public PlaySoundEvent DieSound;
    [SerializeField] public bool IsStartRun;
    [SerializeField] public float Range;
    [SerializeField] public BoxCollider BoxCollider;
    public Rigidbody CharacterRb;
    private StateAnim previousState;
    public bool IsGround;
    private bool isDotarget;
    private bool isIdle;
    public bool IsWinGame;
    public bool IsLostGame;
    public bool IsWeapon;
    public bool IsDead;
    public bool isDoneRotate;

    public virtual void Move()
    {
    }

    public void SetState(State getState)
    {
        CurrentState = getState.GetStateAnim;
        if (CurrentState != previousState)
        {
            getState.EnterState();
            previousState = CurrentState;
        }
    }

    public void DoState(AnimationClip clip, DoneAnimEvent doneAnimEvent)
    {
        if (clip != null)
        {
            StartCoroutine(Invoke(clip, doneAnimEvent));
        }
    }

    IEnumerator Invoke(AnimationClip clip, DoneAnimEvent doneAnimEvent)
    {
        var anim = AnimancerComponent.Play(clip);
        yield return anim;
        doneAnimEvent?.Raise(doneAnimEvent);
    }

    public void SetDead()
    {
        if (IsDead != false && IsWeapon != true)
        {
            CharacterRb.velocity = new Vector3(0, CharacterRb.velocity.y, CharacterRb.velocity.z);
            SetState(new DieAnim(this, DieAnim, doneDie));
            BoxCollider.enabled = false;
        }
    }

    public virtual void Initialize()
    {
        if (!IsStartRun)
        {
            transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }

        IsDead = false;
        IsLostGame = false;
        isIdle = true;
        IsWinGame = false;
        CurrentState = StateAnim.Idle;
        previousState = StateAnim.Non;
        isDotarget = false;
        CharacterRb = GetComponent<Rigidbody>();
        BoxCollider = GetComponent<BoxCollider>();
        BoxCollider.enabled = true;
    }

    public virtual void DoUpdate()
    {
        SetDead();
        if (RaycastPosi != null && IsLostGame == false && IsDead != true && !IsWinGame)
        {
            ShootRaycastTargetDown();
            if (IsGround)
            {
                if (!IsStartRun)
                {
                    if (Target == null)
                    {
                        isDoneRotate = false;
                        isDotarget = false;
                        isIdle = true;
                        DoRaycast();
                    }
                }
                else
                {
                    if (Target == null)
                    {
                        isDoneRotate = true;
                        isDotarget = false;
                        StartRun();
                    }
                }

                if (Target != null)
                {
                    if (Target.gameObject.CompareTag(NameTag.Barrier))
                    {
                        isDoneRotate = false;
                        isIdle = true;
                        DoRaycast();
                    }
                    else
                    {
                        CheckRange();
                    }
                }
            }
            else
            {
                SetState(new JumpAnim(this, JumpAnim, null));
            }
        }
    }

    void DoRaycast()
    {
        SetIdle();
        ShootRaycastTargetLeft();
        ShootRaycastTargetRight();
    }

    void SetIdle()
    {
        if (CurrentState != StateAnim.Die && CurrentState != StateAnim.DanceWin)
        {
            if (isIdle)
            {
                if (IsWeapon != false)
                {
                    SetState(new IdleWeaponAnim(this, IdleWeaponAnim, null));
                }
                else
                {
                    SetState(new IdleAnim(this, IdleAnim, null));
                }
            }
        }
    }

    private void ShootRaycastTargetRight()
    {
        Debug.DrawRay(RaycastPosi.position, Vector3.right * RaycastTargetDistance, Color.red);
        if (Physics.Raycast(RaycastPosi.position, Vector3.right, out HitTarget, RaycastTargetDistance, TargetLayer))
        {
            Target = HitTarget.transform;
            DoRotate();
        }
        else
        {
        }
    }

    private void ShootRaycastTargetLeft()
    {
        Debug.DrawRay(RaycastPosi.position, Vector3.left * RaycastTargetDistance, Color.red);
        if (Physics.Raycast(RaycastPosi.position, Vector3.left, out HitTarget, RaycastTargetDistance, TargetLayer))
        {
            Target = HitTarget.transform;
            DoRotate();
        }
        else
        {
        }
    }

    private void ShootRaycastTargetDown()
    {
        Debug.DrawRay(RaycastPosi.position, Vector3.down * RaycastGroundDistance, Color.red);
        if (Physics.Raycast(RaycastPosi.position, Vector3.down, out HitTarget, RaycastGroundDistance, GroundLayer))
        {
            IsGround = true;
        }
        else
        {
            Physics.gravity = new Vector3(0, -20, 0);
            CharacterRb.AddForce(Vector3.down * 6);
            IsGround = false;
        }
    }

    void CheckRange()
    {
        if (isDotarget != true)
        {
            isIdle = false;
            if ((Mathf.Abs(Target.position.x - transform.position.x)) <= Range)
            {
                CharacterRb.velocity = Vector3.zero;
                if (isDoneRotate != false)
                {
                    DoTarget();
                    isDotarget = true;
                }
            }
            else
            {
                if (isDoneRotate != false)
                {
                    Move();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        DoUpdate();
    }

    public virtual void DoTarget()
    {
    }

    public virtual void StartRun()
    {
    }

    private void DoRotate()
    {
        if (Target != null && !Target.CompareTag(NameTag.Barrier))
        {
            if (Target.position.x >= transform.position.x)
            {
                // transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
                transform.DORotate(new Vector3(0, 90, 0), 0.1f, RotateMode.Fast)
                    .OnComplete((() => isDoneRotate = true));
            }
            else
            {
                transform.DORotate(new Vector3(0, -90, 0), 0.1f, RotateMode.Fast)
                    .OnComplete((() => isDoneRotate = true));
            }
        }
    }
}