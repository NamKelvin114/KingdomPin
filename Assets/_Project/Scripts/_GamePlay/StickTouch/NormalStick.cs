using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class NormalStick : BaseStick
{
    private float destinationx=-50f;
    public override void Move()
    {
        transform.DOLocalMoveX(destinationx, StickMoveSpeed);
    }
    
}
