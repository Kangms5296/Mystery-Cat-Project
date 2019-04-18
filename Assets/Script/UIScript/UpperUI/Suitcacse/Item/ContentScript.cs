using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContentScript : MonoBehaviour {

    // 2018-05-28 전체적으로 2차 수정 시작
    // 조합 기능 추가에 따라, 이 스크립트를 조합 - 아이템 리스트 모든 경우에서 사용할 수 있도록 수정.

    // 아이템 slot을 담고있는 content 객체
    public GameObject itemContent;
    // 아이템 리스트의 전체 slot
    private SlotScript[] itemSlots;
    // 아이템 리스트의 현재 채워져 있는 slot 갯수
    private int conSlot_item = 0;


    // 재료 slot을 담고있는 content 객체
    public GameObject materialContent;
    // 재료 리스트의 전체 slot
    private SlotScript[] materialSlots;
    // 재료 리스트의 현재 채워져 있는 slot 갯수
    private int conSlot_material = 0;


    // 현재 drag로 조합 창으로 물건을 가져왔는지 체크
    private bool isDrag = false;


    // csv 파일에서 파싱한, 전체 아이템에 대한 정보
    [HideInInspector] public ParsingData allData;

    // csv 파일에서 파싱한, 전체 아이템에 대한 조합 정보
    [HideInInspector] public ParsingData allMix;

    // stage1 각 item sprite
    [HideInInspector] public Sprite[] stage1_sprites;


    private void Start()
    {
        // 아이템 리스트에 대한 각 변수들을 할당
        Transform trans = itemContent.transform;
        itemSlots = new SlotScript[trans.childCount];
        for (int num = 0; num < trans.childCount; num++)
        {
            itemSlots[num] = trans.GetChild(num).gameObject.GetComponent<SlotScript>();
            itemSlots[num].InitSlot(num);
        }


        // 재료 리스트에 대한 각 변수들을 할당
        trans = materialContent.transform;
        materialSlots = new SlotScript[trans.childCount];
        for (int num = 0; num < trans.childCount; num++)
        {
            materialSlots[num] = trans.GetChild(num).gameObject.GetComponent<SlotScript>();
            materialSlots[num].InitSlot(-1);
        }


        // All Item List scriptable object 캐싱
        allData = Resources.Load("CSVData/All Item List Asset") as ParsingData;

        // All Mix List scriptable object 캐싱
        allMix = Resources.Load("CSVData/All Mix List Asset") as ParsingData;

        // stage1 item 이미지 캐싱
        stage1_sprites = Resources.LoadAll<Sprite>("ArtResource/Stage1/object/Stage1 All Object");
    }


    // ========================================================== 공개 메서드 정의 ====================================================

    // 조합창 속의 조합 버튼 Click
    public void Mix()
    {
        // 임시 캐싱
        List<Dictionary<string, object>> temp = allMix.list;

        // 재료 slot에 들어있는 모든 재료들의 수의 합
        int countSum = 0;
        for (int i = 0; i < conSlot_material; i++)
            countSum += materialSlots[i].GetCount();

        // 조합 가능 식별
        bool mixOK = false;

        // Mix Info List를 돌며 현재 올려놓은 Material 들을 가지고 조합 가능한 item이 있는지 확인
        for (int i =0; i < temp.Count; i++)
        {
            // Mix Resource 하나를 가져와서..
            string mixInfo = temp[i]["RESOURCE"] as string;
            // '/' 문자마다 분리(즉, 각 재료들로 문자열을 분리)
            string[] splits = mixInfo.Split('/');

            // 1. 재료의 수에 대한 비교
            if(countSum != splits.Length)
            {
                // 등록한 재료와 필요한 재료의 수가 다르다.
                // 조합 불가.
                continue;
            }

            // 2. 재료의 종류에 대한 비교
            for (int j = 0; j < conSlot_material; j++)
            {
                // 각 slot마다 조합 정보를 이용하여 비교한다.
                // 비교할 slot의 아이템 수
                int tempCount = materialSlots[j].GetCount();

                // 이곳에서 재료 slot에 올려진 재료와 재료 목록 info를 비교한다.
                foreach (string split in splits)
                {
                    if (materialSlots[j].GetName() == split)
                        tempCount--;
                }

                // 이 수가 0이되지 않았다. 즉, slot에 필요하지 않은 재료를 추가한 것 이므로 조합을 할 수 없다.
                if (tempCount != 0)
                {
                    // 조합 불가.
                    mixOK = false;
                    break;
                }

                mixOK = true;
            }

            if (mixOK)
            {
                // 재료의 수와 종류가 같으므로 조합 가능
                for (int k = conSlot_material - 1; k >= 0; k--)
                {
                    while(materialSlots[k].GetCount() > 0)
                    {
                        // 조합 창에 올려놓은 재료의 수만큼 아이템 창에서의 아이템을 삭제
                        itemSlots[materialSlots[k].GetIndex()].DeleteItem(1);
                        // 조합 창에 올려놓은 재료 정보를 삭제
                        materialSlots[k].DeleteMaterial(1);
                    }
                }

                // 조합한 결과 아이템을 넣는다.
                // 만약 조합 결과 아이템과 같은 종류의 아이템이 이미 있다면 그 slot의 count를 증가시키고..
                string tempID = allData.GetIDByName(temp[i]["RESULT"] as string);
                bool tempBool = false;
                for (int k = 0; k < conSlot_item; k++)
                {
                    if(itemSlots[k].GetID() == tempID)
                    {
                        tempBool = true;
                        itemSlots[k].CountPlus(1);
                    }
                }
                // 만약 조합 결과 아이템과 같은 종류의 아이템이 없다면 마지막 slot에 추가시키고..
                if (!tempBool)
                {
                    string tempName = temp[i]["RESULT"] as string;
                    string tempExp = GetExplanation(tempID);
                    Sprite tempSprite = GetSprite(tempID);

                    itemSlots[conSlot_item].GetNewItem(tempID, tempName, tempExp, tempSprite);
                    itemSlots[conSlot_item].CountPlus(1);

                    conSlot_item++;
                }

                // 삭제된 Item slot에 의해 생긴 빈 공간을 정렬
                SortItemSlot();

                // 이전에 표시되어있는 조합 불가 표시를 지우고..
                IsMixOKReverse();

                // 새로 정렬된 아이템들에 조합 불가를 판정
                IsMixOK();

                // 만약 아이템 획득에 대한 AllCondition 변화 유무가 있다면 해당 condition을 체크
                AllConditions conditions = Resources.Load<AllConditions>("AllConditions");
                foreach (Condition condition in conditions.conditions)
                {
                    if (condition.description == MixConditionConverter.Converter(temp[i]["RESULT"] as string))
                    {
                        condition.satisfied = true;
                        break;
                    }
                }
                return;
            }
        }
    }


    // 현재 가지고 있는 아이템 목록 중 조합이 불가능한 아이템 표시
    public void IsMixOK()
    {
        // 각 slot 마다 가지고 있는 물건이 조합 가능한지 판별
        // slot이 비어있으면 아무 작동 x
        foreach (SlotScript slot in itemSlots)
            slot.IsMixOK();
    }


    // 조합 불가능한 아이템 표시를 제거
    public void IsMixOKReverse()
    {
        foreach(SlotScript slot in itemSlots)
        {
            slot.IsMixOKReverse();
        }
    }

    // 조합 하려는 item을 조합 창으로 들고 왔는지 판별
    public void Enter()
    {
        isDrag = true;
    }

    // 조합 하려는 item을 조합 창으로 들고 왔는지 판별
    public void Exit()
    {
        isDrag = false;
    }

    // 현재 slot으로부터 drag 중인지 판별
    public bool GetIsDrag()
    {
        return isDrag;
    }




    //------------------------------------------------------------------------------ Item slot 호출 function ----------------------------------------------

    // Item 획득 시 호출(ID는 item의 csv 파일 내 ID 값)
    public void GetItem(string ID)
    {
        // 새로 가지게된 아이템이..
        for (int i = 0; i < conSlot_item; i++)
        {
            // 현재 가지고있는 아이템과 같은 아이템이라면..
            if (itemSlots[i].GetID() == ID)
            {
                // 해당 slot 아이템의 수를 증가시키고 종료
                itemSlots[i].CountPlus(1);
                return;
            }
        }

        // 비워져 있는 item 칸이 있는지 확인
        if (conSlot_item >= itemSlots.Length)
        {
            // 허용 가능한 아이템 획득 갯수를 초과하였다,

            return;
        }
        else
        {
            string tempName = GetName(ID);
            string tempExp = GetExplanation(ID);
            Sprite tempSprite = GetSprite(ID);

            // slot에 아이템 정보를 전송
            itemSlots[conSlot_item].GetNewItem(ID, tempName, tempExp, tempSprite);
            itemSlots[conSlot_item].CountPlus(1);

            // 현재 채워져 있는 slot 갯수 변경
            conSlot_item++;
        }
    }

    // Item 제거 시 호출(ID는 item의 csv 파일 내 ID 값) - 삭제 유무를 반환
    public bool RemoveItem(string ID)
    {
        bool isDoingRemove = false;
        int index = 0;

        // 해당 ID의 아이템이 있는지 조사
        for (int i = 0; i < conSlot_item; i++)
        {
            // 현재 가지고 있는 아이템이 삭제될 대상이면..
            if (itemSlots[i].GetID() == ID)
            {
                // 해당 정보를 저장
                index = i;
                isDoingRemove = true;

                break;
            }
        }

        // 해당 ID의 아이템이 없으므로 삭제를 할 수 없음을 반환하며 종료
        if (isDoingRemove != true)
            return isDoingRemove;


        // 해당 ID의 아이템을 삭제
        itemSlots[index].DeleteItem(1);

        // 필요 시 각 slot간 정렬
        SortItemSlot();

        return isDoingRemove;
    }

    // 해당 slot의 정보 삭제
    public void DeleteItemSlot(int index)
    {
        itemSlots[index].DeleteSlot();
    }

    // 모든 slot 사이 빈 slot들을 정렬시킨다.
    public void SortItemSlot()
    {
        int deleteCount = 0;

        // 모든 slot을 순회하며..
        for(int i = 0; i < conSlot_item; i++)
        {
            // 해당 slot의 ID가 "" 인 경우.. (즉, 해당 slot은 삭제되었다. )
            if(itemSlots[i].GetID() == "")
            {
                // 이후의 모든 slot들은 deleteCount만큼 앞으로 당겨진다.
                deleteCount++;
            }
            else
            {
                itemSlots[i - deleteCount].GetNewItem(
                    itemSlots[i].GetID(),
                    itemSlots[i].GetName(),
                    itemSlots[i].GetExp(),
                    itemSlots[i].GetImage());
                itemSlots[i - deleteCount].SetCount(itemSlots[i].GetCount());
            }
        }

        for(int i = conSlot_item - deleteCount; i < conSlot_item; i++ )
        {
            itemSlots[i].DeleteSlot();
        }
        conSlot_item -= deleteCount;
    }

    // 해당 index에 해당하는 item의 ID 반환
    public string GetIDByIndexInItemSlot(int index)
    {
        return itemSlots[index].GetID();
    }

    // 해당 index에 해당하는 item의 Count 반환
    public int GetCountByIndexInItemSlot(int index)
    {
        return itemSlots[index].GetCount();
    }

    // 현재 사용중인 item slot의 갯수 반환
    public int GetItemSlotsCount()
    {
        return conSlot_item;
    }

    // 해당 이름에 해당하는 아이템이 있는가?
    public bool IsGottenItem(string name)
    {
        // 새로 가지게된 아이템이..
        for (int i = 0; i < conSlot_item; i++)
        {
            // 매개변수의 이름과 같은 아이템이면..
            if (itemSlots[i].GetName() == name)
                return true;
        }
        return false;
    }

    public string GetIDByName(string name)
    {
        return allData.GetIDByName(name);
    }

    public string GetExpByName(string name)
    {
        return allData.GetExplanationByName(name);
    }

    //------------------------------------------------------------------------------ Material slot 호출 function ----------------------------------------------

    // item slot의 아이템을 material slot으로 이동 시 호출
    public void SetMaterial(string ID, string name, string exp, Sprite sprite, int index)
    {
        // 새로 등록할 재료가..
        for (int i = 0; i < conSlot_material; i++)
        {
            // 현재 가지고있는 재료와 같은 재료라면..
            if (materialSlots[i].GetIndex() == index)
            {
                // 해당 slot 재료의 수를 증가시키고 종료, 단 최대 증가 수는 참조하는 item slot의 count 수이다.
                if (itemSlots[index].GetCount() > materialSlots[i].GetCount())
                {
                    materialSlots[i].CountPlus(1);
                }
                return;
            }
        }
        
        // 비워져 있는 item 칸이 있는지 확인
        if (conSlot_material >= materialSlots.Length)
        {
            // 허용 가능한 재료 등록 갯수를 초과하였다,

            return;
        }
        else
        {
            // 만약 등록하려는 item slot의 아이템이 이미 재료로 등록되어 있다면 등록을 거부
            foreach (SlotScript slot in materialSlots)
            {
                if (slot.GetIndex() == index)
                    return;
            }

            // slot에 재료 정보를 전송
            materialSlots[conSlot_material].SetNewMaterial(ID, name, exp, sprite, index);
            materialSlots[conSlot_material].CountPlus(1);

            // 현재 채워져 있는 material 갯수 변경
            conSlot_material++;
        }
    }

    // 해당 index 정보를 가지는 material slot을 비운다.
    public void DeleteMaterialSlot(int index)
    {
        // 모든 slot material들의 index를 매개변수 index와 비교하여, 같은 index를 가지는 slot을 삭제한다.
        // 가장 마지막의 slot에서 같은 index가 나올경우 sort 과정이 생략되므로 가장 마지막 slot부터 검사를 진행한다.
        for (int i = conSlot_material - 1; i >= 0; i--)
        {
            // 현재 material list의 재료들의 index와 전달받은 index를 비교
            if (materialSlots[i].GetIndex() == index)
            {
                // material list 중 가장 마지막 항이 삭제되는 재료일 경우..
                if (i == conSlot_material - 1)
                {
                    // 해당 재료의 정보를 바로 지우고..
                    materialSlots[i].DeleteSlot();

                    // 현재 재료의 수를 변경하고
                    conSlot_material--;
                    // 종료
                    return;
                }
                // 그 외, 즉 중간의 material slot의 재료를 삭제하는 경우..
                else
                {
                    // 삭제된 slot 이후부터 마지막 항까지 정렬을 수행
                    for (int j = i; j < conSlot_material; j++)
                    {
                        // 마지막 slot은 삭제만 수행
                        if (j == conSlot_material - 1)
                        {
                            materialSlots[j].DeleteSlot();
                        }
                        // 마지막 slot을 제외한 나머지 slot들은, 다음의 slot 정보를 자신의 slot으로 가져온다.
                        else
                        {
                            materialSlots[j].SetNewMaterial(materialSlots[j + 1].GetID()
                                , materialSlots[j + 1].GetName()
                                , materialSlots[j + 1].GetExp()
                                , materialSlots[j + 1].GetImage()
                                , materialSlots[j + 1].GetIndex());
                            materialSlots[j].SetCount(materialSlots[j + 1].GetCount());
                        }
                    }
                    //현재 재료의 수를 변경
                    conSlot_material--;
                }
            }
        }
    }

    // 조합창 표시 버튼을 클릭할 때마다 기존에 있던 material 정보를 삭제 
    public void DeleteAllMaterial()
    {
        while (conSlot_material > 0)
            DeleteMaterialSlot(materialSlots[conSlot_material - 1].GetIndex());
    }




    // ========================================================== 비공개 메서드 정의 ====================================================


    // 전체 아이템 목록의 scriptable object 에서 해당 ID에 해당하는 item의 이름 반환
    private string GetName(string ID)
    {
        return allData.GetNameByID(ID);
    }

    // 전체 아이템 목록의 scriptable object 에서 해당 ID에 해당하는 item의 설명 반환
    private string GetExplanation(string ID)
    {
        return allData.GetExplanationByID(ID);
    }

    // 전체 이미지 목록 중 해당 ID에 해당하는 Sprite 반환, 없으면 null 반환
    private Sprite GetSprite(string ID)
    {
        string spriteName = allData.GetSpriteByID(ID);

        foreach(Sprite sprite in stage1_sprites)
            if (sprite.name == spriteName)
                return sprite;

        return null;
    }
}
