using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialGuideStartReaction : DelayedReaction
{
    private GameObject myCorotine;
    private bool isNext = false;

    // 순서대로 이놈들을 가리킨다.
    public Button[] target;

    // 순서대로 Guide UI의 부모가 된다.
    public Transform[] parents;

    // 가리키는 오브젝트
    public GuideArrowScript guideArrow;





    protected override void ImmediateReaction()
    {
        myCorotine = CoroutineHandler.Start_Coroutine(Wating()).gameObject;
    }
    
    IEnumerator Wating()
    {
        while (myCorotine == null)
            yield return null;

        guideArrow.StartGuide(myCorotine);

        while (guideArrow.index < target.Length)
        {
            // 다음 버튼을 가리킨다.
            target[guideArrow.index].onClick.AddListener(OnClickTarget);
            guideArrow.SetTarget(target[guideArrow.index].transform, parents[guideArrow.index]);

            // 버튼을 누를떄까지 대기
            while (!isNext)
                yield return null;
            isNext = false;

            guideArrow.index++;
        }

        guideArrow.EndGuide();

        Destroy(myCorotine);
    }

    void OnClickTarget()
    {
        target[guideArrow.index].onClick.RemoveListener(OnClickTarget);
        isNext = true;
    }
}
