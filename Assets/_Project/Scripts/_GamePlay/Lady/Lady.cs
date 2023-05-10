using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Lady : Character
{
    public PlaySoundEvent GetRescueSound;
    public DoneDoAction DoneGetResCue;

    public void GetRescue()
    {
        SetState(new GetRescueAnim(this,GetRescueAnim,DoneGetResCue));
        GetRescueSound.Raise();
        BoxCollider.enabled = false;
        // CharacterRb.isKinematic = true;
    }

    public void WinAnim()
    {
        transform.DORotate(new Vector3(0, 180, 0), 0.1f, RotateMode.Fast)
            .OnComplete((() => SetState(new DanceWinAnim(this,DanceWin,null))));
    }
}