using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDocuInfoReaction : DelayedReaction
{

    private DocumentManager manager;

    // 이 리액션으로 얻게되는 캐릭터 info 이름
    public string informationName;

    protected override void ImmediateReaction()
    {
        manager = GameObject.Find("DocumentDisplayer").GetComponent<DocumentManager>();
        // 넘어가는 문서 정보에 대한 데이터를 slot에 추가
        manager.SetNewSlot(informationName);
    }
}
