using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceFrame : MonoBehaviour {

	public Text firstText;
	public Text secondText;

	public Button firstButton;
	public Button secondButton;

    public AudioSource touchAudioSource;
    public AudioClip touchAudioClip;

    public void ShowChoiceFrame(string firstText, string secondText, ReactionCollection firstReaction, ReactionCollection secondReaction){
		gameObject.SetActive (true);
		this.firstText.text = firstText;
		this.secondText.text = secondText;

		firstButton.onClick.AddListener(delegate{
			transform.parent.GetComponent<TextDisplayer>().reactionButton.enabled = true;
			firstReaction.InitAndReact();
			HideChoiceFrame();
		});
		secondButton.onClick.AddListener (delegate {
			transform.parent.GetComponent<TextDisplayer>().reactionButton.enabled = true;
			secondReaction.InitAndReact ();
			HideChoiceFrame();
		});

        StartCoroutine(Visible());
	}

    IEnumerator Visible()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        float alpha = 0;
        while (alpha < 1)
        {
            alpha += Time.deltaTime * 5;
            canvasGroup.alpha = alpha;
            yield return null;
        }
        canvasGroup.alpha = 1;

        firstButton.enabled = true;
        secondButton.enabled = true;
    }

	public void HideChoiceFrame(){

        touchAudioSource.clip = touchAudioClip;
        touchAudioSource.Play();

        firstButton.enabled = false;
        secondButton.enabled = false;

        firstButton.onClick.RemoveAllListeners ();
		secondButton.onClick.RemoveAllListeners ();


		gameObject.SetActive (false);
	}
}
