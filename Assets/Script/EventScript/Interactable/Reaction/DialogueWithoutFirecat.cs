using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueWithoutFirecat : DelayedReaction
{
	public GameObject dialogue;
	public Text text;

	protected override void ImmediateReaction()
	{
		FSLocator.textDisplayer.dialougeFrameDisplayer = dialogue;
		FSLocator.textDisplayer.dialougeText = text;
	}
		
}