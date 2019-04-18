using UnityEngine;

public class StopCameraReaction : DelayedReaction
{
	protected override void SpecificInit()
	{

	}


	protected override void ImmediateReaction()
	{
		//FindObjectOfType<tutorialGame> ().gameCamera.cameraSpeed = 0;
	}
}