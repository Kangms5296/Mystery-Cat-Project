using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 현재 Displayer 우측 버튼 중 Look 버튼이 눌러져있을때, 아래 행동에 대한 처리 방법을 기술
public class LookMode : TouchMode
{
    // 돋보기 아이콘들
    private RectTransform observationAreaBG;
    private RectTransform observationAreaInput;

    // 각 Mode는 여러 객체가 필요치 않으니, 싱글톤 패턴으로 구현한다.
    public static LookMode lookMode = new LookMode();
    private LookMode() { }

    private Vector2 firstBgPosition = new Vector2(60, 0);
    private Vector2 firstInputPosition = new Vector2(-25, -35);

    public TouchPointScript conTouch;


    // ===================================================== public function ===========================================================


    // NPC 버튼다운 패턴 정의
    public void OnButtonDownNPC(TouchPointScript conTouch)
    {
        if (observationAreaBG == null)
        {
            observationAreaBG = GameObject.Find("ObservationArea").GetComponent<RectTransform>();
            observationAreaInput = GameObject.Find("ObservationAreaInput").GetComponent<RectTransform>();
        }

        Vector3 areaPosition;
        areaPosition = Input.mousePosition;
        observationAreaBG.transform.position = areaPosition;

        float tempX = observationAreaBG.anchoredPosition.x;
        float tempY = observationAreaBG.anchoredPosition.y;
        if (observationAreaBG.anchoredPosition.x < 30)
            tempX = 30;
        if (observationAreaBG.anchoredPosition.x > 140)
            tempX = 140;
        if (observationAreaBG.anchoredPosition.y < -140)
            tempY = -140;
        if (observationAreaBG.anchoredPosition.y > 65)
            tempY = 65;

        observationAreaBG.anchoredPosition = new Vector2(tempX, tempY);
        observationAreaInput.anchoredPosition = firstInputPosition + firstBgPosition - new Vector2(tempX, tempY);
    }

    // NPC 버튼업 패턴 정의
    public void OnButtonUpNPC()
    {
        if (observationAreaBG == null)
        {
            observationAreaBG = GameObject.Find("ObservationArea").GetComponent<RectTransform>();
            observationAreaInput = GameObject.Find("ObservationAreaInput").GetComponent<RectTransform>();
        }

        // 돋보기를 제거
        observationAreaBG.anchoredPosition = new Vector2(500, 0);
    }

    // NPC 드래그 패턴 정의
    public void OnDragNPC()
    {
        if (observationAreaBG == null)
        {
            observationAreaBG = GameObject.Find("ObservationArea").GetComponent<RectTransform>();
            observationAreaInput = GameObject.Find("ObservationAreaInput").GetComponent<RectTransform>();
        }

        Vector3 areaPosition;
        areaPosition = Input.mousePosition;
        observationAreaBG.transform.position = areaPosition;

        float tempX = observationAreaBG.anchoredPosition.x;
        float tempY = observationAreaBG.anchoredPosition.y;
        if (observationAreaBG.anchoredPosition.x < 30)
            tempX = 30;
        if (observationAreaBG.anchoredPosition.x > 140)
            tempX = 140;
        if (observationAreaBG.anchoredPosition.y < -140)
            tempY = -140;
        if (observationAreaBG.anchoredPosition.y > 65)
            tempY = 65;

        observationAreaBG.anchoredPosition = new Vector2(tempX, tempY);
        observationAreaInput.anchoredPosition = firstInputPosition + firstBgPosition - new Vector2(tempX, tempY);
    }
    
    public void InTouchArea(TouchPointScript conTouch)
    {
        if (conTouch.name != "Default")
            StaticCorouting.Start("TimeCounting", TimeCounting(conTouch));
    }

    public void OutTouchArea()
    {
        StaticCorouting.Stop("TimeCounting");
        conTouch = null;
    }


    // ===================================================== private function ===========================================================

    // 지정된 TouchPoint 최소 관찰 시간 제어
    IEnumerator TimeCounting(TouchPointScript conTouch)
    {
        float touchPointTime = 0.0f;

        // 최소 2초동안 TouchPoint를 돋보기로 보아야 관찰한것으로 인정
        while (touchPointTime < 1.0f)
        {
            touchPointTime += Time.deltaTime;
            yield return null;
        }

        // 우선 우유배달소년만 다르게 작동되므로 따로 하드코딩.. 시간이 없다..
        if (conTouch.nPCSpecialWork == TouchPointScript.NPCSpecialWork.MILKBOY)
        {
            GameObject.FindObjectOfType<ObservationManager>().GetItem("기 록", "흉터 같은 게 있다. 좀 갈라진 것 같다…?", "", GameObject.Find("07_AfterFindScar").GetComponent<ReactionCollection>()); ;
        }
        else
        {

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
                                    tempSlot.characterExp += "------------\n";
                                    tempSlot.characterExp += infoName + "\n";
                                    tempDocument.CharacterKnow(npcName);

                                    // 대사 처리
                                    GameObject.FindObjectOfType<ObservationManager>().GetItem("기 록", infoName, talkScript, null);
                                }

                                break;
                            }
                        }
                        break;

                }
            }
        }
    }

}
