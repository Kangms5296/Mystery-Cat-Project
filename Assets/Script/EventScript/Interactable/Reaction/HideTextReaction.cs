using UnityEngine;

public class HideTextReaction : DelayedReaction
{
	
	protected override void ImmediateReaction()
	{
		FSLocator.textDisplayer.HideDialogueHolder ();
		FSLocator.characterDisplayer.HideImage ();
	}
}