using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour {

	private static EventController m_data;

	public static EventController Instance
	{
		get {
			//Debug.Log (loadPath);
			if (!m_data)
				m_data = FindObjectOfType<EventController> ();
			return m_data;
		}
		set { m_data = value; }

	}

	public List<ReactionCollection> reactionCollectionList;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void InvokeEvent()
	{
		reactionCollectionList [0].React ();
	}
}
