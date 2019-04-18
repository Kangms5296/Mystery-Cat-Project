using UnityEngine;

public class SubQuestEndReaction : DelayedReaction
{
	public ConditionObjectInteraction conditionObjectInteraction;

	protected override void SpecificInit()
	{
	}


	protected override void ImmediateReaction()
	{
		//conditionObjectInteraction.subQuest = null;
	}
}