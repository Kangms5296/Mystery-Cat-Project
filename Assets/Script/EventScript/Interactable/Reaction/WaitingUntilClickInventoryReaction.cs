using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingUntilClickInventoryReaction : DelayedReaction {
    private GameObject myCorotine;
    
    protected override void ImmediateReaction()
    {
        FSLocator.textDisplayer.reactionButton.enabled = false;

        myCorotine = CoroutineHandler.Start_Coroutine(Wating()).gameObject;
    }

    IEnumerator Wating()
    {
        GameObject itemScript = FindObjectOfType<ItemScript>().transform.Find("ReactionButton").gameObject;

        // 인벤토리를 열 때 까지 대기
        while (itemScript.activeSelf == false)
            yield return null;

        // 인벤토리를 끌 때 까지 대기
        while (itemScript.activeSelf == true)
            yield return null;

        FSLocator.textDisplayer.reactionButton.enabled = true;
        FSLocator.textDisplayer.reactionButton.onClick.Invoke();

        Destroy(myCorotine);
    }
}
