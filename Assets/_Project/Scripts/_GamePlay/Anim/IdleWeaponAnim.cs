using System.Collections;
using System.Collections.Generic;
using Animancer;
using UnityEngine;

public class IdleWeaponAnim : State
{
    public override StateAnim GetStateAnim { get; set; } = StateAnim.IdleWeaPon;

    public IdleWeaponAnim(Character character, AnimationClip clip, DoneAnimEvent doneAnimEvent) : base(character, clip,
        doneAnimEvent)
    {
    }
}