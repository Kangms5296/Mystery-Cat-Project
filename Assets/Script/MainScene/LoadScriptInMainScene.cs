using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScriptInMainScene : MonoBehaviour {

    // 메인 canvas
    public GameObject loadDisplayer;
    // 최종 선택 canvas
    public GameObject loadConfirmationDisplayer;
    // 불러올 데이터가 없는 slot을 불러오기 클릭할 때 나오는 canvas
    public GameObject loadCantDisplayer;

    public AudioSource effect;

    public MainSceneManager manager;

    [Header("Slot Info")]
    public SaveSlot[] slots;
    public Sprite activatedBG;
    public Sprite deActivatedBG;

    private int conClick = -1;
    private bool isSave;



    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            SaveDataSystem saveData = Resources.Load<SaveDataSystem>("SaveData/Slot" + i);
            slots[i].Init(saveData.isUsing, saveData.time, saveData.stage, saveData.mission);
        }

    }

    public void OnClickClose()
    {
        effect.Play();

        loadDisplayer.SetActive(false);
    }

    public void OnClickSlot(int index)
    {
        effect.Play();

        if (conClick != -1)
            slots[conClick].GetComponent<Image>().sprite = deActivatedBG;
        slots[index].GetComponent<Image>().sprite = activatedBG;

        conClick = index;

        SaveDataSystem saveData = Resources.Load<SaveDataSystem>("SaveData/Slot" + index);
        if (saveData.isUsing)
            loadConfirmationDisplayer.SetActive(true);
        else
            loadCantDisplayer.SetActive(true);
    }

    public void OnClickYes()
    {
        effect.Play();
        
        StaticInfoForSound.playingSlotIndex = conClick;
        loadConfirmationDisplayer.SetActive(false);
        loadDisplayer.SetActive(false);

        manager.LoadGame(90);

    }

    public void OnClickNo()
    {
        effect.Play();

        slots[conClick].GetComponent<Image>().sprite = deActivatedBG;
        conClick = -1;

        loadConfirmationDisplayer.SetActive(false);
    }

    public void OnClickOK()
    {
        effect.Play();

        slots[conClick].GetComponent<Image>().sprite = deActivatedBG;
        conClick = -1;

        loadCantDisplayer.SetActive(false);
    }
}
