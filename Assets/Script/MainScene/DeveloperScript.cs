using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperScript : MonoBehaviour {

    public CanvasGroup hangul;
    public CanvasGroup english;

    public AudioSource source;

    private bool isHangul = true;

    private Coroutine changingCoroutine;

    public void OnClickNext()
    {
        source.Play();

        isHangul = !isHangul;

        if (changingCoroutine == null)
            changingCoroutine = StartCoroutine(Changing());
    }

    IEnumerator Changing()
    {
        float conValue;
        if (isHangul)
            conValue = 0;
        else
            conValue = 1;

            while (true)
        {
            hangul.alpha = conValue;
            english.alpha = 1 - conValue;

            if (isHangul)
                conValue += Time.deltaTime;
            else
                conValue -= Time.deltaTime;

            if (isHangul && conValue > 1)
                break;
            if (!isHangul && conValue < 0)
                break;
                
                yield return null;
        }
        if (isHangul)
        {
            hangul.alpha = 1;
            english.alpha = 0;
        }
        else
        {
            hangul.alpha = 0;
            english.alpha = 1;
        }
        changingCoroutine = null;
    }

}
