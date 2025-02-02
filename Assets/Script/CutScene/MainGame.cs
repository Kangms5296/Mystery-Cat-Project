﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum Stage { Backgarden, Home, Village, Bar, Library_1, Library_2, Library_3, Library1_Tutorial, Library2_Tutorial, Village_Tutorial, None };

public class MainGame : MonoBehaviour {
    [Header("Start Reaciton Collections")]
    public ReactionCollection defaultReactionCollection;
    public ReactionCollection tutorialReactionCollection;

    [Header("Con Stage")]
    public Stage stage;
    public Transform stageObject;

    [Header("Can Save")]
    public bool isCanSave;

    [Header("Save - 20day")]
    public bool isDay20;            // true이면 문서단서 '오늘 날짜'에 20일로 기록
    public InformationSlot slot;
    public InformationClue clue;
    public string infoName;
    [TextArea] public string infoExp;
    public Sprite infoImage;

    [Header("Save - OnBackgarden")]
    public bool isOnBackGarden;
    public AudioSource footSource;
    public AudioClip footClip;

    [Header("Save - BirthClue")]
    public bool isFindBirthClue;    // true이면 문서단서 '출생기록부'를 획득한 상태, 획득하지 않은 생태이면 옛날 집의 오브젝트들 상호작용 제한.
    public GameObject[] hideObjects;

    [Header("Save - Ear")]
    public bool isGetEar;           // true이면 귀 오브젝트 map에서 삭제.
    public GameObject ear;

    [Header("Save - Hair")]
    public bool isGetHair;          // true이면 모자 오브젝트 map에서 삭제.
    public GameObject hair;

    [Header("Save - Spoon")]
    public bool isGetSpoon;         // true이면 숟가락 오브젝트 map에서 삭제.
    public GameObject spoon;

    [Header("Save - CriminalLine")]
    public bool isOnCriminalLine;   // true이면 InitialApproachToCriminalArea 오브젝트 SetActive
    public GameObject criminalLine;

    [Header("Save - LibraryLine")]
    public bool isOnLibraryLine;    // true이면 도서관 접근 이벤트 생성
    public GameObject libraryLine;

    [Header("Save - Library NPC Out")]
    public bool isLibraryNpcOut;    // true이면 도서관 외부로 NPC 배치.
    public Transform libraryNpc;
    public Transform libraryNpcNewPos;

    [Header("Save - Drunken NPC")]
    public bool isCustomerNotDrunken;  // true이면 취한 상태
    public Transform customerNpc;
    public Transform npcNewPos;
    public Sprite npcNewSprite;
    public GameObject npcDrunkenReaction;

    [Header("Save - Police Wall")]
    public bool isPoliceWallRemove;  // true이면 police Wall 삭제.
    public GameObject policeWall;

    [Header("Save - Library Temp")]
    public bool isLibraryTempRemove; // true이면 Library Temp 오브젝트 삭제
    public GameObject libraryTemp;


    private SaveDataSystem saveData;

    // Use this for initialization
    void Start () {
        if ((1920 * Screen.height / Screen.width) > 1080)
        {
            CanvasScaler[] canvases = GameObject.FindObjectsOfType<CanvasScaler>();
            for (int i = 0; i < canvases.Length; i++)
                canvases[i].referenceResolution = new Vector2(1920, 1920 * Screen.height / Screen.width);
        }

        // Load / Save를 위한 script 캐싱
        saveData = FindObjectOfType<SaveDataSystem>();

        // 현재 사용하는 slot 정보를 가져온다.
        int usingSlotCount = StaticInfoForSound.playingSlotIndex;

        // 메인 씬에서 불러오기를 선택했다면..
        if (usingSlotCount != 4)
        {
            // 이전 데이터를 불러온다.
            saveData.LoadData(usingSlotCount);

            // 이전 데이터로 현재 데이터를 변경한다.
            LoadData();

            // 게임 시작.
            defaultReactionCollection.InitAndReact();
        }
        else
        {
            StaticInfoForSound.con_BGM_Audio = GameObject.Find("BGMSound_Main").GetComponent<AudioSource>();

            // 튜토리얼 시작
            tutorialReactionCollection.InitAndReact();
        }
	}
    
