using System.Collections;
using System.Collections.Generic;
using Animancer;
using UnityEngine;

public class RescueAnim : State
{
    public override StateAnim GetStateAnim { get; set; } = StateAnim.Rescue;

    public RescueAnim(Character character, AnimationClip clip, DoneAnimEvent doneAnimEvent) : base(character,
        clip,
        doneAnimEvent)
    {
    }
}