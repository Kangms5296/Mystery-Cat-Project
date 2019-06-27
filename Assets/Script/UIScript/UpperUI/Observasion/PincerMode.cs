using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PincerMode : TouchMode
{
    // 각 Mode는 여러 객체가 필요치 않으니, 싱글톤 패턴으로 구현한다.
    public static PincerMode pincerMode = new PincerMode();
    private PincerMode() { }

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
    private bool isTouch = false;

    // 각 캐릭터 별 아이템(털) 획득 유무 확인
    [HideInInspector] public Dictionary<string, string> furGetList = new Dictionary<string, string>();

    // 아이템 획득 정보 및 NPC 대사 저장
    public struct GET_INFO
    {
        public string itemName;
        public string talkScript;
    }
    public GET_INFO get_info = new GET_INFO();


    // ===================================================== public function ===========================================================


    public void OnButtonDownNPC(TouchPointScript conTouch)
    {
        touchDownPosition = Input.mousePosition;

        isTouch = true;
    }

    public void OnButtonUpNPC()
    {
        if (isTouch == false)
            return;

        touchUpPosition = Input.mousePosition;

        // 만약 드래그한 길이(y축만 판별)가 최소 길이를 넘으면..
        if (touchUpPosition.y - touchDownPosition.y >= standardMinDrag * (Screen.width / screenWidth))
        {
            ParsingData tempData = Resources.Load("CSVData/All Observation List Asset") as ParsingData;
            ContentScript tempScript = GameObject.FindObjectOfType<ContentScript>();

            // 현재 대화하는 NPC 이름 캐싱
            string npcName = GameObject.FindObjectOfType<ObservationManager>().npcName;

            for (int i = 0; i < tempData.list.Count; i++)
            {
                if (tempData.list[i]["Way"] as string == "Pincer"
                    && tempData.list[i]["Character"] as string == npcName)
                {
                    // 이전에 획득하였는지 조사.
                    AllConditions conditions = Resources.Load<AllConditions>("AllConditions");
                    foreach (Condition condition in conditions.conditions)
                    {
                        if (condition.description == tempData.list[i]["Condition"] as string)
                        {
                            // 이전에 얻은 털이거나, 혹은 얻을 털이 없는경우 대사만 처리
                            if (condition.satisfied == true || tempData.list[i]["Division"] as string == "x")
                            {
                                GameObject.FindObjectOfType<ObservationManager>().GetItem("", "", tempData.list[i]["TalkScript"] as string, null);
                            }
                            // 이번에 얻게되는 아이템이므로 아이템 획득 + 대사처리
                            else
                            {
                                // 얻은 아이템 이름
                                string itemName = tempData.list[i]["Param"] as string;

                                // 현재 아이템 얻는 방법이 아이템의 ID를 통한 획득밖에 없으므로 ID를 먼저 확인.
                                string itemID = tempScript.GetIDByName(itemName);
                                // 해당 이름의 고양이털을 얻는다.
                                tempScript.GetItem(itemID);

                                // UI의 총괄 스크립트에게, 아이템을 얻었음을 알린다.
                                GameObject.FindObjectOfType<ObservationManager>().GetItem(itemName,
                                    tempScript.GetExpByName(itemName), tempData.list[i]["TalkScript"] as string, null);

                                // 아이템 획득하였음을 기록
                                condition.satisfied = true;

                            }
                            break;
                        }
                    }
                    break;
                }
            }
        }
        isTouch = false;
        Debug.Log("qsfsdf");
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
