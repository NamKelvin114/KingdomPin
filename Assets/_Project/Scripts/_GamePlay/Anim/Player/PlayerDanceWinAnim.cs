using System.Collections;
using System.Collections.Generic;
using Animancer;
using UnityEngine;

public class PlayerDanceWinAnim : State
{
    public override StateAnim GetStateAnim { get; set; } = StateAnim.DanceWin;

    public PlayerDanceWinAnim(Character character, AnimationClip clip, DoneAnimEvent doneAnimEvent) : base(character,
        clip,
        doneAnimEvent)
    {
    }
}
