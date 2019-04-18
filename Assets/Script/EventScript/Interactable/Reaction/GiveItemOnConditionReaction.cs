using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveItemOnConditionReaction : DelayedReaction
{
    private GameObject myCorotine;

    private ItemScript inventory;

    // 대화를 하는 NPC 이름
    public string npcName;

    [System.Serializable]
    public struct Give_Info
    {
        // 주려는 아이템이 이 아이템이면..
        public string itemName;
        // 이 리액션 콜렉션에 담겨있는 내용이 실행되며..
        public ReactionCollection reactionCollection;
    }
    public List<Give_Info> infoList;
    // 이상한 물건을 주었을 경우의 리액션
    public ReactionCollection notGivedReaction;

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
        inventory.isSimpleGive = false;

        // 사용자가 아이템 선택 후 "주다" 버튼 클릭까지 대기
        while (inventory.giveBg.activeSelf)
            yield return null;

        // 인벤토리 간 조작 금지 설정 해제
        FSLocator.textDisplayer.reactionButton.enabled = true;

        // 클릭한 slot 아이템의 이름과 infoList 내의 아이템 이름 중 맞는 게 있다면..
        for (int i = 0;i < infoList.Count; i++)
        {
            if (infoList[i].itemName == inventory.itemName.text)
            {
                // 클릭한 slot의 물건 삭제, condition 체크 수행
                //inventory.Give_OnCondition();
                // 2018-11-10 수정 : 준 아이템에 따른 리액션리스트에서 전달한 아이템을 삭제
                inventory.isGive = true;

                // 해당 아이템을 주었을 때의 reaction 실행
                infoList[i].reactionCollection.InitAndReact();
            }
        }
        
        // 주려는 물건이 지정돤 물건이 아니라면..
        if(inventory.isGive == false)
            notGivedReaction.InitAndReact();

        inventory.isGive = false;  
        inventory.isSimpleGive = true;

        // 인벤토리 간 조작 금지 해제
        FSLocator.textDisplayer.reactionButton.gameObject.SetActive(true);

        Destroy(myCorotine);
    }
}
