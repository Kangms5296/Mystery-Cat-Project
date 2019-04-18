using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplayer : MonoBehaviour {


    //대화를 다음 대화로 업데이트하는데 걸리는 시간
    [Range(0.0f, 1.0f)]
    public float timeBetUpdateLetters;


    public Text dialougeText;
    public GameObject dialougeFrameDisplayer;


    //public Text nameText;
    public GameObject nameFrameDisplayer;
	public Text nameText;

	public Button reactionButton;
    public OneChoiceFrame oneChoiceFrame;
	public ChoiceFrame choiceFrame;
	public TripleChoiceFrame tripleChoiceFrame;


    //바깥에서 현재 타이핑 중인지 컨펌가능
    public bool isTyping
    {
        get; private set;
    }

    private string onTypingDialogue;

    public void ShowDialogueHolder()
    {
        
        dialougeFrameDisplayer.SetActive(true);
        if(nameText.text == "")
            nameFrameDisplayer.SetActive(false);
        else
            nameFrameDisplayer.SetActive(true);
            
    }

    public void HideDialogueHolder()
    {
        dialougeFrameDisplayer.SetActive(false);
        nameFrameDisplayer.SetActive(false);
    }

    //현재 타이핑 중인데, 이것을 바로 스킵하고 전체 문자열을 띄우고 싶을떄.
    //얘를 들면 문자가 나타나는 도중 터치하는 경우.
    public void SkipTypingLetter()
    {

        StopCoroutine("TypeText");
        StopCoroutine("TypeAndAddText");

        isTyping = false;

        // 지금까지의 대화창을 초기화
        dialougeText.text = "";

        // 새로 대화를 출력
        bool temp = false;
        for (int i = 0; i < onTypingDialogue.Length; i++)
        {
            // $ : 노랑
            // % : 파랑
            // * : 빨강
            // 의 색으로 다음 글자를 변경
            if (onTypingDialogue[i] == '$')
            {
                temp = true;
                continue;
            }

            if (temp)
            {
                dialougeText.text += "<color=yellow>" + onTypingDialogue[i] + "</color>";
                temp = false;
            }
            else
            {
                dialougeText.text += onTypingDialogue[i];
            }
        }
        //dialougeText.text = onTypingDialogue;
    }




    public void Say(string text_, string name_)
    {
        nameText.text = name_;
        onTypingDialogue = text_;
        //HideDialogueHolder();

        if (timeBetUpdateLetters <= 0f)
        {
            dialougeText.text = text_;
        }
        else
        {
            StartCoroutine("TypeText", onTypingDialogue);
            
        }


    }


    //텍스트가 업데이트 되는 간격을 지정하기 위해서 존재
    public IEnumerator TypeText(string texts)
    {
        bool temp = false;
        // 필요하다면 후에 DisplayableDipalyer 그림을 그릴때 일시 정지 루프를 돌릴 수 있겠음

        ShowDialogueHolder();

        isTyping = true;


        dialougeText.text = string.Empty;

        for(int i = 0; i < texts.Length; i++)
        {
            // $ : 노랑
            // % : 파랑
            // * : 빨강
            // 의 색으로 다음 글자를 변경
            if(texts[i] == '$')
            {
                temp = true;
                continue;
            }

            if (temp)
            {
                dialougeText.text += "<color=yellow>" + texts[i] + "</color>";
                temp = false;
            }
            else
            {
                dialougeText.text += texts[i];
            }

            yield return new WaitForSeconds(timeBetUpdateLetters);
        }

        isTyping = false;
    }

    public IEnumerator TypeAndAddText(string texts)
    {

        ShowDialogueHolder();



        isTyping = true;

        foreach (char letter in texts.ToCharArray())
        {

            dialougeText.text += letter;
            yield return new WaitForSeconds(timeBetUpdateLetters);
        }

        isTyping = false;
    }
}
