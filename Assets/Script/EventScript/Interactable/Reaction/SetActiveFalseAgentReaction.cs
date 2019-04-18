using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolyNav;

public class SetActiveFalseAgentReaction : DelayedReaction
{
    public GameObject target;

    protected override void ImmediateReaction()
    {
        // 따라다니는걸 멈추고..
        target.GetComponent<FollowTarget>().enabled = false;
        target.GetComponent<PolyNavAgent>().Stop();
        target.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        target.GetComponent<PolyNavAgent>().enabled = false;
        target.GetComponent<NewCharacter>().Stop();
    }
}
