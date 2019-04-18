using UnityEngine;

public class StopMoveReaction : DelayedReaction
{
	//raycast m_raycast;

	protected override void SpecificInit()
	{
		//m_raycast = FindObjectOfType<raycast> ();
	}


	protected override void ImmediateReaction()
	{
		//m_raycast.enabled = false;
	}
}