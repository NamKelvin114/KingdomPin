using System.Collections;
using System.Collections.Generic;
using Animancer;
using UnityEngine;

public class AttackAnim : State
{
    public override StateAnim GetStateAnim { get; set; } = StateAnim.Attack;

    public AttackAnim(Character character, AnimationClip clip, DoneAnimEvent doneAnimEvent) : base(character, clip,
        doneAnimEvent)
    {
    }
}