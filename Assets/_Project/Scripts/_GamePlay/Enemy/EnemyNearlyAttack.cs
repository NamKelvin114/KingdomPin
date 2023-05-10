using System;
using System.Collections;
using System.Collections.Generic;

using Animancer;

using UnityEngine;

using DG.Tweening;

using Pancake;

public class EnemyNearlyAttack : Character, Iattack
{
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private FloatVariable enemySpeed;
    [SerializeField] private DoneDoAction doneAttack;
    [SerializeField] private StateDirection StateDirection;
    private RaycastHit hitCheck;
    private Vector3 previousRotaion;
    [SerializeField] private LayerMask checkLayer;

    public override void Initialize()
    {
        boxCollider.enabled = false;
        base.Initialize();
    }

    public void Attack()
    {
        StartCoroutine(DoAttack());
    }

    public override void Move()
    {
        if (IsWinGame != true)
        {
            SetState(new RunAnim(this, RunAnim, null));
            CharacterRb.velocity = transform.forward * enemySpeed.Value;
            base.Move();
        }
        else
        {
            CharacterRb.velocity = Vector3.zero;
        }
    }

    public override void DoUpdate()
    {
        IgnorCollision();
        base.DoUpdate();
    }

    void IgnorCollision()
    {
        Physics.IgnoreLayerCollision(11, 9, true);
        Physics.IgnoreLayerCollision(11, 8, true);
        Physics.IgnoreLayerCollision(11, 12, true);
        Physics.IgnoreLayerCollision(11, 11, true);
    }

    public override void StartRun()
    {
        Move();
        if (Physics.Raycast(RaycastPosi.position, transform.forward, out hitCheck, 0.8f, checkLayer))
        {
            if (hitCheck.transform.CompareTag(NameTag.Player))
            {
                Target = hitCheck.transform;
            }
            else
            {
                transform.Rotate(0, -180, 0);
                previousRotaion = transform.rotation.eulerAngles;
            }
        }

        Debug.DrawRay(RaycastPosi.position, transform.forward * 0.6f, Color.blue);
        base.StartRun();
    }

    public override void DoTarget()
    {
        var checktarget = Target.transform.gameObject.tag;
        switch (checktarget)
        {
            case NameTag.Player:
                Attack();
                break;
            case NameTag.Barrier:
                break;
        }
    }

    public void Reset()
    {
        if (IsStartRun!=false)
        {
            transform.rotation = Quaternion.AngleAxis(previousRotaion.y, Vector3.up);
            Debug.Log(previousRotaion);
        }
        Target = null;
        boxCollider.enabled = false;
    }

    IEnumerator DoAttack()
    {
        if (Target.position.x >= this.transform.position.x)
        {
            transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }
        else
        {
            transform.rotation = Quaternion.AngleAxis(-180, Vector3.up);
        }

        var gettarget = Target.gameObject.GetComponent<Player>() as Player;
        if (gettarget.IsWeapon == false)
        {
            SetState(new AttackAnim(this, AttackAnim, doneAttack));
            yield return new WaitForSeconds(0.2f);
            boxCollider.enabled = true;
        }
        else
        {
            SetState(new IdleAnim(this, IdleAnim, null));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(NameTag.Player))
        {
            var getobj = other.gameObject.GetComponent<Player>();
            getobj.IsDead = true;
        }
    }

    public void Win()
    {
        CharacterRb.velocity = Vector3.zero;
        IsWinGame = true;
        IsStartRun = false;
        transform.DORotate(new Vector3(0, 180, 0), 0.1f, RotateMode.Fast)
            .OnComplete((() => SetState(new DanceWinAnim(this, DanceWin, null))));
    }
}

public enum StateDirection
{
    Left,
    Right,
    Middle,
}