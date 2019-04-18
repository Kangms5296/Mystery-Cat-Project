using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocumentInformationManager : MonoBehaviour {

    // normal 상태의 배경 Sprite
    public Sprite deactivatedSlotBg;
    // click 상태의 배경 Sprite
    public Sprite activatedSlotBg;

    // Character slot
    public InformationSlot[] slots;

    // 현재 사용중인 slot 수
    private int conSlot = 0;

    // 현재 클릭되어있는 슬롯
    private GameObject conClickSlot = null;

    // 좌측 내용 표시 UI 캐싱
    private Text expText = null;

    // information 정보를 추출할 csv 파일
    [HideInInspector] public ParsingData allData;


    // ============================================================== Public Function =============================================================

    public void Init()
    {
        // 각 slot들 초기화 및 컴포넌트 연결
        slots = transform.GetComponentsInChildren<InformationSlot>();
        foreach (InformationSlot slot in slots)
            slot.Init(gameObject.GetComponent<DocumentInformationManager>());

        expText = transform.Find("ExpSheet").Find("ExpText").GetComponent<Text>();

        // information 정보를 추출할 csv 파일t 캐싱
        allData = Resources.Load("CSVData/All Item List Asset") as ParsingData;
    }

    // 좌측의 문서 버튼 클릭 or 최초 UI 표시
    public void OnClickInformation()
    {
        conClickSlot = null;

        // 좌측 클릭 slot 정보 초기화
        expText.text = "";

        // 모든 슬롯이 비활성화 배경 상태를 가지도록 한다.
        foreach (InformationSlot slot in slots)
        {
            slot.GetComponent<Image>().sprite = deactivatedSlotBg;
        }
    }

    // 유저가 새로운 slot을 클릭하면, 해당 slot의 정보가 전달.
    public void ClickNewSlot(GameObject newSlot)
    {
        // 기존의 클릭한 slot의 배경 이미지를 normal image로 변경
        if (conClickSlot != null)
            conClickSlot.GetComponent<Image>().sprite = deactivatedSlotBg;

        // 클릭한 slot의 배경 이미지를 변경
        newSlot.GetComponent<Image>().sprite = activatedSlotBg;

        // 새로 클릭한 slot을 기존 클릭한 slot으로 지정
        conClickSlot = newSlot;

        // 클릭한 slot 내 정보를 우측 칸에 표시
        expText.text = newSlot.GetComponent<InformationSlot>().informationExp;
    }

    // 모든 slot들의 값을 데이터베이스에 저장하도록 지시
    public void SaveSlotsState()
    {
        GetComponent<InformationData>().SaveXmlData();
    }

    // 데이터베이스에 저장되어 있는 모든 slot들의 값을 반환
    public void LoadSlotsState()
    {
        GetComponent<InformationData>().LoadXmlData();
    }

    // slot에 새로운 정보를 등록한다. 이는 리액션에서 호출되어진다.
    public void SetNewSlot(string name)
    {
        // 최대 슬롯 수를 넘었는지 확인
        if (conSlot < slots.Length)
        {
            // 기존의 슬롯 중 같은 내용의 슬롯이 있는지 확인
            foreach(InformationSlot slot in slots)
            {
                // 같은 이름의 슬롯이 있으므로 종료
                if (slot.informationName == name)
                    return;
            }

            // 새로운 슬롯에 추가
            slots[conSlot].SetNewSlot(name, GetExpByName(name));
            conSlot++;
        }
    }


    // ============================================================== Private Function =============================================================

    private string GetExpByName(string tempName)
    {
        return allData.GetExplanationByName(tempName);
    }
}
