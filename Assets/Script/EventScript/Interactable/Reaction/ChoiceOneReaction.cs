using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceOneReaction : DelayedReaction
{
    public string firstChoice;

    public ReactionCollection firstReactionCollection;

    protected override void SpecificInit()
    {

    }


    protected override void ImmediateReaction()
    {
        FSLocator.textDisplayer.oneChoiceFrame.ShowChoiceFrame(firstChoice, firstReactionCollection);

    }
}
