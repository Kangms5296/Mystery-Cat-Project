using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChangeReaction : DelayedReaction
{
    public SpriteRenderer target;
    public Sprite wantToChange;

    protected override void SpecificInit()
    { }


    protected override void ImmediateReaction()
    {
        target.sprite = wantToChange;
    }
}
