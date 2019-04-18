using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICaching : MonoBehaviour {

    // 상단에 존재하는 모든 UI들(tag가 UI로 되어있다.)
    private GameObject[] UI;


	// Use this for initialization
	void Awake () {
        UI = GameObject.FindGameObjectsWithTag("UI");
	}

    public GameObject[] GetUI()
    {
        return UI;
    }
	
}
