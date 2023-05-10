using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AxisStick : BaseStick
{
    [SerializeField] private Transform head1;
    [SerializeField] private Transform head2;
    [SerializeField] private GameObject pin;
    private bool _isTop;
    private bool isTouch;

    public override void Move()
    {
        if (isTouch)
        {
            isTouch = false;
            if (_isTop != false)
            {
                pin.transform.DOMoveY(head2.position.y, StickMoveSpeed).OnComplete((() =>
                {
                    _isTop = false;
                    isTouch = true;
                }));
            }
            else
            {
                pin.transform.DOMoveY(head1.position.y, StickMoveSpeed*0.5f).OnComplete((() =>
                {
                    _isTop = true;
                    isTouch = true;
                }));
            }
        }

        base.Move();
    }

    public override void Initialize()
    {
        isTouch = true;
        _isTop = true;
        pin.transform.position = head1.position;
        base.Initialize();
    }
}