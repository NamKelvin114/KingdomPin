using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSeed : BaseObject
{
    public override void DoUpdate()
    {
        Physics.IgnoreLayerCollision(4,8,true);
        Physics.IgnoreLayerCollision(4,9,true);
        Physics.IgnoreLayerCollision(4,11,true);
        Physics.IgnoreLayerCollision(4,12,true);
        base.DoUpdate();
    }
}
