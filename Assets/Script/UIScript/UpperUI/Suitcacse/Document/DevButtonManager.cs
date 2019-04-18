using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevButtonManager : MonoBehaviour {

    // 화성화, 비활성화 이미지
    public Sprite activatedSprite;
    public Sprite deactivatedSprite;

    // 현재 UI 활성화 상태
    public enum CLICK_STATE { INFORMATION, CHARACTER };
    private CLICK_STATE conState = CLICK_STATE.INFORMATION;

    // 정보, 인물 이미지
    private Image information;
    private Image character;


	// Use this for initialization
	void Start () {
        information = transform.Find("InformationButton").GetComponent<Image>();
        character = transform.Find("CharacterButton").GetComponent<Image>();
	}


    // ========================================================== Public Function ================================================================

    // UI가 최초로 실행되어질 때의 상태 지정
    public void Init()
    {
        // 문서 버튼을 클릭한 모습으로 시작
        OnClickInformationButton();
    }

    // 창 좌측의 문서 버튼 클릭
    public void OnClickInformationButton()
    {
        if(character == null)
        {
            information = transform.Find("InformationButton").GetComponent<Image>();
            character = transform.Find("CharacterButton").GetComponent<Image>();
        }

        conState = CLICK_STATE.INFORMATION;

        information.sprite = activatedSprite;
        character.sprite = deactivatedSprite;
    }

    // 창 좌측의 인물 버튼 클릭
    public void OnClickCharacterButton()
    {
        conState = CLICK_STATE.CHARACTER;

        information.sprite = deactivatedSprite;
        character.sprite = activatedSprite;
    }

    




    // ========================================================= Get Set Function ==================================================================

    public CLICK_STATE GetState()
    {
        return conState;
    }

}
