using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WaitingUntilClickDocuInfoReaction : DelayedReaction
{
    private GameObject myCorotine;

    private bool isInfoClick = false;

    public Button button;

    protected override void ImmediateReaction()
    {
        FSLocator.textDisplayer.reactionButton.enabled = false;

        myCorotine = CoroutineHandler.Start_Coroutine(Wating()).gameObject;
    }

    IEnumerator Wating()
    {
        // 문서 정보를 클릭하였는지 확인
        button.onClick.AddListener(ClickInfo);

        DocumentManager docuScript = FindObjectOfType<DocumentManager>();

        // 문서창을 열 때 까지 대기
        while (true)
        {
            yield return null;
            if (docuScript.information.isOpened == true)
                break;
        }

        // 문서 정보 클릭 까지 대기
        while (true)
        {
            yield return null;
            if (isInfoClick == true)
                break;
        }

        // 문서창을 닫을 때 까지 대기
        while (true)
        {
            yield return null;
            if (docuScript.information.isOpened == false)
                break;
        }


        button.onClick.RemoveListener(ClickInfo);

        FSLocator.textDisplayer.reactionButton.enabled = true;
        FSLocator.textDisplayer.reactionButton.onClick.Invoke();
        Destroy(myCorotine);
    }

    void ClickInfo()
    {
        Debug.Log(1111);
        isInfoClick = true;
    }
}