using UnityEngine;

public class TextShowReaction : DelayedReaction
{
	public GameObject text;


	protected override void SpecificInit()
	{

	}


	protected override void ImmediateReaction()
	{
		text.SetActive (true);

	}
}