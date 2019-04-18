using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitScript : MonoBehaviour {

    // 환경설정 canvas
    public GameObject settingDisplayer;

    // 종료 canvas
    public GameObject exitDisplayer;


    // 동~그라미
    public List<Sprite> clickSprite;




    // 냐옹
    public void OnClickYes()
    {
        // app 종료
        Application.Quit();
    }

    // 싫다옹
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
