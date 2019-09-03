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
    
    private GameObject myCorotine;

    protected override void ImmediateReaction()
    {
        FSLocator.textDisplayer.reactionButton.enabled = false;

        myCorotine = CoroutineHandler.Start_Coroutine(FadeIn()).gameObject;
    }

	private IEnumerator FadeIn()
	{
        FSLocator.textDisplayer.reactionButton.enabled = true;
        FSLocator.textDisplayer.reactionButton.onClick.Invoke();

        canvas.alpha = 0.0f;
        canvas.gameObject.SetActive(true);

        canvas.GetComponent<Image>().sprite = showedImage;

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

        Destroy(myCorotine);
    }

}