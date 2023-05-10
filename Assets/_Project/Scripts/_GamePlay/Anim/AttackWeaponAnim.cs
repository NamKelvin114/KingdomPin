using System.Collections;
using System.Collections.Generic;
using Animancer;
using UnityEngine;

public class AttackWeaponAnim : State
{
    public override StateAnim GetStateAnim { get; set; } = StateAnim.AttackWeapon;

    public AttackWeaponAnim(Character character, AnimationClip clip, DoneAnimEvent doneAnimEvent) : base(character,
        clip,
        doneAnimEvent)
    {
    }
}
