using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ImageChangeReaction : DelayedReaction
{
    public Image image;
    public Sprite sprite;


    protected override void ImmediateReaction()
    {
        image.sprite = sprite;
    }
}
