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
        ItemScript itemScript = FindObjectOfType<ItemScript>();
        ContentScript content = FindObjectOfType<ContentScript>();

        // 인벤토리를 열 때 까지 대기
        while (true)
        {
            yield return null;
            if (itemScript.isOpened == true)
            {
                break;
            }
        }

        // 인벤토리를 끌 때 까지 대기
        while (true)
        {
            yield return null;
            if (itemScript.isOpened == false)
            {
                break;
            }
        }

        // 조합을 성공하였는가?
        if (content.IsGottenItem(itemName))
            makeReaction.InitAndReact();
        else
            CantReaction.InitAndReact();

        FSLocator.textDisplayer.reactionButton.enabled = true;
    
        Destroy(myCorotine);
    }
}