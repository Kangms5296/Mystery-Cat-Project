using UnityEngine;
using UnityEngine.UI;
public class CutSceneImageReaction : DelayedReaction
{
	public Sprite sprite;

	protected override void ImmediateReaction()
	{
		//TutorialController.Instance.cutSceneFrame.sprite = sprite;
	}
}