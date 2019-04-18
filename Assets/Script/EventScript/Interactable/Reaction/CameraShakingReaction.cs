using UnityEngine;
using System.Collections;
public class CameraShakingReaction : DelayedReaction
{
	private CameraShaking mCamera;

	public bool shakePosition;
	public bool shakeRotation;
	public float shakeIntensity;
	public float shakeDecay;

	protected override void SpecificInit ()
	{
		mCamera = FindObjectOfType<CameraShaking> ();
	}

	protected override void ImmediateReaction()
	{
		mCamera.Init (shakePosition, shakeRotation, shakeIntensity, shakeDecay);
		mCamera.DoShake ();
	}
}