using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatStoryManager : MonoBehaviour {

	public GameObject TouchPannel;
	public GameObject CutScenePannel;
	public GameObject Next;
	public GameObject Skip;
	public GameObject BlackPaneel;
	public GameObject Before;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OffPannel(){
		TouchPannel.SetActive (false);
		CutScenePannel.SetActive (false);
		Next.SetActive (false);
		Skip.SetActive (false);
		Before.SetActive (false);
	}
}
