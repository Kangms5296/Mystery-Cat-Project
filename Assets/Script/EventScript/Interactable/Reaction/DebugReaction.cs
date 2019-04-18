using UnityEngine;


public class DebugReaction : Reaction
{
	public ConditionCollectionText[] conditionCollectionsText = new ConditionCollectionText[0];

	protected override void ImmediateReaction()
	{
		//Debug.Log ("Test Reaction");
	}
}