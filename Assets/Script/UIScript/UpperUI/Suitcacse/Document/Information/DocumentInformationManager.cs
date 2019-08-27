using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocumentInformationManager : MonoBehaviour {

    // normal 상태의 slot 배경
    public Sprite deactivatedSlotBg;
    // click 상태의 slot 배경 
    public Sprite activatedSlotBg;

    // normal 상태의 clue 배경
    public Sprite deactivatedClueBg;
    // click 상태의 clue 배경 
    public Sprite activatedClueBg;



    // information slot
    public InformationSlot[] slots;
    // information clue
    public InformationClue[] clues;

    // 현재 클릭되어있는 슬롯과 단서
    private GameObject conClickSlot = null;
    private GameObject conClickClue = null;

    // 중앙 내용 표시 UI 캐싱
    private Image InfoImage;
    private Text expText;
    private RectTransform expRect;

    // 우측 slot List 캐싱
    private RectTransform slotsRect;

    // information 정보를 추출할 csv 파일
    [HideInInspector] public ParsingData allData;

    // ============================================================== Public Function =============================================================

    public void Init()
    {
        // 각 slot들 초기화 및 컴포넌트 연결
        slots = transform.GetComponentsInChildren<InformationSlot>();
        foreach (InformationSlot slot in slots)
            slot.Init(gameObject.GetComponent<DocumentInformationManager>());

        clues = transform.GetComponentsInChildren<InformationClue>();
        foreach (InformationClue clue in clues)
            clue.Init(gameObject.GetComponent<DocumentInformationManager>());

        InfoImage = transform.Find("InfoImage").GetComponent<Image>();
        expText = transform.Find("ExpSheet").Find("Viewport").Find("ExpText").GetComponent<Text>();
        expRect = expText.GetComponent<RectTransform>();

        slotsRect = transform.Find("ScrollBack").Find("Viewport").Find("Content").GetComponent<RectTransform>();

        // information 정보를 추출할 csv 파일t 캐싱
        allData = Resources.Load("CSVData/All Item List Asset") as ParsingData;
    }

    // 좌측의 문서 버튼 클릭 or 최초 UI 표시
    public void OnClickInformation()
    {
        // 중앙 클릭 slot 정보 초기화
        InfoImage.sprite = null;
        InfoImage.enabled = false;
        expText.text = "";
        expRect.anchoredPosition = new Vector2(0, -75f);

        // 우측 slot List 위치 초기화
        slotsRect.anchoredPosition = new Vector2(200, -1765.2f);

        // 이전에 클릭한 slot의 배경 이미지를 DeActivated로 변경 및 새로운 slot의 배경을 Activated로 변경.
        if (conClickSlot != null)
            conClickSlot.GetComponent<Image>().sprite = deactivatedSlotBg;
        conClickSlot = null;

        // 이전에 클릭한 clue의 배경 이미지를 DeActivated로 변경 및 현재 클릭한 clue의 정보를 초기화
        if (conClickClue != null)
            conClickClue.GetComponent<Image>().sprite = deactivatedClueBg;
        conClickClue = null;
    }

    // 유저가 새로운 slot을 클릭하면, 해당 slot의 정보가 전달.
    public void ClickNewSlot(GameObject newSlot)
    {
        // 이전에 클릭한 slot의 배경 이미지를 DeActivated로 변경 및 새로운 slot의 배경을 Activated로 변경.
        if (conClickSlot != null)
            conClickSlot.GetComponent<Image>().sprite = deactivatedSlotBg;
        newSlot.GetComponent<Image>().sprite = activatedSlotBg;
        conClickSlot = newSlot;

        // 이전에 클릭한 clue의 배경 이미지를 DeActivated로 변경 및 현재 클릭한 clue의 정보를 초기화
        if (conClickClue != null)
            conClickClue.GetComponent<Image>().sprite = deactivatedClueBg;
        conClickClue = null;

        // 클릭한 slot 내 정보를 중앙 칸에 표시
        InformationSlot clickSlot = newSlot.GetComponent<InformationSlot>();
        if (clickSlot.transform.Find("Text").GetComponent<Text>().text == "? ? ?")
        {
            InfoImage.sprite = null;
            InfoImage.enabled = false;
            expText.text = "";
        }
        else
        {
            InfoImage.sprite = clickSlot.informationImage;
            InfoImage.enabled = true;
            expText.text = clickSlot.informationExp;

            conClickSlot.transform.Find("New").GetComponent<Image>().enabled = false;
        }
        expRect.anchoredPosition = new Vector2(0, -75f);
    }

    public void ClickNewClue(GameObject newClue)
    {
        // 이전에 클릭한 slot의 배경 이미지를 DeActivated로 변경 및 새로운 slot의 배경을 Activated로 변경.
        if (conClickSlot != null)
            conClickSlot.GetComponent<Image>().sprite = deactivatedSlotBg;
        conClickSlot = null;

        // 이전에 클릭한 clue의 배경 이미지를 DeActivated로 변경 및 현재 클릭한 clue의 정보를 초기화
        if (conClickClue != null)
            conClickClue.GetComponent<Image>().sprite = deactivatedClueBg;
        newClue.GetComponent<Image>().sprite = activatedClueBg;
        conClickClue = newClue;

        // 클릭한 slot 내 정보를 중앙 칸에 표시
        InformationClue clickClue = newClue.GetComponent<InformationClue>();
        InfoImage.sprite = clickClue.informationImage;
        InfoImage.enabled = true;
        expText.text = clickClue.informationExp;
        expRect.anchoredPosition = new Vector2(0, -75f);
    }

    // slot에 새로운 정보를 등록한다. 이는 리액션에서 호출되어진다.
    public void KnowNewInfo(string infoName)
    {
        // 기존의 슬롯 중 같은 내용의 슬롯이 있는지 확인
        foreach (InformationSlot slot in slots)
        {
            // 같은 이름의 슬롯이 있으면..
            if (slot.informationName == infoName)
            {
                // 해당 슬롯을 해금
                slot.transform.Find("Text").GetComponent<Text>().text = infoName;
                slot.transform.Find("New").GetComponent<Image>().enabled = true;
                break;
            }
        }

        // 기존의 슬롯 중 같은 내용의 단서가 있는지 확인
        foreach (InformationClue clue in clues)
        {
            // 같은 이름의 단서가 있으면..
            if (clue.informationName == infoName)
            {
                clue.GetComponent<Image>().raycastTarget = true;
                clue.transform.Find("UnKnown").gameObject.SetActive(false);
                break;
            }
        }
    }

    public void KnowNewInfoByIndex(int index)
    {
        // 해당 인덱스의 문서정보는 바로 해금
        slots[index].transform.Find("Text").GetComponent<Text>().text = slots[index].informationName;

        // 해금된 문서정보와 같은 정보를 가지는 단서가 있으면 같이 해금
        string infoName = slots[index].informationName;
        foreach (InformationClue clue in clues)
        {
            // 같은 이름의 단서가 있으면..
            if (clue.informationName == infoName)
            {
                clue.GetComponent<Image>().raycastTarget = true;
                clue.transform.Find("UnKnown").gameObject.SetActive(false);
                break;
            }
        }
    }

    public bool IsSlotKnown(int index)
    {
        if (slots[index].transform.Find("Text").GetComponent<Text>().text == "? ? ?")
            return false;
        return true;
    }
    

    // ============================================================== Private Function =============================================================

    private string GetExpByName(string tempName)
    {
        return allData.GetExplanationByName(tempName);
    }
}
