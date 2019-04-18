using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CutSceneTextDisplayer : MonoBehaviour
{
    //대화를 다음 대화로 업데이트하는데 걸리는 시간
    [Range(0.0f, 1.0f)]
    private float timeBetUpdateLetters = 0.03f;

    private float dissolveTime = 0.0f;
    private float textMaintainTime = 0.0f;
    public Text cutSceneText;
    private Color cutSceneTextColor;

    private void Awake()
    {
        cutSceneTextColor = cutSceneText.color;
    }

    public void ChangeCutSceneTextFontStyle(Font _font, int _fontSize)
    {
        cutSceneText.font = _font;
        cutSceneText.fontSize = _fontSize;
    }

    public void ShowText()
    {

        //ImediateStopTransition();
        StartCoroutine("Show");
    }

    public void MaintainText()
    {
        StartCoroutine("Maintain");
    }

    public void HideText()
    {

        //ImediateStopTransition();
        StartCoroutine("Hide");
    }


    public void DrawText(string text, float dissolve, float maintain)
    {
        dissolveTime = dissolve;
        textMaintainTime = maintain;
        cutSceneText.text = text;
        ImmediateStopTransition();
        if (text != "")
        {
            ShowText();
        }
        else
        {
            cutSceneTextColor.a = 0.0f;
            cutSceneText.color = cutSceneTextColor;
        }

    }



    private void ImmediateStopTransition()
    {
        StopCoroutine("Show");
        StopCoroutine("Maintain");
        StopCoroutine("Hide");
    }

    private IEnumerator Show()
    {
        cutSceneTextColor.a = 0.0f;
        cutSceneText.color = cutSceneTextColor;

        float startTime = Time.time;

        while (Time.time <= startTime + dissolveTime)
        {
            cutSceneTextColor.a += (Time.deltaTime / dissolveTime);
            cutSceneText.color = cutSceneTextColor;
            yield return null;
        }

        cutSceneTextColor.a = 1.0f;
        cutSceneText.color = cutSceneTextColor;
        MaintainText();
    }

    private IEnumerator Maintain()
    {
        yield return new WaitForSeconds(textMaintainTime);
        HideText();
    }

    private IEnumerator Hide()
    {
        //characterCanvasGroup.alpha = 1.0f;
        float startTime = Time.time;

        while (Time.time <= startTime + dissolveTime)
        {
            cutSceneTextColor.a -= (Time.deltaTime / dissolveTime);
            cutSceneText.color = cutSceneTextColor;
            yield return null;
        }

        cutSceneTextColor.a = 0.0f;
        cutSceneText.color = cutSceneTextColor;
    }




    //바깥에서 현재 타이핑 중인지 컨펌가능
    public bool isTyping
    {
        get; private set;
    }

    private string onTypingDialogue;

    //현재 타이핑 중인데, 이것을 바로 스킵하고 전체 문자열을 띄우고 싶을떄.
    //얘를 들면 문자가 나타나는 도중 터치하는 경우.
    public void SkipTypingLetter()
    {

        StopCoroutine("TypeText");
        StopCoroutine("TypeAndAddText");

        isTyping = false;

        cutSceneText.text = onTypingDialogue;
    }


    public void Say(string text_, float dissolve, float maintain, float _timeBetUpdateLetters)
    {
        cutSceneTextColor.a = 1.0f;
        cutSceneText.color = cutSceneTextColor;
        dissolveTime = dissolve;
        textMaintainTime = maintain;
        timeBetUpdateLetters = _timeBetUpdateLetters;
        onTypingDialogue = text_;

        if (timeBetUpdateLetters <= 0f)
        {
            cutSceneText.text = text_;
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

        cutSceneTextColor.a = 1.0f;
        cutSceneText.color = cutSceneTextColor;

        isTyping = true;


        cutSceneText.text = string.Empty;
        foreach (char letter in texts.ToCharArray())
        {

            cutSceneText.text += letter;
            yield return new WaitForSeconds(timeBetUpdateLetters);
        }

        isTyping = false;
        MaintainText();
    }

    public IEnumerator TypeAndAddText(string texts)
    {

        cutSceneTextColor.a = 1.0f;
        cutSceneText.color = cutSceneTextColor;



        isTyping = true;

        foreach (char letter in texts.ToCharArray())
        {

            cutSceneText.text += letter;
            yield return new WaitForSeconds(timeBetUpdateLetters);
        }

        isTyping = false;
    }


}
