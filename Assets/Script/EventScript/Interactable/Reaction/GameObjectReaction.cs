using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectReaction : DelayedReaction
{
	public GameObject SetObject;
	public bool check;

	protected override void ImmediateReaction()
	{
		SetObject.SetActive (check);
	}
}
