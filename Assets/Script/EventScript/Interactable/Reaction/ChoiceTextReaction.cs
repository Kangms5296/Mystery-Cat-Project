using UnityEngine;
using System.Collections;
public class ChoiceTextReaction : DelayedReaction
{

	public string firstChoice;
	public string secondChoice;

	public ReactionCollection firstReactionCollection;
	public ReactionCollection secondReactionCollection;

	protected override void SpecificInit()
	{

	}


	protected override void ImmediateReaction()
	{
		FSLocator.textDisplayer.choiceFrame.ShowChoiceFrame (firstChoice, secondChoice, firstReactionCollection, secondReactionCollection);

	}
}