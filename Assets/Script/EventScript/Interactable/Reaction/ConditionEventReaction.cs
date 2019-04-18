using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionEventReaction : DelayedReaction {

	public Condition condition;

	public ReactionCollection trueReaction;
	public ReactionCollection falseReaction;

	protected override void SpecificInit ()
	{

	}

	protected override void ImmediateReaction()
	{
		if (condition.satisfied) {
			trueReaction.React ();
		} else {
			falseReaction.React ();
		}
	}
}
