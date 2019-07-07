using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingUntilMixReaction : DelayedReaction
{
    private GameObject myCorotine;

    public string itemName;

    public ReactionCollection makeReaction;
    public ReactionCollection CantReaction;

    protected override void ImmediateReaction()
    {
        FSLocator.textDisplayer.reactionButton.enabled = false;

        myCorotine = CoroutineHandler.Start_Coroutine(Wating()).gameObject;
    }

    IEnumerator Wating()
    {
        GameObject itemScript = FindObjectOfType<ItemScript>().transform.Find("ReactionButton").gameObject;
        ContentScript content = FindObjectOfType<ContentScript>();

        // 인벤토리를 열 때 까지 대기
        while (itemScript.activeSelf == false)
            yield return null;

        // 인벤토리를 끌 때 까지 대기
        while (itemScript.activeSelf == true)
            yield return null;

        // 조합을 성공하였는가?
        if (content.IsGottenItem(itemName))
            makeReaction.InitAndReact();
        else
            CantReaction.InitAndReact();

        FSLocator.textDisplayer.reactionButton.enabled = true;
    
        Destroy(myCorotine);
    }
}