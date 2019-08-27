using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGuideAbortReaction : DelayedReaction
{
    // 가리키는 오브젝트
    public GuideArrowScript guideArrow;

    protected override void ImmediateReaction()
    {
        guideArrow.AbortGuide();

    }
}

