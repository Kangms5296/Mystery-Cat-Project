using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionCallReaction : DelayedReaction
{
    public GameObject target;

    public string functionName;

    protected override void ImmediateReaction()
    {
        target.SendMessage(functionName);
    }
}
