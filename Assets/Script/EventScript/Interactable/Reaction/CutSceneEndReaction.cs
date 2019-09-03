using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CutSceneEndReaction : DelayedReaction
{
	float currentTime;

	public float fadeTime;
	public CanvasGroup canvas;

	public List<GameObject> activeFalseObjectList;
	public List<GameObject> activeTrueObjectList;

	GameObject myCorotine;

	protected override void ImmediateReaction()
	{
        FSLocator.textDisplayer.reactionButton.enabled = false;

        myCorotine = CoroutineHandler.Start_Coroutine(FadeOut()).gameObject;
    }

	private IEnumerator FadeOut()
	{
        FSLocator.textDisplayer.reactionButton.enabled = true;
        FSLocator.textDisplayer.reactionButton.onClick.Invoke();

        currentTime = Time.time;

		if (fadeTime == 0f) {
			canvas.alpha = 0.0f;
			yield return null;


		} else {
			while (currentTime + fadeTime > Time.time) {
				
				canvas.alpha -= (float)((float)Time.deltaTime / fadeTime);

				yield return null;
			}
		}
		canvas.gameObject.SetActive (false);

		for (int i = 0; i < activeFalseObjectList.Count; i++) {
			activeFalseObjectList [i].SetActive (false);
		}
		for (int i = 0; i < activeTrueObjectList.Count; i++) {
			activeTrueObjectList [i].SetActive (true);
		}
			
		Destroy(myCorotine);
	}
}