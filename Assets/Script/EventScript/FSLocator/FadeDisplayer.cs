using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeDisplayer : MonoBehaviour
{

    public CanvasGroup fadeHolder;
    public Image m_fadeColor;
    
    public float m_fadeTime = 1.0f;

    public bool isFading
    {
        get;
        private set;
    }


    public void FadeIn(Color fadeColor, float fadeTime)
    {
        m_fadeTime = fadeTime;
        StartCoroutine(BlackFadeIn(fadeColor));
    }

    public void FadeOut(Color fadeColor, float fadeTime)
    {
        m_fadeTime = fadeTime;
        StartCoroutine(BlackFadeOut(fadeColor));
    }

    public void FadeOutIn(Color fadeColor, float fadeTime)
    {
        m_fadeTime = fadeTime;
        StartCoroutine(BlackFadeOutIn(fadeColor));
    }

    private IEnumerator BlackFadeIn(Color fadeColor)
    {
        m_fadeColor.color = fadeColor;
        fadeHolder.blocksRaycasts = true;

        
        StopCoroutine("BlackFadeOut");

        fadeHolder.alpha = 1.0f;

        float currentTime = Time.time;

        while (currentTime + m_fadeTime > Time.time)
        {

            fadeHolder.alpha -= (Time.deltaTime / m_fadeTime);


            yield return null;
        }

        fadeHolder.alpha = 0.0f;
        fadeHolder.blocksRaycasts = false;
    }

    private IEnumerator BlackFadeOut(Color fadeColor)
    {
        m_fadeColor.color = fadeColor;
        fadeHolder.blocksRaycasts = true;


        StopCoroutine("BlackFadeIn");



        fadeHolder.alpha = 0.0f;

        float currentTime = Time.time;

        while (currentTime + m_fadeTime > Time.time)
        {

            fadeHolder.alpha += (Time.deltaTime / m_fadeTime);
            yield return null;
        }

        fadeHolder.alpha = 1.0f;
        fadeHolder.blocksRaycasts = false;
    }

    private IEnumerator BlackFadeOutIn(Color fadeColor)
    {
        m_fadeColor.color = fadeColor;
        fadeHolder.blocksRaycasts = true;

        StopCoroutine("BlackFadeIn");

        fadeHolder.alpha = 0.0f;

        float currentTime = Time.time;

        while (currentTime + m_fadeTime > Time.time)
        {

            fadeHolder.alpha += (Time.deltaTime / m_fadeTime);
            yield return null;
        }

        fadeHolder.alpha = 1.0f;
        fadeHolder.blocksRaycasts = false;

        StartCoroutine(BlackFadeIn(fadeColor));
    }
}
