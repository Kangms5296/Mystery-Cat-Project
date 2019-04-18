using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveCutSceneReaction : DelayedReaction
{
	public GameObject moveObject;

	public List<Vector3> moveDirList;
	public List<float> moveSpeedList;
	public List<float> moveTimeList;

	GameObject go;

	protected override void SpecificInit()
	{

	}


	protected override void ImmediateReaction()
	{
		go = CoroutineHandler.Start_Coroutine(MoveObject()).gameObject;
	}

	IEnumerator MoveObject()
	{
		float currentTime;
		for(int i = 0; i < moveTimeList.Count; i++)
		{
			currentTime = 0.0f;
			//Debug.Log (moveTimeList [i]);
			while(currentTime <= moveTimeList[i])
			{
				moveObject.transform.localPosition += moveDirList[i] * moveSpeedList [i] * Time.fixedDeltaTime;
				currentTime += Time.fixedDeltaTime;

				yield return new WaitForFixedUpdate();
			}

		}
			
		Destroy(go);
	}
}