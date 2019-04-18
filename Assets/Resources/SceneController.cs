using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SceneController : MonoBehaviour {

	private static SceneController instance;
	const string loadPath = "SceneController";

	public static SceneController Instance
	{
		get
		{
			if (!instance)
				instance = FindObjectOfType<SceneController>();
			if (!instance)
				instance = Resources.Load<SceneController>(loadPath);
			if (!instance)
				Debug.Log ("Error");
				
			return instance;
		}
		set { instance = value; }
	}

	public string CurrentScene;

	public string SelectedCutscene;
}
