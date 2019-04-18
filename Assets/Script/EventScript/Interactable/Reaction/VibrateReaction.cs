using UnityEngine;


public class VibrateReaction : DelayedReaction
{
	protected override void ImmediateReaction()
	{
		#if UNITY_ANDROID
		Handheld.Vibrate();
		#endif
	}
}