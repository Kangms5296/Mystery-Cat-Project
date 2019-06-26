using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentManager : UIScript {

    public GameObject documentDisplayer;

    public DocumentInformationManager information;
    public DocumentCharacterManager character;
    public DevButtonManager devButton;

    public void Start()
    {
        character.Init();
        information.Init();
    }


    // UI를 종료
    public void OnClickExit()
    {
        information.isOpened = false;
        character.isOpened = false;

        documentDisplayer.SetActive(false);
    }

    // 화면 상단 UI를 통하여 UI를 실행
    public override void OnClickUI()
    {
        documentDisplayer.SetActive(true);

        OnClickInformationButton();

        // information UI 기본 모습으로 초기화
        information.OnClickInformation();
        information.gameObject.SetActive(true);
        information.isOpened = true;

        // character UI 기본 모습으로 초기화
        character.OnClickCharacter();
        character.gameObject.SetActive(false);
        character.isOpened = false;

        // 초기 화면 표시 UI 지정
        devButton.Init();
    }

    // 창 좌측의 문서 버튼 클릭
    public void OnClickInformationButton()
    {
        // information UI 기본 모습으로 초기화
        information.Init();

        information.gameObject.SetActive(true);
        information.isOpened = true;

        character.gameObject.SetActive(false);
        character.isOpened = false;
    }

    // 창 좌측의 인물 버튼 클릭
    public void OnClickCharacterButton()
    {
        // character 스크립트 기본 모습으로 초기화
        character.Init();

        information.gameObject.SetActive(false);
        information.isOpened = false;

        character.gameObject.SetActive(true);
        character.isOpened = true;
    }

    // 리액션으로부터 새로 해금할 캐릭터의 이름을 DocumentCharacterManager 스크립트로 전송
    public void CharacterKnow(string tempName)
    {
        character.CharacterKnow(tempName);

    }

    // 리액션으로부터 slot에 들어갈 새로운 정보의 이름을 받아 이를 DocumentInformationManager 스크립트로 전송
    public void SetNewSlot(string name)
    {
        information.SetNewSlot(name);
    }
}
