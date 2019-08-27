﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WatingUntilClickSaveReaction : DelayedReaction
{
    private GameObject myCorotine;

    public Button button;
    private bool isSaveClick = false;

    public ReactionCollection afterReaction;
    public ReactionCollection notReaction;

    protected override void ImmediateReaction()
    {
        FSLocator.textDisplayer.reactionButton.enabled = false;

        myCorotine = CoroutineHandler.Start_Coroutine(Wating()).gameObject;
    }

    IEnumerator Wating()
    {
        while (myCorotine == null)
            yield return null;

        // 문서 정보를 클릭하였는지 확인하는 리스너 추가
        button.onClick.AddListener(ClickSave);


        GameObject settingCanvas = FindObjectOfType<SettingScript>().transform.Find("ReactionButton").gameObject;
        
        // 환경설정 Displayer을 열 때 까지 대기
        while (settingCanvas.activeSelf == false)
            yield return null;

        // 환경설정 Displayer을 끌 때 까지 대기
        while (settingCanvas.activeSelf == true)
            yield return null;

        // 저장 버튼을 눌렀는가?
        if (isSaveClick)
            afterReaction.InitAndReact();
        else
            notReaction.InitAndReact();

        FSLocator.textDisplayer.reactionButton.enabled = true;

        Destroy(myCorotine);
    }


    void ClickSave()
    {
        isSaveClick = true;

        button.onClick.RemoveListener(ClickSave);
    }
}