using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaSeed : BaseObject
{
    [SerializeField] private GameObject smoke;
    public override void DoUpdate()
    {
        Physics.IgnoreLayerCollision(13, 8, true);
        base.DoUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(NameTag.Player)|| other.gameObject.CompareTag(NameTag.Enemy)|| other.gameObject.CompareTag(NameTag.Rescue))
        {
            var getobj = other.GetComponent<Character>();
            getobj.IsDead = true;
        }
        else if (other.gameObject.CompareTag(NameTag.Treasure))
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag(NameTag.WaterSeed))
        {
            Instantiate(smoke, transform.position, transform.rotation);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}