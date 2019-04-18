using UnityEngine;
using System.Collections;

public class HideCutScenePannel : DelayedReaction
{

	float currentTime;

	public int fadeTime;
	CanvasGroup canvas;

	GameObject myCorotine;

	protected override void ImmediateReaction()
	{
		//canvas = TutorialController.Instance.cutScenePannel;



		myCorotine = CoroutineHandler.Start_Coroutine (FadeOut ()).gameObject;
	}

	private IEnumerator FadeOut()
	{
		currentTime = Time.time;

		while (currentTime + fadeTime > Time.time)
		{
			canvas.alpha -= (Time.deltaTime / fadeTime);
			yield return null;
		}
		//TutorialController.Instance.cutScenePannel.gameObject.SetActive (false);

		Destroy(myCorotine);
	}
}