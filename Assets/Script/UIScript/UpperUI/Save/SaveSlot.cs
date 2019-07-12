using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour {

    public bool isUsing;

    private Text _time;
    private Text _pos;
    private Text _mission;

    private GameObject _empty;


    public void Init(bool isUsing, string time, Stage stage, string mission)
    {
        _time = transform.Find("Time").GetComponent<Text>();
        _pos = transform.Find("Pos").GetComponent<Text>();
        _mission = transform.Find("Mission").GetComponent<Text>();
        _empty = transform.Find("Empty").gameObject;

        this.isUsing = isUsing;
        if (isUsing)
        {
            _time.text = time;
            _time.gameObject.SetActive(true);
            _pos.text = StageConverter(stage);
            _pos.gameObject.SetActive(true);
            _mission.text = mission;
            _mission.gameObject.SetActive(true);

            _empty.SetActive(false);
        }
        else
        {
            _time.gameObject.SetActive(false);
            _pos.gameObject.SetActive(false);
            _mission.gameObject.SetActive(false);

            _empty.SetActive(true);
        }
    }

    private string StageConverter(Stage stage)
    {
        switch(stage)
        {
            case Stage.Backgarden:
                return "뒤뜰";
            case Stage.Bar:
                return "바";
            case Stage.Home:
                return "옛날 집";
            case Stage.Library_1:
            case Stage.Library_2:
            case Stage.Library_3:
            case Stage.Library1_Tutorial:
            case Stage.Library2_Tutorial:
                return "도서관";
            case Stage.Village:
            case Stage.Village_Tutorial:
                return "광장";
        }
        return "";
    }
}
