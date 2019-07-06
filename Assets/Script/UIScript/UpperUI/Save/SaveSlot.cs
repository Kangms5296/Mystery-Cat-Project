using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour {

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
            case Stage.Library1:
            case Stage.Library2:
            case Stage.Library3:
            case Stage.Library1T:
            case Stage.Library2T:
                return "도서관";
            case Stage.Viliage:
            case Stage.ViliageT:
                return "광장";
        }
        return "";
    }
}
