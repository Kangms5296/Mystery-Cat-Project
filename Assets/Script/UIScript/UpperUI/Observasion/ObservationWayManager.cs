using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObservationWayManager : MonoBehaviour {

    // 버튼 상태 표시 이미지
    public Sprite activated;
    public Sprite deactivated;

    // 현재 클릭한 버튼의 상태
    public enum ActivatedBtnState {Default, Touch, Look, Pincer};
    public ActivatedBtnState activatedBtnState;

    // 터치 변화를 호출할 총괄 manager
    public ObservationManager observationManager;




    // ================================================= public Function ===========================================================

    // Displayer 표시
    public void Init(bool disabledPull, bool disabledLook, bool disabledPincer)
    {
        // 각 버튼들의 이전 클릭 정보 초기화 처리 및 클릭불가 처리
        Transform pull = transform.Find("Pull");
        Transform look = transform.Find("Look");
        Transform pincer = transform.Find("Pincer");

        if(disabledPull)
        {
            pull.GetComponent<Button>().enabled = false;
            pull.Find("Disabled").gameObject.SetActive(true);
        }
        else
        {
            pull.GetComponent<Button>().enabled = true;
            pull.Find("Disabled").gameObject.SetActive(false);
            pull.GetComponent<Image>().sprite = deactivated;
        }

        if (disabledLook)
        {
            look.GetComponent<Button>().enabled = false;
            look.Find("Disabled").gameObject.SetActive(true);
        }
        else
        {
            look.GetComponent<Button>().enabled = true;
            look.Find("Disabled").gameObject.SetActive(false);
            look.GetComponent<Image>().sprite = deactivated;
        }

        if (disabledPincer)
        {
            pincer.GetComponent<Button>().enabled = false;
            pincer.Find("Disabled").gameObject.SetActive(true);
        }
        else
        {
            pincer.GetComponent<Button>().enabled = true;
            pincer.Find("Disabled").gameObject.SetActive(false);
            pincer.GetComponent<Image>().sprite = deactivated;
        }

        // 최초 상태 지정
        activatedBtnState = ActivatedBtnState.Default;

        // 최초 지정 상태를 총괄 manager에게 전달
        observationManager.OnClickDefaultBtn();
    }


    // Pull 버튼을 클릭
    public void OnClickPullBtn()
    {
        // 현재 상태 확인
        switch(activatedBtnState)
        {
            case ActivatedBtnState.Touch:
                // 기존에 클릭된 버튼을 다시 클릭하였으므로 변화x
                return;
            // 기존에 Look 버튼이 클릭되어 있었으며, 현재 Touch 버튼을 클릭하였으므로..
            case ActivatedBtnState.Look:
                // 1, 기존에 클릭된 Look 버튼의 배경 이미지를 deactivated 로 변경
                transform.Find("Look").GetComponent<Image>().sprite = deactivated;
                break;
            // 기존에 Pincer 버튼이 클릭되어 있었으며, 현재 Touch 버튼을 클릭하였으므로..
            case ActivatedBtnState.Pincer:
                // 1, 기존에 클릭된 Pincer 버튼의 배경 이미지를 deactivated 로 변경
                transform.Find("Pincer").GetComponent<Image>().sprite = deactivated;
                break;
        }
        // Touch 버튼을 activated 이미지로 변경
        transform.Find("Pull").GetComponent<Image>().sprite = activated;

        // 현재 상태 변경
        activatedBtnState = ActivatedBtnState.Touch;

        // 상태 변화에 대한 정보를 총괄 manager에게 전달
        observationManager.OnClickPullBtn();
    }

    // Look 버튼을 클릭
    public void OnClickLookBtn()
    {
        // 현재 상태 확인
        switch (activatedBtnState)
        {
            // 기존에 Touch 버튼이 클릭되어 있었으며, 현재 Look 버튼을 클릭하였으므로..
            case ActivatedBtnState.Touch:
                // 1, 기존에 클릭된 Touch 버튼의 배경 이미지를 deactivated 로 변경
                transform.Find("Pull").GetComponent<Image>().sprite = deactivated;
                break;
            case ActivatedBtnState.Look:
                // 기존에 클릭된 버튼을 다시 클릭하였으므로 변화x
                return;
            // 기존에 Pincer 버튼이 클릭되어 있었으며, 현재 Look 버튼을 클릭하였으므로..
            case ActivatedBtnState.Pincer:
                // 1, 기존에 클릭된 Pincer 버튼의 배경 이미지를 deactivated 로 변경
                transform.Find("Pincer").GetComponent<Image>().sprite = deactivated;
                break;
        }
        // Look 버튼을 activated 이미지로 변경
        transform.Find("Look").GetComponent<Image>().sprite = activated;

        // 현재 상태 변경
        activatedBtnState = ActivatedBtnState.Look;

        // 상태 변화에 대한 정보를 총괄 manager에게 전달
        observationManager.OnClickLookBtn();
    }

    // Pincer 버튼을 클릭
    public void OnClickPincerBtn()
    {
        // 현재 상태 확인
        switch (activatedBtnState)
        {
            // 기존에 Touch 버튼이 클릭되어 있었으며, 현재 Pincer 버튼을 클릭하였으므로..
            case ActivatedBtnState.Touch:
                // 1, 기존에 클릭된 Touch 버튼의 배경 이미지를 deactivated 로 변경
                transform.Find("Pull").GetComponent<Image>().sprite = deactivated;
                break;
            // 기존에 Look 버튼이 클릭되어 있었으며, 현재 Pincer 버튼을 클릭하였으므로..
            case ActivatedBtnState.Look:
                // 1, 기존에 클릭된 Look 버튼의 배경 이미지를 deactivated 로 변경
                transform.Find("Look").GetComponent<Image>().sprite = deactivated;
                break;
            case ActivatedBtnState.Pincer:
                // 기존에 클릭된 버튼을 다시 클릭하였으므로 변화x
                return;
        }
        // Pincer 버튼을 activated 이미지로 변경
        transform.Find("Pincer").GetComponent<Image>().sprite = activated;

        // 현재 상태 변경
        activatedBtnState = ActivatedBtnState.Pincer;

        // 상태 변화에 대한 정보를 총괄 manager에게 전달
        observationManager.OnClickPincerBtn();
    }




    // ================================================= private Function ===========================================================



}
