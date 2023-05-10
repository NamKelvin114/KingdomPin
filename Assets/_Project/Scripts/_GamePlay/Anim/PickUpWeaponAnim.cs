using System.Collections;
using System.Collections.Generic;
using Animancer;
using UnityEngine;

public class PickUpWeaponAnim : State
{
    public override StateAnim GetStateAnim { get; set; } = StateAnim.PickupWeapon;

    public PickUpWeaponAnim(Character character, AnimationClip clip, DoneAnimEvent doneAnimEvent) : base(character,
        clip,
        doneAnimEvent)
    {
    }
}