using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

using UnityEngine;

public class EnemyShoot : Character, Iattack
{
    public PlaySoundEvent EnemyShootSound;
    [SerializeField] private DoneDoAction doneShoot;

    public void Attack()
    {
        StartCoroutine(DoAttack());
    }

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shotPosi;

    public Transform AttackPosi
    {
        get => shotPosi;
        set => shotPosi = value;
    }

    public override void DoTarget()
    {
        var checktarget = Target.transform.gameObject.tag;
        switch (checktarget)
        {
            case NameTag.Player:
                Attack();
                break;
            case NameTag.Rescue:
                Attack();
                break;
            case NameTag.Barrier:
                break;
        }
    }

    public void Reset() => Target = null;

    IEnumerator DoAttack()
    {
        var gettarget = Target.gameObject.GetComponent<Player>() as Player;
        if (gettarget.IsWeapon == false)
        {
            SetState(new AttackAnim(this, AttackAnim, doneShoot));
            yield return new WaitForSeconds(0.11f);
            EnemyShootSound.Raise();
            Instantiate(bullet, shotPosi.position, shotPosi.rotation);
        }
        else
        {
            SetState(new IdleAnim(this, IdleAnim, null));
        }
    }

    public void Win()
    {
        if (IsDead != true)
        {
            transform.DORotate(new Vector3(0, 180, 0), 0.1f, RotateMode.Fast)
                .OnComplete((() => SetState(new DanceWinAnim(this, DanceWin, null))));
        }
    }
}