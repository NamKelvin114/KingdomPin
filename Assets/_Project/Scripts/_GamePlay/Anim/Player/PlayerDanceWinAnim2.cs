using System.Collections;
using System.Collections.Generic;
using Animancer;
using UnityEngine;

public class PlayerDanceWinAnim2 : State
{
    public override StateAnim GetStateAnim { get; set; } = StateAnim.DanceWin2;

    public PlayerDanceWinAnim2(Character character, AnimationClip clip, DoneAnimEvent doneAnimEvent) : base(character,
        clip,
        doneAnimEvent)
    {
    }
}
