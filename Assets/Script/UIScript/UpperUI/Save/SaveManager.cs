using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveManager : MonoBehaviour {

    public ReactionCollection loadReaction;

    [Header("Displayer")]


    public GameObject reactionButton;

    // 이전으로 돌아갈 canvas
    public GameObject mainDisplayer;
    // 저장 canvas
    public GameObject saveDisplayer;
    // 저장 / 불러오기 선택 canvas
    public GameObject saveConfirmationDisplayer;
    // 최종 선택 canvas
    public GameObject saveReConfirmationDisplayer;
    // 저장 / 불러오기 불가 canvas
    public GameObject saveCnatDisplater;
    // 불러올 데이터가 없는 slot을 불러오기 클릭할 때 나오는 canvas
    public GameObject LoadCantDisplayer;

    [Header("Slot Info")]
    public SaveSlot[] slots;
    public Sprite activatedBG;
    public Sprite deActivatedBG;

    private int conClick = -1;
    private bool isSave;



    // Use this for initialization
    void Start () {
        for(int i = 0; i < slots.Length; i++)
        {
            if (File.Exists(Application.persistentDataPath + "SlotData" + i + ".json"))
            {
                string jsonString = File.ReadAllText(Application.persistentDataPath + "SlotData" + i + ".json");
                SlotData tempData = JsonUtility.FromJson<SlotData>(jsonString);

                slots[i].Init(true, tempData.time, tempData.stage, tempData.mission);
            }
        }
		
	}
	

    public void OnClickIcon()
    {
        mainDisplayer.SetActive(false);
        saveDisplayer.SetActive(true);

        if (conClick != -1)
        {
            slots[conClick].GetComponent<Image>().sprite = deActivatedBG;
            conClick = -1;
        }
        
    }

    public void OnClickClose()
    {
        saveDisplayer.SetActive(false);

        reactionButton.SetActive(false);
    }

    public void OnClickBefore()
    {
        mainDisplayer.SetActive(true);
        saveDisplayer.SetActive(false);
    }

    public void OnClickSlot(int index)
    {
        if(conClick != -1)
            slots[conClick].GetComponent<Image>().sprite = deActivatedBG;
        slots[index].GetComponent<Image>().sprite = activatedBG;

        conClick = index;

        saveConfirmationDisplayer.SetActive(true);
    }

    public void OnClickSave()
    {
        if(FindObjectOfType<MainGame>().isCanSave)
        {
            isSave = true;
            saveReConfirmationDisplayer.SetActive(true);
            saveConfirmationDisplayer.SetActive(false);
        }
        else
        {
            saveConfirmationDisplayer.SetActive(false);

            saveCnatDisplater.SetActive(true);
        }
    }

    public void OnClickLoad()
    {
        if (FindObjectOfType<MainGame>().isCanSave)
        {
            // 저장된 데이터가 있는 Slot을 Load 하는 경우
            if (slots[conClick].isUsing)
            {
                saveConfirmationDisplayer.SetActive(false);

                isSave = false;
                saveReConfirmationDisplayer.SetActive(true);
            }
            // 저장된 데이터가 없는 Slot을 Load 하는 경우
            else
            {
                saveConfirmationDisplayer.SetActive(false);

                LoadCantDisplayer.SetActive(true);
            }
        }
        else
        {
            saveCnatDisplater.SetActive(true);
            saveConfirmationDisplayer.SetActive(false);
        }
    }

    public void OnClickYes()
    {
        saveReConfirmationDisplayer.SetActive(false);

        // 현재 상태 저장
        if (isSave)
        {
            SaveDataSystem save = FindObjectOfType<SaveDataSystem>();

            // 저장
            save.SaveData(conClick);

            // slot 표시 변경
            slots[conClick].Init(true, save.tempData.time, save.tempData.stage, save.tempData.mission);
        }
        // 지정 slot으로 게임 데이터 불러오기
        else
        {
            // 재시작 후 Load 할 데이터 지정
            StaticInfoForSound.playingSlotIndex = conClick;

            // 켜져있는 Canvas 제거
            saveDisplayer.SetActive(false);
            
            // 재시작
            loadReaction.InitAndReact();
        }
    }

    public void OnClickNo()
    {
        slots[conClick].GetComponent<Image>().sprite = deActivatedBG;
        conClick = -1;

        saveReConfirmationDisplayer.SetActive(false);
    }
    
    public void OnClickYesByCantSave()
    {
        saveCnatDisplater.SetActive(false);

    }

    public void OnClickYesByCantLoad()
    {
        slots[conClick].GetComponent<Image>().sprite = deActivatedBG;
        conClick = -1;

        LoadCantDisplayer.SetActive(false);
    }
}
