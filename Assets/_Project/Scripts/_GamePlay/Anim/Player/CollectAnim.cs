using System.Collections;
using System.Collections.Generic;
using Animancer;
using UnityEngine;

public class CollectAnim : State
{
    public override StateAnim GetStateAnim { get; set; } = StateAnim.Collect;

    public CollectAnim(Character character, AnimationClip clip, DoneAnimEvent doneAnimEvent) : base(character,
        clip,
        doneAnimEvent)
    {
    }
}