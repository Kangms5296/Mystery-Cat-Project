using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour {

    [Header("Displayer")]
    // 이전으로 돌아갈 canvas
    public GameObject settingDisplayer;
    // 메인 canvas
    public GameObject saveDisplayer;
    // 저장 / 불러오기 선택 canvas
    public GameObject saveConfirmationDisplayer;
    // 최종 선택 canvas
    public GameObject saveReConfirmationDisplayer;

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
        isSave = true;
        saveReConfirmationDisplayer.SetActive(true);
        saveConfirmationDisplayer.SetActive(false);
    }

    public void OnClickCallingUp()
    {
        isSave = false;
        saveReConfirmationDisplayer.SetActive(true);
        saveConfirmationDisplayer.SetActive(false);
    }

    public void OnClickYes()
    {
        // 현재 상태 저장
        if(isSave)
        {
            SaveDataSystem saveData = Resources.Load<SaveDataSystem>("SaveData/Slot" + conClick);
            saveData.SaveData();

            slots[conClick].Init(saveData.isUsing, saveData.time, saveData.stage, saveData.mission);
        }
        // 지정 slot으로 게임 데이터 불러오기
        else
        {

        }

        saveReConfirmationDisplayer.SetActive(false);
    }

    public void OnClickNo()
    {
        slots[conClick].GetComponent<Image>().sprite = deActivatedBG;
        conClick = -1;

        saveReConfirmationDisplayer.SetActive(false);
    }

    public void Test()
    {
        Debug.Log("sdfdsafdsaf");
    }
}
