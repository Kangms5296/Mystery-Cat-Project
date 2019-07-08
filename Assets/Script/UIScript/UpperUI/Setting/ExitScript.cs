using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitScript : MonoBehaviour {

    public ReactionCollection exitReaction;

    // 환경설정 canvas
    public GameObject settingDisplayer;

    // 종료 canvas
    public GameObject exitDisplayer;


    public void OnClickYes()
    {
        // 종료 창을 끈다.
        exitDisplayer.gameObject.SetActive(false);

        exitReaction.InitAndReact();
    }

    public void OnClickNo()
    {
        // 종료 창을 끈다.
        exitDisplayer.gameObject.SetActive(false);

        // 환경설정 창을 킨다.
        settingDisplayer.gameObject.SetActive(true);
    }

    // 종료 버튼 클릭 - UI 외부 영역 클릭
    public void OnClickClose()
    {
        // 종료 창을 끈다.
        exitDisplayer.gameObject.SetActive(false);
    }
}
