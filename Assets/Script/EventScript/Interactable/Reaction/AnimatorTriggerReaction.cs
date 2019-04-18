using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTriggerReaction : DelayedReaction
{
    public Animator character;
    public string triggerName;

    protected override void SpecificInit()
    {

    }

    protected override void ImmediateReaction()
    {
        character.SetTrigger(triggerName);
    }
}
