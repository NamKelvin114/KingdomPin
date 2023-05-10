using System.Collections;
using System.Collections.Generic;
using Animancer;
using UnityEngine;

public class GetRescueAnim : State
{
    public override StateAnim GetStateAnim { get; set; } = StateAnim.GetRescue;

    public GetRescueAnim(Character character, AnimationClip clip, DoneAnimEvent doneAnimEvent) : base(character,
        clip,
        doneAnimEvent)
    {
    }
}