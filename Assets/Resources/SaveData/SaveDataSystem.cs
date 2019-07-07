using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

[System.Serializable]
public enum Stage { Backgarden, Home, Viliage, Bar, Library1, Library2, Library3, Library1T, Library2T, ViliageT, None };

[CreateAssetMenu]
public class SaveDataSystem : ScriptableObject {

    // 이전에 저장한 적이 있는가?
    public bool isUsing;



    // 저장한 시간
    public string time;

    // 저장할 당시 플레이어의 위치
    public Vector3 pos;

    // 저장할 당시 stage
    public Stage stage;

    // 저장할 당시의 미션
    public string mission;



    // 현재 진행상태
    public List<bool> conditionsCheck;

    // 인벤토리
    [System.Serializable]
    public struct ItemInfo
    {
        public string id;
        public int count;
    }
    public List<ItemInfo> itemsCheck;

    // 문서정보
    public List<bool> infosCheck;

    // 인물정보
    [System.Serializable]
    public struct CharInfo
    {
        public string name;
        [TextArea] public string exp;
    }
    public List<CharInfo> charsCheck;



    // Sound
    public string conBgmSourceName;
    public string conBgmClipName;



    // 그외(하드코딩내역)
    public bool isDay20;            // true이면 문서단서 '오늘 날짜'에 20일로 기록
    public bool isFindBirthClue;    // true이면 문서단서 '출생기록부'를 획득한 상태, 획득하지 않은 생태이면 옛날 집의 오브젝트들 상호작용 제한.
    public bool isGetEar;           // true이면 귀 오브젝트 map에서 삭제.
    public bool isGetHair;          // true이면 모자 오브젝트 map에서 삭제.
    public bool isGetSpoon;         // true이면 숟가락 오브젝트 map에서 삭제.
    public bool isOnCriminalLine;   // true이면 InitialApproachToCriminalArea 오브젝트 SetActive
    public bool isOnLibraryLine;    // true이면 AfterGiveMilk_BeforeGiveFur 오브젝트 SetActive
    public bool isCustomerNotDrunken;// true이면 술 깬 상태
    public bool isPoliceWallRemove;  // true이면 police Wall 삭제.
    public bool isLibraryTempRemove; // true이면 Library Temp 오브젝트 삭제


    // -------------------------------------------------------------------- public Function --------------------------------------------------------------------

    public void SaveData()
    {
        SaveBase();

        SaveCondition();

        SaveInventory();

        SaveDocumentInfo();

        SaveDocumentChar();

        SaveSound();

        SaveEtc();
    }

    public void ResetData()
    {
        isUsing = false;

        time = "";
        pos = Vector3.zero;
        stage = Stage.None;
        mission = "";

        conditionsCheck.Clear();
        itemsCheck.Clear();
        infosCheck.Clear();
        charsCheck.Clear();

        conBgmSourceName = "";
        conBgmClipName = "";

        isDay20 = false;
        isFindBirthClue = false;
        isGetEar = false;
        isGetHair = false;
        isGetSpoon = false;
        isOnCriminalLine = false;
        isOnLibraryLine = false;
        isCustomerNotDrunken = false;
        isPoliceWallRemove = false;
        isLibraryTempRemove = false;
    }

    // -------------------------------------------------------------------- private Function --------------------------------------------------------------------

    // 기본적인 내용 저장
    private void SaveBase()
    {
        // 현재 slot을 사용중인가? (이전에 저장을 한 slot인가?)
        isUsing = true;

        // 저장 시간 기록
        time = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm");

        // 캐릭터 위치 저장
        pos = FindObjectOfType<NewPlayer>().GetComponent<Transform>().position;
        
        // 현재 미션 저장
        mission = FindObjectOfType<MissionScript>().GetMission();

        UnityEditor.EditorUtility.SetDirty(this);
    }
    
    // 진행상태(Condition) 저장
    private void SaveCondition()
    {
        // 이전 값 제거
        conditionsCheck.Clear();

        // 현재의 상태를 저장
        AllConditions allConditions = Resources.Load<AllConditions>("AllConditions");
        foreach (Condition condition in allConditions.conditions)
            conditionsCheck.Add(condition.satisfied);
    }

    // 인벤토리 저장
    private void SaveInventory()
    {
        // 이전 값 제거
        itemsCheck.Clear();

        // 현재 상태를 저장
        ContentScript inventory = FindObjectOfType<ContentScript>();
        for (int i = 0; i < inventory.GetItemSlotsCount(); i++)
        {
            ItemInfo item = new ItemInfo
            {
                id = inventory.GetIDByIndexInItemSlot(i),
                count = inventory.GetCountByIndexInItemSlot(i)
            };

            itemsCheck.Add(item);
        }
    }

    // 문서정보 저장
    private void SaveDocumentInfo()
    {
        // 이전 값 제거
        infosCheck.Clear();

        // 현재 상태를 저장
        DocumentInformationManager information = FindObjectOfType<DocumentManager>().information;
        for(int i =0; i < information.slots.Length; i++)
            infosCheck.Add(information.IsSlotKnown(i));
    }

    // 인물정보 저장
    private void SaveDocumentChar()
    {
        // 이전 값 제거
        charsCheck.Clear();

        // 현재 상태를 저장
        DocumentCharacterManager character = FindObjectOfType<DocumentManager>().character;
        foreach(CharacterSlot slot in character.slots)
        {
            if(slot.isKnown)
            {
                CharInfo charInfo = new CharInfo
                {
                    name = slot.characterName,
                    exp = slot.characterExp
                };
                charsCheck.Add(charInfo);
            }
        }
    }

    // 저장할 시점의 음향상태 저장
    private void SaveSound()
    {
        conBgmSourceName = StaticInfoForSound.con_BGM_Audio.name;
        if (StaticInfoForSound.con_BGM_Audio.clip == null)
            conBgmClipName = "";
        else
            conBgmClipName = StaticInfoForSound.con_BGM_Audio.clip.name;
    }

    // 그 외
    private void SaveEtc()
    {
        MainGame instantData = FindObjectOfType<MainGame>();
        isDay20 = instantData.isDay20;
        isFindBirthClue = instantData.isFindBirthClue;
        isGetEar = instantData.isGetEar;
        isGetHair = instantData.isGetHair;
        isGetSpoon = instantData.isGetSpoon;
        isOnCriminalLine = instantData.isOnCriminalLine;
        isOnLibraryLine = instantData.isOnLibraryLine;
        isCustomerNotDrunken = instantData.isCustomerNotDrunken;
        isPoliceWallRemove = instantData.isPoliceWallRemove;
        isLibraryTempRemove = instantData.isLibraryTempRemove;
    }
}
