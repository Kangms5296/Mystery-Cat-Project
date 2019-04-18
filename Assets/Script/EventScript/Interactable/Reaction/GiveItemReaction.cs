using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveItemReaction : DelayedReaction
{
    private GameObject myCorotine;

    private ItemScript inventory;

    // 대화를 하는 NPC 이름
    public string npcName;

    // 아이템을 성공적으로 주었을 경우의 Collection
    public ReactionCollection GoodReactionCollection;
    // 아이템을 주지 못했을 경우의 Collection
    public ReactionCollection BadReactionCollection;

    protected override void SpecificInit()
    {
        inventory = FindObjectOfType<ItemScript>();
    }

    protected override void ImmediateReaction()
    {
        // 아이템 "주기" 루틴 실행
        myCorotine = CoroutineHandler.Start_Coroutine(CheckForGiving()).gameObject;
    }

    IEnumerator CheckForGiving()
    {
        FSLocator.textDisplayer.HideDialogueHolder();
        FSLocator.characterDisplayer.HideImage();

        // 인벤토리 간 조작 금지 설정
        FSLocator.textDisplayer.reactionButton.gameObject.SetActive(false);

        // "주다" 버튼이 포함되어 있는 인벤토리 UI 표시
        inventory.OnClickGive(npcName);

        // 사용자가 아이템 선택 후 "주다" 버튼 클릭까지 대기
        while (inventory.giveBg.activeSelf)
        {
            yield return null;
        }

        FSLocator.textDisplayer.reactionButton.enabled = true;
        // 아이템을 정상적으로 주었다면..
        if (inventory.isGive)
        {
            GoodReactionCollection.InitAndReact();
            inventory.isGive = false;
        }
        // 아이템을 받지 않았다면..
        else
        {
            BadReactionCollection.InitAndReact();
        }

        // 인벤토리 간 조작 금지 해제
        FSLocator.textDisplayer.reactionButton.gameObject.SetActive(true);

        Destroy(myCorotine);
    }
}
