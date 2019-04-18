using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneChoiceFrame : MonoBehaviour {

    public Text firstText;

    public Button firstButton;

    public AudioSource touchAudioSource;
    public AudioClip touchAudioClip;



    public void ShowChoiceFrame(string firstText, ReactionCollection firstReaction)
    {
        gameObject.SetActive(true);
        this.firstText.text = firstText;

        firstButton.onClick.AddListener(delegate {
            transform.parent.GetComponent<TextDisplayer>().reactionButton.enabled = true;
            firstReaction.InitAndReact();
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
    }


    public void HideChoiceFrame()
    {
        touchAudioSource.clip = touchAudioClip;
        touchAudioSource.Play();

        firstButton.enabled = false;

        firstButton.onClick.RemoveAllListeners();

        gameObject.SetActive(false);
    }

}
