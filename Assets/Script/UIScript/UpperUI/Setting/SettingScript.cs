using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingScript : UIScript {

    // 환경설정 canvas
    public GameObject settingDisplayer;

    // 사운드 canvas
    public GameObject soundDisplayer;

    // 종료 canvas
    public GameObject exitDisplayer;


    public override void OnClickUI()
    {
        // 환경설정 창을 킨다.
        settingDisplayer.gameObject.SetActive(true);
    }

    public void OnClickClose()
    {
        // 환경설정 창을 끈다.
        settingDisplayer.gameObject.SetActive(false);
    }

    public void OnClickSound()
    {
        // 환경설정 창을 끈다.
        settingDisplayer.gameObject.SetActive(false);

        // 사운드 창을 킨다.
        soundDisplayer.gameObject.SetActive(true);
    }

    public void OnClickMain()
    {

    }

    public void OnClickExit()
    {
        // 환경설정 창을 끈다.
        settingDisplayer.gameObject.SetActive(false);

        // 종료 창을 킨다.
        exitDisplayer.gameObject.SetActive(true);
    }
}
