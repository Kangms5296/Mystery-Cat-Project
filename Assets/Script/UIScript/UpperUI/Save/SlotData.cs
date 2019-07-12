using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SlotData {

    // 이전에 저장한 적이 있는가?
    public bool isUsing;

    // 저장한 시간
    public string time;

    // 저장할 당시 플레이어의 위치
    public double posX;
    public double posY;
    public double posZ;

    // 저장할 당시 stage
    public Stage stage;

    // 저장할 당시의 미션
    public string mission;



    // 현재 진행상태
    public List<bool> conditionsCheck = new List<bool>();

    // 인벤토리
    [System.Serializable]
    public struct ItemInfo
    {
        public string id;
        public int count;
    }
    public List<ItemInfo> itemsCheck = new List<ItemInfo>();

    // 문서정보
    public List<bool> infosCheck = new List<bool>();

    // 인물정보
    [System.Serializable]
    public struct CharInfo
    {
        public string name;
        [TextArea] public string exp;
    }
    public List<CharInfo> charsCheck = new List<CharInfo>();



    // Sound
    public string conBgmSourceName;
    public string conBgmClipName;



    // 그외(하드코딩내역)
    public bool isDay20;            // true이면 문서단서 '오늘 날짜'에 20일로 기록
    public bool isOnBackgarden;
    public bool isFindBirthClue;    // true이면 문서단서 '출생기록부'를 획득한 상태, 획득하지 않은 생태이면 옛날 집의 오브젝트들 상호작용 제한.
    public bool isGetEar;           // true이면 귀 오브젝트 map에서 삭제.
    public bool isGetHair;          // true이면 모자 오브젝트 map에서 삭제.
    public bool isGetSpoon;         // true이면 숟가락 오브젝트 map에서 삭제.
    public bool isOnCriminalLine;   // true이면 InitialApproachToCriminalArea 오브젝트 SetActive
    public bool isOnLibraryLine;    // true이면 AfterGiveMilk_BeforeGiveFur 오브젝트 SetActive
    public bool isCustomerNotDrunken;// true이면 술 깬 상태
    public bool isPoliceWallRemove;  // true이면 police Wall 삭제.
    public bool isLibraryTempRemove; // true이면 Library Temp 오브젝트 삭제
    public bool isCanSave;
    public bool isLibraryNpcOut;
}
