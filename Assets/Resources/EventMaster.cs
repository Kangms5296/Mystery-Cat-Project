using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SCENE_NAME{ 
	SCENE_01, SCENE_02, SELECT, 
}

[CreateAssetMenu]
public class EventMaster : ScriptableObject {

	private static EventMaster m_instance;
	private const string loadPath = "EventMaster";

	public ReactionCollection currentReactionCollection;

	public SCENE_NAME cutSceneName;

	public static EventMaster Instance{
		get{
			if (!m_instance)
				m_instance = FindObjectOfType<EventMaster>();
			return m_instance;
		}
		set{
			m_instance = value;
		}
	}
}
