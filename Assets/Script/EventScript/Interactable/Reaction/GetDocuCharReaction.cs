using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDocuCharReaction : DelayedReaction
{
    private DocumentManager manager;

    // 이 리액션으로 얻게되는 캐릭터 info 이름
    public string characterName;

    protected override void ImmediateReaction()
    {
        manager = GameObject.Find("DocumentDisplayer").GetComponent<DocumentManager>();

        // 넘어가는 캐릭터 이름과 같은 slot을 해금
        manager.CharacterKnow(characterName);
    }
}
