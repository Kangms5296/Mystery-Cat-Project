using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDisplayChangeReaction : DelayedReaction
{
    public Stage changeStage;

    protected override void ImmediateReaction()
    {
        MainGame temp = FindObjectOfType<MainGame>();

        // 현재 맵을 비활성화
        temp.stageObject.Find(temp.stage.ToString()).gameObject.SetActive(false);
        // 새로운 맵 지정
        FindObjectOfType<MainGame>().stage = changeStage;
        // 새로운 맵을 활성화
        temp.stageObject.Find(temp.stage.ToString()).gameObject.SetActive(true);
    }
}
