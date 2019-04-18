using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class CutSceneStartReaction : DelayedReaction
{
    // 화면에 cutScene 될 이미지
    public Sprite showedImage;

	float currentTime;

	public float fadeTime;
	public CanvasGroup canvas;

	public List<GameObject> activeFalseObjectList;
	public List<GameObject> activeTrueObjectList;

	CoroutineHandler myCoroutine;

    protected override void ImmediateReaction()
    {
		canvas.alpha = 0.0f;
		canvas.gameObject.SetActive (true);

        canvas.GetComponent<Image>().sprite = showedImage;
	
		myCoroutine = CoroutineHandler.Start_Coroutine (FadeIn ());
    }

	private IEnumerator FadeIn()
	{
		currentTime = Time.time;

		if (fadeTime == 0f) {
			canvas.alpha = 1f;
			yield return null;
		} else {
			while (currentTime + fadeTime > Time.time) {
				canvas.alpha += (float)((float)Time.deltaTime / fadeTime);
				yield return null;
			}
		}

		for (int i = 0; i < activeFalseObjectList.Count; i++) {
			activeFalseObjectList [i].SetActive (false);
		}

		for (int i = 0; i < activeTrueObjectList.Count; i++) {
			activeTrueObjectList [i].SetActive (true);
		}

		GameObject go = myCoroutine.gameObject;
		Destroy(go);
	}

}