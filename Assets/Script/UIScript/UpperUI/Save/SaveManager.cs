using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour {

    public GameObject reactionButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickSave()
    {
        reactionButton.SetActive(true);
    }

    public void OnClickExit()
    {
        reactionButton.SetActive(false);
    }


}
