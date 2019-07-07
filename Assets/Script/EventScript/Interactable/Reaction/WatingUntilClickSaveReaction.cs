using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WatingUntilClickSaveReaction : DelayedReaction
{
    private GameObject myCorotine;

    public ReactionCollection afterReaction;
    
    protected override void ImmediateReaction()
    {
        FSLocator.textDisplayer.reactionButton.enabled = false;

        myCorotine = CoroutineHandler.Start_Coroutine(Wating()).gameObject;
    }

    IEnumerator Wating()
    {
        GameObject settingCanvas = FindObjectOfType<SettingScript>().transform.Find("ReactionButton").gameObject;
        GameObject saveCanvas = FindObjectOfType<SaveManager>().transform.Find("ReactionButton").gameObject;

        // 환경설정 Displayer을 열 때 까지 대기
        while (settingCanvas.activeSelf == false)
            yield return null;

        // 저장 Displayer을 열 때 까지 대기
        while (saveCanvas.activeSelf == false)
            yield return null;

        // 저장 Displayer을 끌 때 까지 대기
        while (saveCanvas.activeSelf == true)
            yield return null;

        // 환경설정 Displayer을 끌 때 까지 대기
        while (settingCanvas.activeSelf == true)
            yield return null;

        FSLocator.textDisplayer.reactionButton.enabled = true;

        afterReaction.InitAndReact();

        Destroy(myCorotine);
    }
}