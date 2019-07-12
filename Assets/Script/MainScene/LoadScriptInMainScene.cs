using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LoadScriptInMainScene : MonoBehaviour {

    // 메인 canvas
    public GameObject loadDisplayer;
    // 최종 선택 canvas
    public GameObject loadConfirmationDisplayer;
    // 불러올 데이터가 없는 slot을 불러오기 클릭할 때 나오는 canvas
    public GameObject loadCantDisplayer;

    public AudioSource effect;
    public AudioClip click;
    
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
            if (File.Exists(Application.persistentDataPath + "SlotData" + i + ".json"))
            {
                string jsonString = File.ReadAllText(Application.persistentDataPath + "SlotData" + i + ".json");
                SlotData tempData = JsonUtility.FromJson<SlotData>(jsonString);

                slots[i].Init(true, tempData.time, tempData.stage, tempData.mission);
            }
        }
    }

    public void OnClickClose()
    {
        effect.clip = click;
        effect.Play();

        loadDisplayer.SetActive(false);
    }

    public void OnClickSlot(int index)
    {
        effect.clip = click;
        effect.Play();

        if (conClick != -1)
            slots[conClick].GetComponent<Image>().sprite = deActivatedBG;
        slots[index].GetComponent<Image>().sprite = activatedBG;

        conClick = index;
        
        if (slots[index].isUsing)
            loadConfirmationDisplayer.SetActive(true);
        else
            loadCantDisplayer.SetActive(true);
    }

    public void OnClickYes()
    {
        StaticInfoForSound.playingSlotIndex = conClick;
        loadConfirmationDisplayer.SetActive(false);
        loadDisplayer.SetActive(false);

        manager.LoadGame(90);
    }

    public void OnClickNo()
    {
        effect.clip = click;
        effect.Play();

        slots[conClick].GetComponent<Image>().sprite = deActivatedBG;
        conClick = -1;

        loadConfirmationDisplayer.SetActive(false);
    }

    public void OnClickOK()
    {
        effect.clip = click;
        effect.Play();

        slots[conClick].GetComponent<Image>().sprite = deActivatedBG;
        conClick = -1;

        loadCantDisplayer.SetActive(false);
    }
}
