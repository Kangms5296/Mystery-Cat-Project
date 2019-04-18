using System.Collections;
using UnityEngine;

public class FadeOutInReaction : DelayedReaction
{
    public float fadeTime = 1.0f;
    public Color fadeColor = new Color(0, 0, 0, 1);
    protected override void ImmediateReaction()
    {
        FSLocator.fadeDisplayer.FadeOutIn(fadeColor, fadeTime);
    }
}
