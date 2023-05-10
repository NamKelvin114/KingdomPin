using System.Collections;
using System.Collections.Generic;

using Animancer;

using DG.Tweening;

using UnityEngine;

public class Player : Character, Ilost, Iwin, Iattack
{
    [SerializeField] public WinGameEvent WinGameEvent;
    [SerializeField] private DoneDoAction doneAttack;
    [SerializeField] private DoneDoAction doneCollect;
    [SerializeField] private DoneDoAction doneResCue;
    [SerializeField] public LostGameEvent LostGameEvent;
    [SerializeField] private Vector3Variable playerPosi;
    [SerializeField] private GameObject Sword;
    [SerializeField] private LayerMask checkLayer;
    private RaycastHit hitCheck;
    [SerializeField] private FloatVariable playerSpeed;
    [SerializeField] private BoxCollider boxAttack;
    public PlaySoundEvent Winanim2Sound;
    private List<GameObject> enemies = new List<GameObject>();

    public override void DoUpdate()
    {
        IgnorCollision();
        base.DoUpdate();
    }

    public override void Initialize()
    {
        boxAttack.enabled = false;
        if (IsWeapon != true)
        {
            Sword.SetActive(false);
        }
        else
        {
            Sword.SetActive(true);
        }

        base.Initialize();
    }

    public override void DoTarget()
    {
        if (IsWinGame != true && IsLostGame != true)
        {
            var checktarget = Target.transform.gameObject.tag;
            switch (checktarget)
            {
                case NameTag.Treasure:
                    StartCoroutine(DoCollect());
                    break;
                case NameTag.Rescue:
                    StartCoroutine(DoResCue());
                    break;
                case NameTag.Sword:
                    Target.gameObject.SetActive(false);
                    PickupWeepon();
                    break;
                case NameTag.Enemy:
                    Attack();
                    break;
            }
        }
        else
        {
            CharacterRb.velocity = Vector3.zero;
        }
    }

    public void WinGame()
    {
        IsWinGame = true;
        DoCompleteLevel();
    }

    void PickupWeepon()
    {
        IsWeapon = true;
        Target = null;
        Sword.SetActive(true);
    }

    public void Attack()
    {
        StartCoroutine(KillEnemy());
    }

    IEnumerator KillEnemy()
    {
        if (IsWeapon == false)
        {
            SetState(new IdleAnim(this, IdleAnim, null));
        }
        else
        {
            var getRandomAttack = Random.Range(0, 2);
            switch (getRandomAttack)
            {
                case 0:
                    SetState(new AttackWeaponAnim(this, AttackWeaponAnim, doneAttack));
                    yield return new WaitForSeconds(0.19f);
                    boxAttack.enabled = true;
                    break;
                case 1:
                    SetState(new AttackWeaponAnim2(this, AttackWeaponAnim2, doneAttack));
                    yield return new WaitForSeconds(0.19f);
                    boxAttack.enabled = true;
                    break;
            }
        }
    }

    public void LostGame()
    {
        IsLostGame = true;
        LostGameEvent?.Raise(LostGameEvent);
    }

    public void DoneAttack()
    {
        if (IsDead != true)
        {
            Target = null;
            boxAttack.enabled = false;
        }
    }

    public void CompleteLevel()
    {
        WinGame();
    }

    void DoCompleteLevel()
    {
        if (IsDead != true)
            CharacterRb.velocity = Vector3.zero;
        transform.DORotate(new Vector3(0, 180, 0), 0.1f, RotateMode.Fast)
            .OnComplete((() => StartCoroutine(SetRanDomWinAnim())));
    }

    IEnumerator SetRanDomWinAnim()
    {
        var getrandomwinanim = Random.Range(0, 2);
        switch (getrandomwinanim)
        {
            case 0:
                SetState(new PlayerDanceWinAnim2(this, DanceWin2, null));
                Winanim2Sound.Raise();
                yield return new WaitForSeconds(0.2f);
                WinGameEvent?.Raise(WinGameEvent);
                break;
            case 1:
                SetState(new PlayerDanceWinAnim(this, DanceWin, null));
                yield return new WaitForSeconds(1);
                WinGameEvent?.Raise(WinGameEvent);
                break;
        }
    }

    public IEnumerator DoCollect()
    {
        SetState(new IdleAnim(this, IdleAnim, null));
        yield return new WaitForSeconds(0.1f);
        SetState(new CollectAnim(this, LootAnim, doneCollect));
        playerPosi.Value = transform.position;
        playerPosi.Raise();
    }

    IEnumerator DoResCue()
    {
        SetState(new IdleAnim(this, IdleAnim, null));
        yield return new WaitForSeconds(0.1f);
        SetState(new RescueAnim(this, RescueAnim, doneResCue));
    }

    public override void StartRun()
    {
        Move();
        if (Physics.Raycast(RaycastPosi.position, transform.forward, out hitCheck, 0.6f, checkLayer))
        {
            if (hitCheck.transform.CompareTag(NameTag.Treasure) || hitCheck.transform.CompareTag(NameTag.Rescue) ||
                hitCheck.transform.CompareTag(NameTag.Sword) || hitCheck.transform.CompareTag(NameTag.Enemy))
            {
                Target = hitCheck.transform;
            }
            else
            {
                transform.Rotate(0, -180, 0);
            }
        }

        Debug.DrawRay(RaycastPosi.position, transform.forward * 0.6f, Color.blue);
        base.StartRun();
    }

    void IgnorCollision()
    {
        Physics.IgnoreLayerCollision(7, 8, true);
        Physics.IgnoreLayerCollision(8, 9, true);
        Physics.IgnoreLayerCollision(8, 12, true);
        Physics.IgnoreLayerCollision(8, 11, true);
    }

    public override void Move()
    {
        if (IsWinGame != true && IsLostGame != true)
        {
            SetState(new RunAnim(this, RunAnim, null));
            CharacterRb.velocity = transform.forward * playerSpeed.Value;
            base.Move();
        }
        else
        {
            CharacterRb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(NameTag.Enemy))
        {
            enemies.Add(other.gameObject);
            foreach (var getnemeny in enemies)
            {
                var getobj = getnemeny.gameObject.GetComponent<Character>();
                getobj.IsDead = true;
                getobj.DieSound.Raise();
            }
        }
    }
}