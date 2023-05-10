using System.Collections;
using System.Collections.Generic;
using Pancake;
using UnityEngine;

public class BearTrap : TrapElement
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(NameTag.Player))
        {
            Debug.Log(other.name);
            var target = other.GetComponent<Character>();
            target.IsDead = true;
            TrapBoxCollider.Destroy();
            Destroy(gameObject);
        }
        base.OnTriggerEnter(other);
    }
}