    public void LoadData()
    {
        // 이전의 위치로 플레이어를 이동
        FindObjectOfType<NewPlayer>().transform.position = new Vector3((float)saveData.tempData.posX, (float)saveData.tempData.posY, (float)saveData.tempData.posZ);

        // 이전의 미션으로 현재 미션을 갱신
        FindObjectOfType<MissionScript>().SetMission(saveData.tempData.mission);

        // 이전 stage로 현재 위치를 갱신
        stage = saveData.tempData.stage;
        // 최초 맵 Active
        stageObject.Find(stage.ToString()).gameObject.SetActive(true);

        // 이전의 상태로 진행상태를 갱신
        AllConditions allConditions = Resources.Load<AllConditions>("AllConditions");
        for (int i = 0; i < allConditions.conditions.Length; i++)
            allConditions.conditions[i].satisfied = saveData.tempData.conditionsCheck[i];

        // 이전의 상태로 인벤토리 정보를 갱신
        ContentScript inventory = FindObjectOfType<ContentScript>();
        for (int i = 0; i < saveData.tempData.itemsCheck.Count; i++)
            for (int j = 0; j < saveData.tempData.itemsCheck[i].count; j++)
                inventory.GetItem(saveData.tempData.itemsCheck[i].id);

        // 이전의 상태로 문서단서 정보를 갱신
        DocumentInformationManager information = FindObjectOfType<DocumentManager>().information;
        for(int i = 0; i < saveData.tempData.infosCheck.Count; i++)
            if(saveData.tempData.infosCheck[i])
                information.KnowNewInfoByIndex(i);

        // 이전의 상태로 인물단서 정보를 갱신
        DocumentCharacterManager character = FindObjectOfType<DocumentManager>().character;
        for (int i = 0; i < saveData.tempData.charsCheck.Count; i++)
        {
            CharacterSlot slot = character.GetSlot(saveData.tempData.charsCheck[i].name);
            slot.CharacterKnow(false);
            slot.characterExp = saveData.tempData.charsCheck[i].exp;
        }

        // 이전의 상태로 사운드 상태를 변경
        StaticInfoForSound.con_BGM_Audio = GameObject.Find(saveData.tempData.conBgmSourceName).GetComponent<AudioSource>();
        if (saveData.tempData.conBgmClipName != "")
        {
            StaticInfoForSound.con_BGM_Audio.clip = Resources.Load<AudioClip>("AudioResource/BGM/" + saveData.tempData.conBgmClipName);
            StartCoroutine(SoundStart());
        }



        // etc
        isCanSave = saveData.tempData.isCanSave;

        isDay20 = saveData.tempData.isDay20;
        if (isDay20)
        {
            if (slot != null)
            {
                slot.informationName = infoName;
                slot.informationExp = infoExp;
                slot.informationImage = infoImage;
            }

            if (clue != null)
            {
                clue.informationName = infoName;
                clue.informationExp = infoExp;
                clue.informationImage = infoImage;

                clue.transform.Find("Known").GetComponent<Image>().sprite = infoImage;

            }
        }

        isOnBackGarden = saveData.tempData.isOnBackgarden;
        if (isOnBackGarden)
        {
            footSource.clip = footClip;
            footSource.Play();
        }

        isFindBirthClue = saveData.tempData.isFindBirthClue;
        if (isFindBirthClue == false)
            foreach (GameObject hideObject in hideObjects)
                hideObject.SetActive(false);


        isGetEar = saveData.tempData.isGetEar;
        if (isGetEar)
            ear.SetActive(false);


        isGetHair = saveData.tempData.isGetHair;
        if (isGetHair)
            hair.SetActive(false);


        isGetSpoon = saveData.tempData.isGetSpoon;
        if (isGetSpoon)
            spoon.SetActive(false);


        isOnCriminalLine = saveData.tempData.isOnCriminalLine;
        if (isOnCriminalLine)
            criminalLine.SetActive(true);


        isOnLibraryLine = saveData.tempData.isOnLibraryLine;
        if (isOnLibraryLine)
            libraryLine.SetActive(true);

        isLibraryNpcOut = saveData.tempData.isLibraryNpcOut;
        if(isLibraryNpcOut)
            libraryNpc.position = new Vector3(libraryNpcNewPos.position.x, libraryNpcNewPos.position.y, libraryNpcNewPos.position.y);


        isCustomerNotDrunken = saveData.tempData.isCustomerNotDrunken;
        if (isCustomerNotDrunken)
        {
            customerNpc.position = new Vector3(npcNewPos.position.x, npcNewPos.position.y, npcNewPos.position.y);
            customerNpc.Find("Sprite").GetComponent<SpriteRenderer>().sprite = npcNewSprite;
            npcDrunkenReaction.SetActive(false);
        }


        isPoliceWallRemove = saveData.tempData.isPoliceWallRemove;
        if (isPoliceWallRemove)
            policeWall.SetActive(false);

        isLibraryTempRemove = saveData.tempData.isLibraryTempRemove;
        if (isLibraryTempRemove)
            libraryTemp.SetActive(false);
        
    }

    IEnumerator SoundStart()
    {
        yield return new WaitForSeconds(0.5f);

        float conTime = 0;
        float maxTime = 2;
        
        StaticInfoForSound.con_BGM_Audio.volume = 0;
        StaticInfoForSound.con_BGM_Audio.Play();

        while (conTime < maxTime)
        {
            StaticInfoForSound.con_BGM_Audio.volume = StaticInfoForSound.BGMSound * (conTime) / maxTime;
            conTime += Time.deltaTime;
            yield return null;
        }
        StaticInfoForSound.con_BGM_Audio.volume = StaticInfoForSound.BGMSound;
    }
}
