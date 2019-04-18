using UnityEngine;
using System.Collections;

public class LifeGainReaction : DelayedReaction
{
	//CharacterManager characterManager;

	protected override void SpecificInit ()
	{
		//TVNTManager.instance.ReferenceTVNTManager (ref characterManager);
	}

	protected override void ImmediateReaction()
	{
		//characterManager.PlayerController.LifeGain ();
	}
}