using UnityEngine;
using System.Collections.Generic;

public class ShowCutScenePannel : DelayedReaction
{
	public List<GameObject> SetActiveTrueObject;

	protected override void SpecificInit()
	{

	}


	protected override void ImmediateReaction()
	{
		for (int i = 0; i < SetActiveTrueObject.Count; i++) {
			SetActiveTrueObject [i].SetActive (true);
		}

		//TutorialController.Instance.cutScenePannel.gameObject.SetActive (true);
	}
}