using UnityEngine;

public class StartMoveReaction : DelayedReaction
{
	//raycast m_raycast;

	protected override void SpecificInit()
	{
		//m_raycast = FindObjectOfType<raycast> ();

	}


	protected override void ImmediateReaction()
	{
		//m_raycast.enabled = true;
	}
}