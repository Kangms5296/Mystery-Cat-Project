﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObservationManager : MonoBehaviour {

    // Displayer Object
    public GameObject Displayer;

    // Displayer 우측 버튼 관리 manager
    public ObservationWayManager observationWayManager;

    // Displayer 좌상단 획득 정보 Text
    public Text gottenItemNameText;
    public Text gottenItemExpText;

    // Displayer 좌하단 NPC 대사 Text
    public Text chatText;

    // Displayer 중단 NPC 이미지
    public Image NPCImage;
    // Look 모드에서 사용될 거대 이미지
    public Image NPCBigImage;

    // Pull Event List
    public GameObject pullEvents;

    // Look Event List
    public GameObject lookEvents;

    [Header("Getting Item(Info) Panel")]
    public GameObject gettingItemPanel;
    public Text gettingItemName;
    public Text gettingItemExp;
    public AudioSource audioSource;
    public AudioClip audioClipForItem;
    public AudioClip audioClipForDocu;
    public AudioClip audioClipForClick;
    private ReactionCollection afterReaction;

    



    // 현재 대화하고 있는 NPC 정보 캐싱
    [HideInInspector]
    public string npcName;

    // Observation을 위한 터치, 드래그 동작에 대한 행동
    private TouchMode touchMode;

    // 좌측 Text들 출력 코루틴
    private IEnumerator itemInfoCoroutine;
    private IEnumerator talkScriptCoroutine;


    // ================================================= public Function ===========================================================

    // Displayer 표시
    public void Init(string npcName, bool disabledPull, bool disabledLook, bool disabledPincer)
    {
        // 현재 대화하고 있는 npc 이름 갱신. 이를 통해 나머지 정보들을 초기화
        this.npcName = npcName;

        // Displayer 우측 버튼 초기화
        observationWayManager.Init(disabledPull, disabledLook, disabledPincer);

        // Displayer 좌측 Text들 초기화
        GottenItemTextInit();
        ChatTextInit();

        // Dlspalyer 중단 NPC 이미지 초기화
        NPCImageInit();

        // Displayer 화면에 표시
        Displayer.SetActive(true);
    }

    // Displayer 시작
    public void Visible()
    {
        Displayer.SetActive(true);
    }

    // Displayer 종료
    public void OnClickExit()
    {
        // 좌측 글자 출력 코루틴들이 진행중이면 중단.
        if (talkScriptCoroutine != null)
            StopCoroutine(itemInfoCoroutine);
        if (talkScriptCoroutine != null)
            StopCoroutine(talkScriptCoroutine);

        Displayer.SetActive(false);
    }

    // 초기 아무런 버튼을 안누른 상태
    public void OnClickDefaultBtn()
    {
        // Pull 행동에 대한 Event List 제거
        pullEvents.SetActive(false);

        // Look 행동에 대한 Event List 제거
        lookEvents.SetActive(false);

        touchMode = DefaultMode.defaultMode;
    }

    // Displayer 우측 버튼 중 Touch 버튼 클릭
    public void OnClickPullBtn()
    {
        // 이미지 터치 기준 알파값 지정(터치 지점 알파값이 0.1f 이상인 경우에만 터치로 인정)
        NPCImage.alphaHitTestMinimumThreshold = 0.1f;
        NPCImage.raycastTarget = false;

        // Pull 행동에 대한 Event List 표시
        pullEvents.transform.Find(npcName).gameObject.SetActive(true);
        pullEvents.SetActive(true);

        // Look 행동에 대한 Event List 제거
        lookEvents.SetActive(false);

        touchMode = PullMode.pullMode;
    }

    // Displayer 우측 버튼 중 Look 버튼 클릭
    public void OnClickLookBtn()
    {
        // 이미지 터치 기준 알파값 지정(터치 지점 알파값이 0.1f 이상인 경우에만 터치로 인정)
        NPCImage.alphaHitTestMinimumThreshold = 0f;
        NPCImage.raycastTarget = true;

        // Look 행동에 대한 Event List 표시
        lookEvents.transform.Find(npcName).gameObject.SetActive(true);
        lookEvents.SetActive(true);

        // Pull 행동에 대한 Event List 제거
        pullEvents.SetActive(false);
        
        touchMode = LookMode.lookMode;
    }

    // Displayer 우측 버튼 중 Pincer 버튼 클릭
    public void OnClickPincerBtn()
    {
        // 이미지 터치 기준 알파값 지정(터치 지점 알파값이 0.1f 이상인 경우에만 터치로 인정)
        NPCImage.alphaHitTestMinimumThreshold = 0.1f;
        NPCImage.raycastTarget = true;

        // Pull 행동에 대한 Event List 제거
        pullEvents.SetActive(false);

        // Look 행동에 대한 Event List 제거
        lookEvents.SetActive(false);

        touchMode = PincerMode.pincerMode;
    }

    // 현재 mode에서의 버튼다운 수행
    public void OnButtonDownNPC(TouchPointScript conTouch)
    {
        touchMode.OnButtonDownNPC(conTouch);
    }

    // 현재 mode에서의 버튼업 수행
    public void OnButtonUpNPC()
    {
        touchMode.OnButtonUpNPC();
    }

    // 현재 mode에서의 버튼드래그 수행
    public void OnDragNPC()
    {
        touchMode.OnDragNPC();
    }

    // 현재 모드에서 특정 Area 접근
    public void InTouchArea(TouchPointScript conTouch)
    {
        touchMode.InTouchArea(conTouch);
    }

    public void OutTouchArea()
    {
        touchMode.OutTouchArea();
    }

    // Pincer Mode에서 아이템을 얻을 때, 얻은 아이템의 정보를 Displayer 좌측 상단에 전송
    public void GetItem(string itemName, string itemExp, string talkScript, ReactionCollection afterReaction)
    {
        // 기존의 코루틴이 실행중이면 중단
        if (talkScriptCoroutine != null)
            StopCoroutine(itemInfoCoroutine);
        if (talkScriptCoroutine != null)
            StopCoroutine(talkScriptCoroutine);
        
        // 아무런 아이템(문서)를 얻지 못한 경우
        if(itemName == "")
        {

        }
        // 정보를 얻은 경우
        else if (itemName == "기 록")
        {
            // 문서 획득 효과음 출력
            audioSource.clip = audioClipForDocu;
            audioSource.Play();

            // 얻은 문서를 작은 Panel에 담아 화면에 표시
            gettingItemName.text = itemName;
            gettingItemExp.text = itemExp;

            // 문서 획득 창이 꺼질때 발생되는 이벤트를 기록
            this.afterReaction = afterReaction;

            gettingItemPanel.SetActive(true);
        }
        // 아이템을 얻은 경우
        else
        {
            // 아이템 획득 효과음 출력
            audioSource.clip = audioClipForItem;
            audioSource.Play();

            // 얻은 아이템을 정보를 기록
            gettingItemName.text = itemName;
            gettingItemExp.text = itemExp;

            // 아이템 획득 창이 꺼질때 발생되는 이벤트를 기록
            this.afterReaction = afterReaction;

            gettingItemPanel.SetActive(true);
        }

        // 아이템 정보를 Displayer 좌측 상단에 전달하여 표시하는 코루틴 실행
        itemInfoCoroutine = ItemInfoText(itemName, itemExp);
        StartCoroutine(itemInfoCoroutine);

        // NPC 대사를 Displayer 좌측 하단에 전달하여 표시하는 코루틴 실행
        talkScriptCoroutine = TalkScriptText(talkScript);
        StartCoroutine(talkScriptCoroutine);
    }

    // 아이템(문서) 획득 Panel 제거 
    public void OnClickExitOnGettingPanel()
    {
        // 클릭 효과음 출력
        audioSource.clip = audioClipForClick;
        audioSource.Play();

        // Panel 제거
        gettingItemPanel.SetActive(false);
        
        // 만약 after 이벤트가 등록되어 있으면 실행
        if (afterReaction != null)
        {
            afterReaction.InitAndReact();
            afterReaction = null;
        }
    }
    

    // ================================================= private Function ===========================================================

    private void GottenItemTextInit()
    {
        gottenItemNameText.text = "";
        gottenItemExpText.text = "";
    }

    private void ChatTextInit()
    {
        chatText.text = "";
    }

    // NPCNameConverter 함수로 변환된 이름(NPC1_Police 등)을 통해 Resource 폴더의 이미지를 가져온다.
    private void NPCImageInit()
    {
        // 이미지를 가져온다
        Sprite npcSprite = Resources.Load<Sprite>("ArtResource/Illust/" + npcName);
        NPCImage.sprite = npcSprite;
        NPCBigImage.sprite = npcSprite;

        //NPCImage.sprite = null;
    }

    // Scriptable object로 작동하는 Reaction 진행 상황에서 현재 대화하고있는 NPC에 대한 정보를 알 방법이 없음(있으면 누가 좀 알려주셈)
    // 그래서 현재 name(ex. 형사, 꼬맹이아빠 등)을 Gameobject 이름(ex. NPC1_Police 등)으로 변환하여 사용.
    private string NPCNameConverter(string name)
    {
        return NpcNameConverter.Converter(name);
    }

    // 얻은 아이템에 대한 정보를 우측에 표시
    IEnumerator ItemInfoText(string itemName, string itemExp)
    {
        float conTypingTime = 0;
        float maxTypingTime = 0.05f;

        // 기존의 내용을 지운다.
        gottenItemNameText.text = "";
        gottenItemExpText.text = "";

        // 이름 한 글자씩 출력
        for(int i = 0; i < itemName.Length; i++)
        {
            gottenItemNameText.text += itemName[i];

            // 글자 입력속도 제어
            while (conTypingTime < maxTypingTime)
            {
                conTypingTime += Time.deltaTime;
                yield return null;
            }
            conTypingTime = 0;

            yield return null;
        }

        // 설명 한 글자씩 출력
        for (int i = 0; i < itemExp.Length; i++)
        {
            // 특수문자 치환
            char temp = itemExp[i];
            switch (itemExp[i])
            {
                case '/':
                    temp = ',';
                    break;
                case 't':
                    temp = '\n';
                    break;
            }
            gottenItemExpText.text += temp;

            // 글자 입력속도 제어
            while (conTypingTime < maxTypingTime)
            {
                conTypingTime += Time.deltaTime;
                yield return null;
            }
            conTypingTime = 0;

            yield return null;
        }

        yield return null;
    }

    // 아이템 획득에 따른 NPC 대사 처리
    IEnumerator TalkScriptText(string talkScript)
    {
        float conTypingTime = 0;
        float maxTypingTime = 0.05f;

        // 기존의 내용을 지운다.
        chatText.text = "";

        // 새로 들어온 대사를 한 글자씩 표시
        for (int i = 0; i < talkScript.Length; i++)
        {
            // 특수문자 치환
            char temp = talkScript[i];
            switch (talkScript[i])
            {
                case '/':
                    temp = ',';
                    break;
                case 't':
                    temp = '\n';
                    break;
            }
            chatText.text += temp;

            // 글자 입력속도 제어
            while (conTypingTime < maxTypingTime)
            {
                conTypingTime += Time.deltaTime;
                yield return null;
            }
            conTypingTime = 0;

            yield return null;
        }
        yield return null;
    }
}
