using System.Collections;
using System.Collections.Generic;
using Animancer;
using UnityEngine;

public class JumpAnim : State
{
    public override StateAnim GetStateAnim { get; set; } = StateAnim.Jump;

    public JumpAnim(Character character, AnimationClip clip, DoneAnimEvent doneAnimEvent) : base(character,
        clip,
        doneAnimEvent)
    {
    }
}