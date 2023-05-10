using System.Collections;
using System.Collections.Generic;
using Animancer;
using UnityEngine;

public class DieAnim : State
{
    public override StateAnim GetStateAnim { get; set; } = StateAnim.Die;

    public DieAnim(Character character, AnimationClip clip, DoneAnimEvent doneAnimEvent) : base(character,
        clip,
        doneAnimEvent)
    {
    }
}