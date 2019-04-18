using UnityEngine;
using System.Collections;


public class DelayChangeSceneReaction : DelayedReaction
{
    public float delayTime;
    GameObject myCorotine;
    public string sceneName;

    protected override void ImmediateReaction()
    {
        myCorotine = CoroutineHandler.Start_Coroutine(Delay()).gameObject;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delayTime);
       
        FSLocator.changeSceneManager.ChangeScene (sceneName);
        Destroy(myCorotine);
    }
}