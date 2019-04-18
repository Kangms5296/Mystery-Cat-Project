using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BackgroundDisplayer : MonoBehaviour
{
    private float dissolveTime = 1.0f;
    public Image backgroundImage;
    public CanvasGroup backgroundCanvasGroup;

    [Range(0.0f, 1.0f)]
    private float timeBetUpdateLetters = 0.03f;
    private float textDissolveTime = 0.0f;
    private float textMaintainTime = 0.0f;
    public Text backgroundText;
    private Color backgroundTextColor;

    public bool isTyping
    {
        get; private set;
    }

    public void ChangeBackgroundTextFontStyle(Font _font, int _fontSize)
    {
        backgroundText.font = _font;
        backgroundText.fontSize = _fontSize;
    }

    private void Awake()
    {
        backgroundTextColor = backgroundText.color;
    }

    private void ImediateStopTransition()
    {
        StopCoroutine("Show");
        StopCoroutine("Hide");
    }

    public void DrawBackground(Sprite newBackground)
    {
        backgroundImage.sprite = newBackground;
        ImediateStopTransition();
        StartCoroutine(Show(newBackground));
    }

    public void HideBackground()
    {
        //ImediateStopTransition();
        StartCoroutine("Hide");
    }

    private IEnumerator Show(Sprite newBackground)
    {
        backgroundCanvasGroup.alpha = 0.0f;


        float startTime = Time.time;

        while (Time.time <= startTime + dissolveTime)
        {
            backgroundCanvasGroup.alpha += (Time.deltaTime / dissolveTime);
            yield return null;
        }

        backgroundCanvasGroup.alpha = 1.0f;
    }

    private IEnumerator Hide()
    {
        //characterCanvasGroup.alpha = 1.0f;
        float startTime = Time.time;

        while (Time.time <= startTime + dissolveTime)
        {
            backgroundCanvasGroup.alpha -= (Time.deltaTime / dissolveTime);
            yield return null;
        }

        backgroundCanvasGroup.alpha = 0.0f;
    }



    public void ShowBackgroundText()
    {

        //ImediateStopTransition();
        StartCoroutine("ShowText");
    }

    public void MaintainBackgroundText()
    {
        StartCoroutine("MaintainText");
    }

    public void HideBackgroundText()
    {

        //ImediateStopTransition();
        StartCoroutine("HideText");
    }


    public void DrawBackgroundText(string text, float dissolve, float maintain)
    {
        textDissolveTime = dissolve;
        textMaintainTime = maintain;
        backgroundText.text = text;
        ImmediateStopTransition();
        if (text != "")
        {
            ShowBackgroundText();
        }
        else
        {
            backgroundTextColor.a = 0.0f;
            backgroundText.color = backgroundTextColor;
        }

    }



    private void ImmediateStopTransition()
    {
        StopCoroutine("ShowText");
        StopCoroutine("MaintainText");
        StopCoroutine("HideText");
    }

    private IEnumerator ShowText()
    {
        backgroundTextColor.a = 0.0f;
        backgroundText.color = backgroundTextColor;

        float startTime = Time.time;

        while (Time.time <= startTime + textDissolveTime)
        {
            backgroundTextColor.a += (Time.deltaTime / textDissolveTime);
            backgroundText.color = backgroundTextColor;
            yield return null;
        }

        backgroundTextColor.a = 1.0f;
        backgroundText.color = backgroundTextColor;
        MaintainBackgroundText();
    }

    private IEnumerator MaintainText()
    {
        yield return new WaitForSeconds(textMaintainTime);
        HideBackgroundText();
    }

    private IEnumerator HideText()
    {
        //characterCanvasGroup.alpha = 1.0f;
        float startTime = Time.time;

        while (Time.time <= startTime + textDissolveTime)
        {
            backgroundTextColor.a -= (Time.deltaTime / textDissolveTime);
            backgroundText.color = backgroundTextColor;
            yield return null;
        }

        backgroundTextColor.a = 0.0f;
        backgroundText.color = backgroundTextColor;
    }

    private string onTypingDialogue;
    public void Say(string text_, float dissolve, float maintain, float _timeBetUpdateLetters)
    {
        backgroundTextColor.a = 1.0f;
        backgroundText.color = backgroundTextColor;
        dissolveTime = dissolve;
        textMaintainTime = maintain;
        timeBetUpdateLetters = _timeBetUpdateLetters;
        onTypingDialogue = text_;

        if (timeBetUpdateLetters <= 0f)
        {
            backgroundText.text = text_;
        }
        else
        {
            StartCoroutine("TypeText", onTypingDialogue);
        }
    }

    //텍스트가 업데이트 되는 간격을 지정하기 위해서 존재
    public IEnumerator TypeText(string texts)
    {
        // 필요하다면 후에 DisplayableDipalyer 그림을 그릴때 일시 정지 루프를 돌릴 수 있겠음

        backgroundTextColor.a = 1.0f;
        backgroundText.color = backgroundTextColor;

        isTyping = true;


        backgroundText.text = string.Empty;
        foreach (char letter in texts.ToCharArray())
        {

            backgroundText.text += letter;
            yield return new WaitForSeconds(timeBetUpdateLetters);
        }

        isTyping = false;
        MaintainText();
    }


}
