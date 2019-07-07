using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGame : MonoBehaviour {
    [Header("Start Reaciton Collections")]
    public ReactionCollection defaultReactionCollection;
    public ReactionCollection tutorialReactionCollection;

    [Header("Save - 20day")]
    public bool isDay20;            // true이면 문서단서 '오늘 날짜'에 20일로 기록
    public InformationSlot slot;
    public InformationClue clue;
    public string infoName;
    [TextArea] public string infoExp;
    public Sprite infoImage;

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
    public bool isOnLibraryLine;    // true이면 도서관 접근 이벤트 생성, 도서관 입장 이벤트 삭제, 도서관 NPC 마을 밖으로 배치
    public GameObject libraryEntranceReaction;
    public GameObject libraryLine;
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

        // 현재 사용하는 slot 정보를 가져온다.
        int usingSlotCount = 0;

        saveData = Resources.Load<SaveDataSystem>("SaveData/Slot" + usingSlotCount);
        if (saveData.isUsing)
        {
            // 이전 데이터로 현제 상태를 변경
            LoadData();

            // 게임 시작
            defaultReactionCollection.InitAndReact();
        }
        else
        {
            // 튜토리얼 시작
            tutorialReactionCollection.InitAndReact();
        }
	}




    private void LoadData()
    {
        // 이전의 위치로 플레이어를 이동
        FindObjectOfType<NewPlayer>().transform.position = saveData.pos;

        // 이전의 미션으로 현재 미션을 갱신
        FindObjectOfType<MissionScript>().SetMission(saveData.mission);

        // 이전의 상태로 진행상태를 갱신
        AllConditions allConditions = Resources.Load<AllConditions>("AllConditions");
        for (int i = 0; i < allConditions.conditions.Length; i++)
            allConditions.conditions[i].satisfied = saveData.conditionsCheck[i];

        // 이전의 상태로 인벤토리 정보를 갱신
        ContentScript inventory = FindObjectOfType<ContentScript>();
        for (int i = 0; i < saveData.itemsCheck.Count; i++)
            for (int j = 0; j < saveData.itemsCheck[i].count; j++)
                inventory.GetItem(saveData.itemsCheck[i].id);

        // 이전의 상태로 문서단서 정보를 갱신
        DocumentInformationManager information = FindObjectOfType<DocumentManager>().information;
        for(int i = 0; i < saveData.infosCheck.Count; i++)
            if(saveData.infosCheck[i])
                information.KnowNewInfoByIndex(i);

        // 이전의 상태로 인물단서 정보를 갱신
        DocumentCharacterManager character = FindObjectOfType<DocumentManager>().character;
        for (int i = 0; i < saveData.charsCheck.Count; i++)
        {
            CharacterSlot slot = character.GetSlot(saveData.charsCheck[i].name);
            slot.CharacterKnow(false);
            slot.characterExp = saveData.charsCheck[i].exp;
        }

        // 이전의 상태로 사운드 상태를 변경
        StaticInfoForSound.con_BGM_Audio = GameObject.Find(saveData.conBgmSourceName).GetComponent<AudioSource>();
        if (saveData.conBgmClipName != "")
        {
            StaticInfoForSound.con_BGM_Audio.clip = Resources.Load<AudioClip>("AudioResource/BGM/" + saveData.conBgmClipName);
            StartCoroutine(SoundStart());
        }

        // etc
        isDay20 = saveData.isDay20;
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


        isFindBirthClue = saveData.isFindBirthClue;
        if (isFindBirthClue == false)
            foreach (GameObject hideObject in hideObjects)
                hideObject.SetActive(false);


        isGetEar = saveData.isGetEar;
        if (isGetEar)
            ear.SetActive(false);


        isGetHair = saveData.isGetHair;
        if (isGetHair)
            hair.SetActive(false);


        isGetSpoon = saveData.isGetSpoon;
        if (isGetSpoon)
            spoon.SetActive(false);


        isOnCriminalLine = saveData.isOnCriminalLine;
        if (isOnCriminalLine)
            criminalLine.SetActive(true);


        isOnLibraryLine = saveData.isOnLibraryLine;
        if (isOnLibraryLine)
        {
            libraryLine.SetActive(true);
            libraryEntranceReaction.SetActive(false);
            libraryNpc.position = libraryNpcNewPos.position;
        }

        isCustomerNotDrunken = saveData.isCustomerNotDrunken;
        if (isCustomerNotDrunken)
        {
            customerNpc.position = npcNewPos.position;
            customerNpc.Find("Sprite").GetComponent<SpriteRenderer>().sprite = npcNewSprite;
            npcDrunkenReaction.SetActive(false);
        }


        isPoliceWallRemove = saveData.isPoliceWallRemove;
        if (isPoliceWallRemove)
            policeWall.SetActive(false);

        isLibraryTempRemove = saveData.isLibraryTempRemove;
        if (isLibraryTempRemove)
            libraryTemp.SetActive(false);
        
    }

    IEnumerator SoundStart()
    {
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
