using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PullMode : TouchMode
{
    // 각 Mode는 여러 객체가 필요치 않으니, 싱글톤 패턴으로 구현한다.
    public static PullMode pullMode = new PullMode();
    private PullMode() { }

    // 가장 기본이 되는 해상도
    private const float screenWidth = 551;
    private const float screenHeight = 310;

    // 가장 기본이 되는 해상도에서의 최소 털뽑기 드래그 거리
    private const float standardMinDrag = 20;

    // 최초 터치다운 좌표
    public Vector3 touchDownPosition;
    // 마지막 터치업 좌표
    public Vector3 touchUpPosition;
    // 터치 시작 확인
    public bool isTouch = false;

    public TouchPointScript conTouch;


    // ===================================================== public function ===========================================================


    public void OnButtonDownNPC(TouchPointScript conTouch)
    {
        // 배경 클릭 시 제외
        if (conTouch != null)
        {
            touchDownPosition = Input.mousePosition;

            isTouch = true;
            this.conTouch = conTouch;
        }
    }

    public void OnButtonUpNPC()
    {
        if (isTouch == false)
        {
            conTouch = null;
            return;
        }
        touchUpPosition = Input.mousePosition;

        // 만약 드래그한 길이(y축만 판별)가 최소 길이를 넘으면..
        if (touchDownPosition.y - touchUpPosition.y >= standardMinDrag * (Screen.height / screenHeight))
        {
            // 우선 우유배달소년만 다르게 작동되므로 따로 하드코딩.. 시간이 없다..
            if (conTouch.nPCSpecialWork == TouchPointScript.NPCSpecialWork.MILKBOY)
            {
                switch (conTouch.touchCount)
                {
                    case 0:
                        // 대사만 처리
                        GameObject.FindObjectOfType<ObservationManager>().GetItem("", "", "냥군님…? 아파옹…", null);
                        conTouch.touchCount++;
                        break;
                    case 1:
                        // 대사만 처리
                        GameObject.FindObjectOfType<ObservationManager>().GetItem("", "", "냥군님… 적당히t하시라옹…", null);
                        conTouch.touchCount++;
                        break;
                    case 2:
                        // 귀 획득
                        GameObject.FindObjectOfType<ObservationManager>().GetItem("가짜 귀", "우유배달소년이 끼우던 가짜 귀", "야, 그만 하라고 했지.", GameObject.Find("08_AfterGetToyEar").GetComponent<ReactionCollection>());
                        break;
                }
                return;
            }

            // 해당 이벤트들을 처리
            ParsingData tempData = Resources.Load("CSVData/All Observation List Asset") as ParsingData;

            for (int i = 0; i < tempData.list.Count; i++)
            {
                // 이벤트를 찾아서..
                if (tempData.list[i]["ID"] as string != conTouch.ID)
                    continue;
                
                // 이 이벤트에서 정보들을 추출
                string npcName = tempData.list[i]["Character"] as string;
                string talkScript = (tempData.list[i]["TalkScript"] as string).Replace("/", ",");
                string division = tempData.list[i]["Division"] as string;
                string condition = tempData.list[i]["Condition"] as string;



                // 추출한 정보를 Manager로 전송하여 처리
                switch (division)
                {
                    case "x":
                        // 별다른 이벤트가 없이 대사만 처리.
                        GameObject.FindObjectOfType<ObservationManager>().GetItem("", "", talkScript, null);
                        break;

                    case "item":
                        // 얻는 아이템의 이름 확인
                        string itemName = tempData.list[i]["Param"] as string;

                        // 이 아이템을..
                        AllConditions conditions = Resources.Load<AllConditions>("AllConditions");
                        foreach (Condition tempCondition in conditions.conditions)
                        {
                            // 이전에 아이템을 얻은적이 있는지 확인
                            if (tempCondition.description == condition)
                            {
                                // 이전에 얻은 아이템이면..
                                if (tempCondition.satisfied == true)
                                {
                                    // 대사만 처리
                                    GameObject.FindObjectOfType<ObservationManager>().GetItem("", "", talkScript, null);
                                }
                                // 이전에 얻은 적이 없다면..
                                else
                                {
                                    // 아이템을 획득하였음을 기록
                                    tempCondition.satisfied = true;

                                    // 실제 아이템을 획득
                                    ContentScript tempcontent = GameObject.FindObjectOfType<ContentScript>();
                                    string itemID = tempcontent.GetIDByName(itemName);
                                    tempcontent.GetItem(itemID);

                                    // 획득한 아이템에 대한 대사 처리
                                    GameObject.FindObjectOfType<ObservationManager>().GetItem(itemName, tempcontent.GetExpByName(itemName), talkScript, null);
                                }
                                break;
                            }
                        }
                        break;

                    case "character":
                        // 얻는 정보의 이름 확인
                        string infoName = tempData.list[i]["Param"] as string;

                        // 이 정보를..
                        conditions = Resources.Load<AllConditions>("AllConditions");
                        foreach (Condition tempCondition in conditions.conditions)
                        {
                            // 이전에 정보를 얻은적이 있는지 확인
                            if (tempCondition.description == condition)
                            {
                                // 이전에 얻은 정보이면..
                                if (tempCondition.satisfied == true)
                                {
                                    // 대사만 처리
                                    GameObject.FindObjectOfType<ObservationManager>().GetItem("", "", talkScript, null);
                                }
                                // 이전에 얻은 적이 없다면..
                                else
                                {
                                    // 정보를 획득하였음을 기록
                                    tempCondition.satisfied = true;

                                    // 해당 아이템을 획득
                                    DocumentCharacterManager tempDocument = GameObject.FindObjectOfType<DocumentManager>().character;

                                    CharacterSlot tempSlot = tempDocument.GetSlot(npcName);
                                    tempSlot.characterExp += "------------------\n";
                                    tempSlot.characterExp += infoName + "\n";
                                    tempDocument.CharacterKnow(npcName);

                                    // 대사 처리
                                    GameObject.FindObjectOfType<ObservationManager>().GetItem("기 록", infoName, talkScript, null);
                                }

                                break;
                            }
                        }
                        break;

                    case "tutorial":

                        // 이 정보를..
                        conditions = Resources.Load<AllConditions>("AllConditions");
                        foreach (Condition tempCondition in conditions.conditions)
                        {
                            // 이전에 정보를 얻은적이 있는지 확인
                            if (tempCondition.description == condition)
                            {
                                // 대사만 처리
                                GameObject.FindObjectOfType<ObservationManager>().GetItem("", "", talkScript, null);

                                // 정보를 획득하였음을 기록
                                tempCondition.satisfied = true;

                                break;
                            }
                        }
                        break;
                }
            }
        }

        isTouch = false;
        conTouch = null;
    }
    public void OnDragNPC()
    {

    }

    public void InTouchArea(TouchPointScript conTouch)
    {

    }

    public void OutTouchArea()
    {

    }
}
