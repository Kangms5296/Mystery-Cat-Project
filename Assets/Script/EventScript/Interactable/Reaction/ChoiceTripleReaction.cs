using UnityEngine;
using System.Collections;
public class ChoiceTripleReaction : DelayedReaction
{

	public string firstChoice;
	public string secondChoice;
	public string thirdChoice;

	public ReactionCollection firstReactionCollection;
	public ReactionCollection secondReactionCollection;
	public ReactionCollection thirdReactionCollection;

	protected override void SpecificInit()
	{

	}


	protected override void ImmediateReaction()
	{
		FSLocator.textDisplayer.tripleChoiceFrame.ShowChoiceFrame (firstChoice, secondChoice, thirdChoice, firstReactionCollection, secondReactionCollection, thirdReactionCollection);

	}
}