using System.Collections.Generic;
using Animancer;
using Pancake;
using UnityEngine;

public abstract class State
{
    public virtual StateAnim GetStateAnim { get; set; }
    protected Character Character;
    private DoneAnimEvent _doneAnimEvent;
    private AnimationClip _clip;

    public virtual void EnterState()
    {
       Character.DoState(_clip,_doneAnimEvent);
    }

    public virtual void ExitState()
    {
    }

    public virtual void Tick()
    {
    }

    public State(Character character, AnimationClip clip, DoneAnimEvent doneAnimEvent)
    {
        Character = character;
        _clip = clip;
        _doneAnimEvent = doneAnimEvent;
    }

    // private IEnumerator<float> ResetState(AnimationClip clip)
    // {
    //     if (clip != null)
    //     {
    //         var state = Amim.Play(clip);
    //         yield return Timing.WaitForSeconds(state.Duration);
    //         if (_doneAnimEvent != null) _doneAnimEvent.Raise(_doneAnimEvent);
    //     }
    // }
    //
    // protected void Invoke(AnimationClip clip)
    // {
    //     Timing.KillCoroutines(Handle);
    //     Handle = Timing.RunCoroutine(ResetState(clip));
    // }
}