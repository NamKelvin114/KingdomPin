using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class DragonHead : TrapElement
{
    [SerializeField] private GameObject fireBreathe;
    [SerializeField] private Transform raycastCheck;
    private RaycastHit hitCheck;
    private RaycastHit hitTarget;
    [SerializeField] private LayerMask ContinueSkillLayer;
    [SerializeField] private LayerMask OneShotSkillLayer;
    [SerializeField] private BoxCollider boxCollider;

    public override void ContinutyAction()
    {
        ShootRaycastCheck(ContinueSkillLayer);
        base.ContinutyAction();
    }

    public override void ContinutyDeAction()
    {
        fireBreathe.gameObject.SetActive(false);
        TrapBoxCollider.gameObject.SetActive(false);
        base.ContinutyDeAction();
        AnimancerComponent.Stop(TrapAnim);
    }

    IEnumerator DoAnimBreathe()
    {
        AnimancerComponent.Play(TrapAnim);
        yield return new WaitForSeconds(0.2f);
        fireBreathe.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        TrapBoxCollider.gameObject.SetActive(true);
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(NameTag.Player) || other.gameObject.CompareTag(NameTag.Rescue) ||
            other.gameObject.CompareTag(NameTag.Enemy))
        {
            Debug.Log(other.gameObject.name);
            var target = other.GetComponent<Character>();
            target.IsDead = true;
            if (IsOneShot != false)
            {
                IsStopSkill = true;
            }
        }

        base.OnTriggerEnter(other);
    }

    void ShootRaycastCheck(LayerMask layerMask)
    {
        Debug.DrawRay(raycastCheck.position, transform.forward * 10, Color.blue);
        if (Physics.Raycast(raycastCheck.position, transform.forward, out hitCheck, 10, layerMask))
        {
            var getdistance = Mathf.Abs(transform.position.x - hitCheck.transform.position.x);
            StartCoroutine(DoAnimBreathe());
            fireBreathe.transform.localScale = new Vector3(fireBreathe.transform.localScale.x,
                fireBreathe.transform.localScale.y, getdistance / 10);
            boxCollider.size = new Vector3(boxCollider.size.x, boxCollider.size.y, getdistance / 1.6f);
            boxCollider.center = new Vector3(boxCollider.center.x, boxCollider.center.y,
                Mathf.Abs(boxCollider.size.z / 2) - 0.5f);
        }
    }

    public override void OneShotAction()
    {
        ShootRaycastCheck(OneShotSkillLayer);
        base.OneShotAction();
    }

    public override void StopAction()
    {
        fireBreathe.gameObject.SetActive(false);
        TrapBoxCollider.gameObject.SetActive(false);
        base.StopAction();
    }
}