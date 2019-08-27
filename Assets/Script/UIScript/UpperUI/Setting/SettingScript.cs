using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingScript : UIScript {

    public GameObject reactionButton;

    // 메인 canvas
    public GameObject mainDisplayer;
    
    // 사운드 canvas
    public GameObject soundDisplayer;

    // 저장 canvas
    public GameObject saveDisplayer;

    // 종료 canvas
    public GameObject exitDisplayer;
    

    public override void OnClickUI()
    {
        // 환경설정 창을 킨다.
        mainDisplayer.SetActive(true);

        // 화면에 표시
        reactionButton.SetActive(true);
    }
    
    public void OnClickClose()
    {
        reactionButton.SetActive(false);
    }

    public void OnClickSound()
    {
        mainDisplayer.SetActive(false);

        soundDisplayer.SetActive(true);
    }

    public void OnClickSave()
    {
        mainDisplayer.SetActive(false);

        saveDisplayer.SetActive(true);
    }

    public void OnClickExit()
    {
        mainDisplayer.SetActive(false);

        exitDisplayer.SetActive(true);
    }
}
