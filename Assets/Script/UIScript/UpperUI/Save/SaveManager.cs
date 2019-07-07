using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour {

    public ReactionCollection loadReaction;

    [Header("Displayer")]
    // 이전으로 돌아갈 canvas
    public GameObject settingDisplayer;
    // 메인 canvas
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
            SaveDataSystem saveData = Resources.Load<SaveDataSystem>("SaveData/Slot" + i);
            slots[i].Init(saveData.isUsing, saveData.time, saveData.stage, saveData.mission);
        }
		
	}
	

    public void OnClickIcon()
    {
        settingDisplayer.SetActive(false);
        saveDisplayer.SetActive(true);

        if (conClick != -1)
        {
            slots[conClick].GetComponent<Image>().sprite = deActivatedBG;
            conClick = -1;
        }
        
    }

    public void OnClickExit()
    {
        saveDisplayer.SetActive(false);
    }

    public void OnClickBefore()
    {
        settingDisplayer.SetActive(true);
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
            saveCnatDisplater.SetActive(true);
            saveConfirmationDisplayer.SetActive(false);
        }
    }

    public void OnClickCallingUp()
    {
        if (FindObjectOfType<MainGame>().isCanSave)
        {
            isSave = false;
            saveReConfirmationDisplayer.SetActive(true);
            saveConfirmationDisplayer.SetActive(false);
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
            SaveDataSystem saveData = Resources.Load<SaveDataSystem>("SaveData/Slot" + conClick);
            saveData.SaveData();

            slots[conClick].Init(saveData.isUsing, saveData.time, saveData.stage, saveData.mission);
        }
        // 지정 slot으로 게임 데이터 불러오기
        else
        {
            SaveDataSystem saveData = Resources.Load<SaveDataSystem>("SaveData/Slot" + conClick);
            if(saveData.isUsing)
            {
                StaticInfoForSound.playingSlotIndex = conClick;
                loadReaction.InitAndReact();
                saveDisplayer.SetActive(false);
            }
            else
            {
                LoadCantDisplayer.SetActive(true);
            }
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
        LoadCantDisplayer.SetActive(false);
    }
}
