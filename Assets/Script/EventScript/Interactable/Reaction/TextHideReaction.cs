using UnityEngine;

public class TextHideReaction : DelayedReaction
{
	public GameObject text;


	protected override void SpecificInit()
	{

	}


	protected override void ImmediateReaction()
	{
		text.SetActive (false);
	}
}