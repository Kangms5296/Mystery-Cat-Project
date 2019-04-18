using UnityEngine;
using System.Collections.Generic;
using PolyNav;

public class FollowReaction : DelayedReaction
{
    public Transform target;
    public Transform follower;

    protected override void SpecificInit()
    {

    }

    protected override void ImmediateReaction()
    {
        follower.GetComponent<FollowTarget>().target = target;
        follower.GetComponent<FollowTarget>().enabled = true;
    }
}
