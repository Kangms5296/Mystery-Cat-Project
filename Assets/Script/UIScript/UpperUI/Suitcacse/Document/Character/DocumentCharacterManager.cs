using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DocumentCharacterManager : MonoBehaviour {

    // normal 상태의 배경 Sprite
    public Sprite deactivatedSlotBg;
    // click 상태의 배경 Sprite
    public Sprite activatedSlotBg;

    // Character slot
    private CharacterSlot[] slots;

    // 현재 클릭되어있는 슬롯
    private GameObject conClickSlot = null;

    // 우측 내용 표시 UI 캐싱
    private Text nameText;
    private Text expText;


    [HideInInspector] public bool isOpened = false;




    // ================================================================= Public Function ===============================================================

    public void Init()
    {
        // 각 slot들을 캐싱
        slots = transform.GetComponentsInChildren<CharacterSlot>();
        foreach (CharacterSlot slot in slots)
            slot.Init(gameObject.GetComponent<DocumentCharacterManager>());

        nameText = transform.Find("ExpSheet").Find("NameText").GetComponent<Text>();
        expText = transform.Find("ExpSheet").Find("ExpText").transform.Find("Text").GetComponent<Text>();
    }

    public void OnClickCharacter()
    {
        conClickSlot = null;

        // 우측 클릭 slot 정보 초기화
        nameText.text = "";
        expText.text = "";

        foreach (CharacterSlot slot in slots)
        {
            slot.transform.Find("Known").Find("Bg").GetComponent<Image>().sprite = deactivatedSlotBg;
        }
    }

    // 전달된 캐릭터 정보에 해당하는 slot의 UI를 변경
    public void CharacterKnow(string tempName)
    {
        foreach (CharacterSlot slot in slots)
        {
            if(slot.characterName == tempName)
            {
                // 이전에 이미 알고있다면..
                if (slot.isKnown)
                    // 그냥 종료
                    return;

                // 이번에 새로 알게 되었다면..
                slot.CharacterKnow();
                return;
            }
        }
    }

    // 특정 npc를 가리키는 slot을 반환
    public CharacterSlot GetSlot(string tempName)
    {
        foreach (CharacterSlot slot in slots)
        {
            if (slot.characterName == tempName)
                return slot;
        }
        return null;
    }

    // 유저가 새로운 slot을 클릭하면, 해당 slot의 정보가 전달.
    public void ClickNewSlot(GameObject newSlot)
    {
        // 기존의 클릭한 slot의 배경 이미지를 normal image로 변경
        if (conClickSlot != null)
            conClickSlot.transform.Find("Known").Find("Bg").GetComponent<Image>().sprite = deactivatedSlotBg;

        // 클릭한 slot의 배경 이미지를 변경
        newSlot.transform.Find("Known").Find("Bg").GetComponent<Image>().sprite = activatedSlotBg;

        // 새로 클릭한 slot을 기존 클릭한 slot으로 지정
        conClickSlot = newSlot;

        // 클릭한 slot 내 정보를 우측 칸에 표시
        nameText.text = newSlot.GetComponent<CharacterSlot>().characterName;
        expText.text = newSlot.GetComponent<CharacterSlot>().characterExp;
    }
}
