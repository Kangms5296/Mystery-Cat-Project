using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemScript : UIScript {

    // 아이템 canvas
    public GameObject itemDisplayer;

    // 현재 표시되고 있는 bg 상태 체크 변수 - Drag and Drop 가능한 bg 확인 등의 작업을 위해 열거형 변수로 확인.
    enum BGSTATE { MIX1, MIX2, GIVE};
    BGSTATE bgState;
    // 조합 버튼이 들어있는 bg
    public GameObject mixBg1;
    // 조합 버튼 클릭 이후 나오는 bg
    public GameObject mixBg2;
    // 주다 버튼이 들어있는 bg
    public GameObject giveBg;

    // Name Text
    public Text itemName;
    // Explanation Text
    public Text itemExp;
    // Item Image
    public Image itemImage;
    // Item Index (Only Use By Give)
    private int clickSlotindex;

    // Drag Item Image RectTransform 콤포넌트 캐싱
    public RectTransform dragImage;

    // "주다" UI 전용 임시변수들
    private string npcName_GiveUIOnlyUse = "";
    // 현재 "주다" 로 인한 UI 표시중인가?
    [HideInInspector] public bool isGive = false;
    // 주기 리액션 관련 조건 변수;
    [HideInInspector] public bool isSimpleGive = true;


    // 메인화면 상단 UI의 Icon 클릭으로 인벤토리 캔버스 표시
    public override void OnClickUI()
    {
        // 인벤토리 UI에 조합 버튼이 들어있는 bg가 표시된다.
        mixBg1.SetActive(true);
        // 인벤토리 UI에 조합 목록 bg가 가려진다.
        mixBg2.SetActive(false);
        // 인벤토리 UI에 주다 버튼이 들어있는 bg가 가려진다.
        giveBg.SetActive(false);

        bgState = BGSTATE.MIX1;

        // 조합 불가능한 아이템을 표시하고 있다면, 이 표시를 제거
        GetComponent<ContentScript>().IsMixOKReverse();

        InitUI();
    }

    // 첫 UI에서 조합 버튼 클릭으로 조합을 할 수 있는 UI로 변환
    public void OnClickMix1()
    {
        // 인벤토리 UI에 조합 버튼이 들어있는 bg가 가려진다.
        mixBg1.SetActive(false);
        // 인벤토리 UI에 조합 목록 bg가 표시진다.
        mixBg2.SetActive(true);
        // 인벤토리 UI에 주다 버튼이 들어있는 bg가 가려진다.
        giveBg.SetActive(false);

        bgState = BGSTATE.MIX2;

        // 자~ 가지고 있는 이이템 목록을 순회하며 조합 가능한 아이템만 표시
        GetComponent<ContentScript>().IsMixOK();

        // 조합 목록에 이전에 사용한 재료가 있다면 삭제
        GetComponent<ContentScript>().DeleteAllMaterial();
    }

    // NPC 대화 상 '주다' 버튼으로 인벤토리 캔버스 표시
    public void OnClickGive(string npcName)
    {
        npcName_GiveUIOnlyUse = npcName;

        // 인벤토리 UI에 조합 버튼이 들어있는 bg가 가려진다.
        mixBg1.SetActive(false);
        // 인벤토리 UI에 조합 목록 bg가 가려진다.
        mixBg2.SetActive(false);
        // 인벤토리 UI에 주다 버튼이 들어있는 bg가 표시된다.
        giveBg.SetActive(true);

        bgState = BGSTATE.GIVE;

        // 조합 불가능한 아이템을 표시하고 있다면, 이 표시를 제거
        GetComponent<ContentScript>().IsMixOKReverse();

        InitUI();
    }

    // 취소 버튼 클릭
    public void OnClickBack()
    {
        // 최초의 상태에서 조합 버튼을 클릭한 이후의 상황에서 취소를 클릭한 경우에는
        if(bgState == BGSTATE.MIX2)
        {
            // 최초의 상태로 되돌아감
            OnClickUI();
        }
        // 그 외에는
        else
        {
            // 클릭했던 정보만 지운다.
            InitUI();
        }
    }

    // 인벤토리 캔버스 종료
    public void OnClickClose()
    {
        // 인벤토리 UI에 조합 버튼이 들어있는 bg가 가려진다.
        mixBg1.SetActive(false);
        // 인벤토리 UI에 조합 목록 bg가 가려진다.
        mixBg2.SetActive(false);
        // 인벤토리 UI에 주다 버튼이 들어있는 bg가 표시된다.
        giveBg.SetActive(false);
        
        InitUI();

        itemDisplayer.gameObject.SetActive(false);
    }

    // slot 클릭 시 좌측 칸에 클릭 아이템 정보 표시
    public void EditClickItemInfo(string name, string exp, Image image, int index)
    {
        itemName.text = name;
        itemExp.text = exp;
        itemImage.sprite = image.sprite;
        itemImage.enabled = true;
        clickSlotindex = index;
    }

    // 사용자가 "주다" 버튼 클릭 시 호출
    public void Give()
    {
        if (isSimpleGive)
        {
            // 현재 플레이어가 누른 아이템을 확인
            Debug.Log("플레이어가 클릭한 아이템은 " + itemName.text);

            // 현재 대화하고있는 NPC를 확인
            Debug.Log("대화하고 있는 NPC는" + npcName_GiveUIOnlyUse);

            // 해당 NPC가 누른 아이템에 대해 필요로 하는지 확인
            bool isNeed = false;
            int temp = -1;
            ParsingData needList = Resources.Load("CSVData/All Give List Asset") as ParsingData;
            List<Dictionary<string, object>> tempList = needList.list;
            for (int i = 0; i < tempList.Count; i++)
            {
                if (tempList[i]["NPC"] as string == npcName_GiveUIOnlyUse &&
                    tempList[i]["ITEM"] as string == itemName.text)
                {
                    Debug.Log(npcName_GiveUIOnlyUse + " 에게 " + itemName.text + " 는 필요하다.");
                    isNeed = true;
                    temp = i;
                }
            }

            // 필요로하면 아이템을 준다.
            if (isNeed)
            {
                // 인벤토리 내 해당 아이템 삭제
                GetComponent<ContentScript>().DeleteItemSlot(clickSlotindex);
                // 인벤토리 정렬
                GetComponent<ContentScript>().SortItemSlot();

                // 아이템 제공 Condition을 체크
                AllConditions conditions = Resources.Load<AllConditions>("AllConditions");
                foreach (Condition condition in conditions.conditions)
                {
                    if (condition.description == tempList[temp]["CONDITION"] as string)
                    {
                        condition.satisfied = true;
                        isGive = true;
                        break;
                    }
                }
            }
            // 필요로하지 않으면 아이템을 주지 않는다.
            else
            {
                // 그에대한 반응
                isGive = false;
            }


            // UI 종료
            OnClickClose();
        }
        else
        {
            // 인벤토리 UI에 조합 버튼이 들어있는 bg가 가려진다.
            mixBg1.SetActive(false);
            // 인벤토리 UI에 조합 목록 bg가 가려진다.
            mixBg2.SetActive(false);
            // 인벤토리 UI에 주다 버튼이 들어있는 bg가 표시된다.
            giveBg.SetActive(false);
            
            itemDisplayer.gameObject.SetActive(false);
        }
    }

    public void Give_OnCondition()
    {
        // 인벤토리 내 해당 아이템 삭제
        GetComponent<ContentScript>().DeleteItemSlot(clickSlotindex);
        // 인벤토리 정렬
        GetComponent<ContentScript>().SortItemSlot();
        // 아이템을 주었다고 표시
        isGive = true;
    }

    // ================================================================== 비공개 메서드 정의 ======================================================

    private void InitUI()
    {
        // 기존의 좌측 칸에 클릭 아이템 정보를 지운다.
        itemName.text = "";
        itemExp.text = "";
        itemImage.enabled = false;

        // UI 표시
        itemDisplayer.SetActive(true);
    }
}
