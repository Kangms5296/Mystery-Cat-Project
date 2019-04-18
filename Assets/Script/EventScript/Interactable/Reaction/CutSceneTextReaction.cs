using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneTextReaction : DelayedReaction
{
    public string Text;
	public float disolve;
	public float maintain;
	public float speed;

 
    protected override void ImmediateReaction()
    {
		FSLocator.cutSceneTextDisplayer.Say (Text, disolve, maintain, speed);
    }
}
