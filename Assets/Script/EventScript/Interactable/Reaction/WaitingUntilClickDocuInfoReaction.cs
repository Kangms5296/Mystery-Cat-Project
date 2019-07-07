using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WaitingUntilClickDocuInfoReaction : DelayedReaction
{
    private GameObject myCorotine;

    public ReactionCollection afterCheckReaction;
    public ReactionCollection CantCheckReaction;


    public Button button;
    private bool isInfoClick = false;

    protected override void ImmediateReaction()
    {
        FSLocator.textDisplayer.reactionButton.enabled = false;

        myCorotine = CoroutineHandler.Start_Coroutine(Wating()).gameObject;
    }

    IEnumerator Wating()
    {
        GameObject docuScript = FindObjectOfType<DocumentManager>().transform.Find("ReactionButton").gameObject;

        // 문서 정보를 클릭하였는지 확인하는 리스너 추가
        button.onClick.AddListener(ClickInfo);

        // 문서창을 열 때 까지 대기
        while (docuScript.activeSelf == false)
            yield return null;

        // 문서창을 끌 때 까지 대기
        while (docuScript.activeSelf == true)
            yield return null;
        
        button.onClick.RemoveListener(ClickInfo);

        // 조합을 성공하였는가?
        if (isInfoClick)
            afterCheckReaction.InitAndReact();
        else
            CantCheckReaction.InitAndReact();

        FSLocator.textDisplayer.reactionButton.enabled = true;

        Destroy(myCorotine);
    }

    void ClickInfo()
    {
        isInfoClick = true;
    }
}