using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BaseObject
{
    public override void DoUpdate()
    {
        transform.Translate(Vector3.right);
        base.DoUpdate();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(NameTag.Player))
        {
            var getobj = collision.gameObject.GetComponent<Player>();
            getobj.IsDead = true;
            Destroy(gameObject);
        }
    }
}