using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingUntilClickInventoryReaction : DelayedReaction {
    private GameObject myCorotine;

    public ReactionCollection afterCheckReaction;
    public ReactionCollection CantCheckReaction;

    public Button button;
    private bool isItemClick = false;

    protected override void ImmediateReaction()
    {
        FSLocator.textDisplayer.reactionButton.enabled = false;

        myCorotine = CoroutineHandler.Start_Coroutine(Wating()).gameObject;
    }

    IEnumerator Wating()
    {
        GameObject itemScript = FindObjectOfType<ItemScript>().transform.Find("ReactionButton").gameObject;

        // 아이템 정보를 클릭하였는지 확인하는 리스너 추가
        button.onClick.AddListener(ClickItem);

        // 인벤토리를 열 때 까지 대기
        while (itemScript.activeSelf == false)
            yield return null;

        // 인벤토리를 끌 때 까지 대기
        while (itemScript.activeSelf == true)
            yield return null;


        // 인베토리 속 물건을 확인하였는가?
        if (isItemClick)
            afterCheckReaction.InitAndReact();
        else
            CantCheckReaction.InitAndReact();

        FSLocator.textDisplayer.reactionButton.enabled = true;
        Destroy(myCorotine);
    }


    void ClickItem()
    {
        isItemClick = true;

        button.onClick.RemoveListener(ClickItem);
    }
}
