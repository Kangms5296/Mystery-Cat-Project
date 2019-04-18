using UnityEngine;
using System.Collections;


public class DelayReaction : DelayedReaction
{
    public float delayTime;
	GameObject myCorotine;

    protected override void ImmediateReaction()
    {
        if (FSLocator.textDisplayer != null)
		    FSLocator.textDisplayer.reactionButton.enabled = false;

		myCorotine = CoroutineHandler.Start_Coroutine(Delay()).gameObject;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delayTime);
        if (FSLocator.textDisplayer != null)
		    FSLocator.textDisplayer.reactionButton.enabled = true;
		    FSLocator.textDisplayer.reactionButton.onClick.Invoke ();

		Destroy(myCorotine);
    }
}