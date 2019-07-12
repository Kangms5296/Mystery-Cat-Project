using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class SaveDataSystem : MonoBehaviour {

    public SlotData tempData;

    // Use this for initialization
    void Start()
    {
        tempData = new SlotData();
    }


    // -------------------------------------------------------------------- public Function --------------------------------------------------------------------

    public void SaveData(int index)
    {
        // tempData에 현재 상황 임시 저장
        SaveBase();
        SaveCondition();
        SaveInventory();
        SaveDocumentInfo();
        SaveDocumentChar();
        SaveSound();
        SaveEtc();

        // tempData에 임시 저장한 내용을 해당 index의 JSON 파일로 저장
        JsonData dataJson = JsonMapper.ToJson(tempData);
        File.WriteAllText(Application.persistentDataPath + "SlotData" + index + ".json", dataJson.ToString());
    }

    public void LoadData(int index)
    {
        // 해당 index의 JSON 파일에 저장되어 있는 데이터를 tempData에 임시 저장
        string jsonString = File.ReadAllText(Application.persistentDataPath + "SlotData" + index + ".json");
        tempData = JsonUtility.FromJson<SlotData>(jsonString);
    }


    // -------------------------------------------------------------------- private Function --------------------------------------------------------------------

    // 기본적인 내용 저장
    private void SaveBase()
    {
        // 현재 slot을 사용중인가? (이전에 저장을 한 slot인가?)
        tempData.isUsing = true;

        // 저장 시간 기록
        tempData.time = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm");

        // 캐릭터 위치 저장
        tempData.posX = FindObjectOfType<NewPlayer>().GetComponent<Transform>().position.x;
        tempData.posY = FindObjectOfType<NewPlayer>().GetComponent<Transform>().position.y;
        tempData.posZ = FindObjectOfType<NewPlayer>().GetComponent<Transform>().position.z;

        // 현재 미션 저장
        tempData.mission = FindObjectOfType<MissionScript>().GetMission();
    }
    
    // 진행상태(Condition) 저장
    private void SaveCondition()
    {
        // 이전 값 제거
        tempData.conditionsCheck.Clear();

        // 현재의 상태를 저장
        AllConditions allConditions = Resources.Load<AllConditions>("AllConditions");
        foreach (Condition condition in allConditions.conditions)
            tempData.conditionsCheck.Add(condition.satisfied);
    }

    // 인벤토리 저장
    private void SaveInventory()
    {
        // 이전 값 제거
        tempData.itemsCheck.Clear();

        // 현재 상태를 저장
        ContentScript inventory = FindObjectOfType<ContentScript>();
        for (int i = 0; i < inventory.GetItemSlotsCount(); i++)
        {
            SlotData.ItemInfo item = new SlotData.ItemInfo
            {
                id = inventory.GetIDByIndexInItemSlot(i),
                count = inventory.GetCountByIndexInItemSlot(i)
            };

            tempData.itemsCheck.Add(item);
        }
    }

    // 문서정보 저장
    private void SaveDocumentInfo()
    {
        // 이전 값 제거
        tempData.infosCheck.Clear();

        // 현재 상태를 저장
        DocumentInformationManager information = FindObjectOfType<DocumentManager>().information;
        for(int i =0; i < information.slots.Length; i++)
            tempData.infosCheck.Add(information.IsSlotKnown(i));
    }

    // 인물정보 저장
    private void SaveDocumentChar()
    {
        // 이전 값 제거
        tempData.charsCheck.Clear();

        // 현재 상태를 저장
        DocumentCharacterManager character = FindObjectOfType<DocumentManager>().character;
        foreach(CharacterSlot slot in character.slots)
        {
            if(slot.isKnown)
            {
                SlotData.CharInfo charInfo = new SlotData.CharInfo
                {
                    name = slot.characterName,
                    exp = slot.characterExp
                };
                tempData.charsCheck.Add(charInfo);
            }
        }
    }

    // 저장할 시점의 음향상태 저장
    private void SaveSound()
    {
        tempData.conBgmSourceName = StaticInfoForSound.con_BGM_Audio.name;
        if (StaticInfoForSound.con_BGM_Audio.clip == null)
            tempData.conBgmClipName = "";
        else
            tempData.conBgmClipName = StaticInfoForSound.con_BGM_Audio.clip.name;
    }

    // 그 외
    private void SaveEtc()
    {
        MainGame instantData = FindObjectOfType<MainGame>();
        tempData.stage = instantData.stage;
        tempData.isDay20 = instantData.isDay20;
        tempData.isOnBackgarden = instantData.isOnBackGarden;
        tempData.isFindBirthClue = instantData.isFindBirthClue;
        tempData.isGetEar = instantData.isGetEar;
        tempData.isGetHair = instantData.isGetHair;
        tempData.isGetSpoon = instantData.isGetSpoon;
        tempData.isOnCriminalLine = instantData.isOnCriminalLine;
        tempData.isOnLibraryLine = instantData.isOnLibraryLine;
        tempData.isCustomerNotDrunken = instantData.isCustomerNotDrunken;
        tempData.isPoliceWallRemove = instantData.isPoliceWallRemove;
        tempData.isLibraryTempRemove = instantData.isLibraryTempRemove;
        tempData.isCanSave = instantData.isCanSave;
        tempData.isLibraryNpcOut = instantData.isLibraryNpcOut;
    }
    
}
