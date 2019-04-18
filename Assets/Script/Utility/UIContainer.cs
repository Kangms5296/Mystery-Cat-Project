using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContainer : MonoBehaviour {

	private List<GameObject> observationList;

	public void Start()
	{
		observationList = new List<GameObject> ();
	}

	public void InsertObservation(GameObject observeUI)
	{
		observationList.Add (observeUI);
	}

	public void DeleteObservation(GameObject observeUI)
	{
		observationList.Remove (observeUI);
	}

	public void HideObservationList(){
		for (int i = 0; i < observationList.Count; i++) {
			if (observationList [i] == null) {
				observationList.RemoveAt (i);
				break;
			}
			observationList [i].SetActive (false);
		}
	}

	public void ShowObservationList(){
        

		for (int i = 0; i < observationList.Count; i++) {
			if (observationList [i] == null) {
				observationList.RemoveAt (i);
				break;
			}
			observationList [i].SetActive (true);
		}
	}
}
