using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggerReaction : DelayedReaction
{
    public Animation target;
    public string triggerName;

    protected override void SpecificInit()
    {

    }

    protected override void ImmediateReaction()
    {
        target.Play(triggerName);
    }
}
