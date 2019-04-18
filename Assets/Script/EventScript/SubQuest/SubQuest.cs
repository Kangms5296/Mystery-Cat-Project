using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubQuest : MonoBehaviour {

	public ReactionCollection subQuestReaction;
	public bool isActive;

	public void Start(){
		isActive = false;
	}

	public void SubQuestReaction(){
		subQuestReaction.InitAndReact ();
	}

	public void SubQuestActiveOn(){
		isActive = true;
	}

	public void SubQuestActiveOff(){
		isActive = false;
	}
}
