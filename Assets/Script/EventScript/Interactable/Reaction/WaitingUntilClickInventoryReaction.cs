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
        ItemScript itemScript = GameObject.FindObjectOfType<ItemScript>();
        
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

        FSLocator.textDisplayer.reactionButton.enabled = true;
        FSLocator.textDisplayer.reactionButton.onClick.Invoke();

        Destroy(myCorotine);
    }
}
