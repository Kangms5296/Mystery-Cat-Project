using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitScript : MonoBehaviour {

    public ReactionCollection exitReaction;

    public GameObject reactionButton;

    // 되돌아갈 canvas
    public GameObject mainDisplayer;

    // 현재 canvas
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
        mainDisplayer.gameObject.SetActive(true);
    }

    // 종료 버튼 클릭 - UI 외부 영역 클릭
    public void OnClickClose()
    {
        reactionButton.SetActive(false);
    }
}
