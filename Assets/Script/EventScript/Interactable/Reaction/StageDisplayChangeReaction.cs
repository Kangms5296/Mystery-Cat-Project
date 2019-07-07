using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDisplayChangeReaction : DelayedReaction
{
    public Stage changeStage;

    protected override void ImmediateReaction()
    {
        FindObjectOfType<MainGame>().stage = changeStage;
    }
}
