using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour {

    
    // 현재 슬롯에 보여지는 캐릭터 이름
    public string characterName;
    // 현재 슬롯에 보여지는 캐릭터 설명
    [TextArea]
    public string characterExp;

    // 표시 방법
    public bool isKnown = false;

    // slot 관리 manager
    private DocumentCharacterManager documentCharacterManager;




    // ================================================================= Public Function ===============================================================

    public void Init(DocumentCharacterManager parent)
    {
        documentCharacterManager = parent;
    }

    // Slot Click Function
    public void OnClickSlot()
    {
        documentCharacterManager.ClickNewSlot(gameObject);
    }

    // 이 슬롯에 담겨있는 캐릭터에 대한 정보를 플레이어가 찾을 경우 호출. 이후 이 슬롯은 캐릭터 정보가 노출된다.
    public void CharacterKnow()
    {
        isKnown = true;

        // Known 이미지 표시
        transform.Find("Known").gameObject.SetActive(true);

        // UnKnown 이미지 제거
        transform.Find("Unknown").gameObject.SetActive(false);

        AudioSource effectAudio = GameObject.Find("EffectSound").GetComponent<AudioSource>();
        effectAudio.clip = Resources.Load<AudioClip>("AudioResource/EffectSound/S_Clue3");
        effectAudio.Play();
    }

    // 현재 slot의 값을 데이터베이스에 저장
    public void SaveSlotState()
    {
        // 현재 isKnown 값 저장
        PlayerPrefs.SetString("CharacterSlot_" + characterName, isKnown.ToString());
    }

    // 데이터베이스에 저장되어 있던 isKnown에 값을 캐싱
    public void LoadSlotState()
    {
        // 저장되어 있는 inKnown 값이 있는지 우선 식별
        if(PlayerPrefs.HasKey("CharacterSlot_" + characterName))
        {
            // 기존에 저장되어 있는 값을 가져온다.
            isKnown = System.Convert.ToBoolean(PlayerPrefs.GetString("CharacterSlot_" + characterName));
        }
    }
}
