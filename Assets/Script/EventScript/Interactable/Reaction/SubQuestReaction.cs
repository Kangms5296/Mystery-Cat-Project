using UnityEngine;

public class SubQuestReaction : DelayedReaction
{
	public ConditionObjectInteraction conditionObjectInteraction;
	public SubQuest subQuest;

	protected override void SpecificInit()
	{
	}


	protected override void ImmediateReaction()
	{
		//conditionObjectInteraction.subQuest = subQuest;
		//conditionObjectInteraction.ButtonRefresh ();

	}
}