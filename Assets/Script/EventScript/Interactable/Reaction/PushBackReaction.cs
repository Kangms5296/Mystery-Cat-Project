using UnityEngine;
using System.Collections;

public class PushBackReaction : DelayedReaction
{
	public Transform pushNpcTransform;
	public float speed;
	public float distance;

	private GameObject myCorotine;
	private NewCharacter player;

	protected override void SpecificInit ()
	{
		player = FindObjectOfType<NewPlayer>();
	}

	protected override void ImmediateReaction()
	{
		FSLocator.textDisplayer.reactionButton.enabled = false;

        /*
		Vector3 pushedVector = player.transform.position - pushNpcTransform.position;
		pushedVector.z = 0;
		Debug.Log ("Push ! : " + pushedVector.normalized);
		Vector3 destination = player.transform.position + pushedVector.normalized * distance;
        */
        // 밀려지는 방향 계산
        Vector2 pushedVector = player.transform.position - pushNpcTransform.position;

        // 밀려지는 방향으로 distance의 거리만큼 밀려남
        player.MoveToPositionSlowlyNotLookingAt (pushedVector, distance);

		myCorotine = CoroutineHandler.Start_Coroutine (CheckMoving()).gameObject;
	}


	IEnumerator CheckMoving()
	{

		while (player.getIsMoving()) {
			yield return null;
		}
        player.Stop();

        FSLocator.textDisplayer.reactionButton.enabled = true;
		FSLocator.textDisplayer.reactionButton.onClick.Invoke ();

		Destroy(myCorotine);
	}
}