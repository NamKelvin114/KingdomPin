using System.Collections;
using System.Collections.Generic;
using Animancer;
using UnityEngine;

public class IdleAnim : State
{
    public override StateAnim GetStateAnim { get; set; } = StateAnim.Idle;

    public IdleAnim(Character character, AnimationClip clip, DoneAnimEvent doneAnimEvent) : base(character, clip, doneAnimEvent)
    {
    }
}
