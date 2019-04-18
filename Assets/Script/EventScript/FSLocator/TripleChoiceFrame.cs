using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TripleChoiceFrame : MonoBehaviour {

	public Text firstText;
	public Text secondText;
	public Text thirdText;

	public Button firstButton;
	public Button secondButton;
	public Button thirdButton;

    public AudioSource touchAudioSource;
    public AudioClip touchAudioClip;

    public void ShowChoiceFrame(string firstText, string secondText, string thirdText, ReactionCollection firstReaction, ReactionCollection secondReaction, ReactionCollection thirdReaction){
		gameObject.SetActive (true);
		this.firstText.text = firstText;
		this.secondText.text = secondText;
		this.thirdText.text = thirdText;

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
		thirdButton.onClick.AddListener (delegate {
			transform.parent.GetComponent<TextDisplayer>().reactionButton.enabled = true;
			thirdReaction.InitAndReact ();
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
        thirdButton.enabled = true;
    }


    public void HideChoiceFrame(){

        touchAudioSource.clip = touchAudioClip;
        touchAudioSource.Play();

        firstButton.enabled = false;
        secondButton.enabled = false;
        thirdButton.enabled = false;

        firstButton.onClick.RemoveAllListeners ();
		secondButton.onClick.RemoveAllListeners ();
		thirdButton.onClick.RemoveAllListeners ();

		gameObject.SetActive (false);
	}
}
