using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour {

	public GameObject Setting;

	public void OnSetting(){
		Setting.SetActive (true);
	}

	public void OffSetting(){
		Setting.SetActive (false);
	}
	
}