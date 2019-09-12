using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TutorialGuideStartInMixReaction : DelayedReaction
{
    private GameObject myCorotine;
    private bool isNext = false;

    public Button inventoryIcon;
    public Button mixIcon;
    public Button mixReIcon;
    public Button closeIcon;

    public ItemScript itemScript;
    public ContentScript contentScript;

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

        // 인벤토리 아이콘 클릭
        inventoryIcon.onClick.AddListener(OnClickTarget);
        guideArrow.SetTarget(inventoryIcon.transform, parents[0]);
        while (!isNext)
            yield return null;
        isNext = false;
        inventoryIcon.onClick.RemoveListener(OnClickTarget);

        // 외부조합버튼 아이콘 클릭 대기
        mixIcon.onClick.AddListener(OnClickTarget);
        guideArrow.SetTarget(mixIcon.transform, parents[1]);
        while (!isNext)
            yield return null;
        isNext = false;
        mixIcon.onClick.RemoveListener(OnClickTarget);

        // 드래그 대기
        guideArrow.SetTarget(contentScript.transform, parents[2]);
        guideArrow.GetComponent<Animator>().SetTrigger("MixDrag");
        while (contentScript.GetConSlot_material() != 2)
            yield return null;
        guideArrow.GetComponent<RectTransform>().anchoredPosition = new Vector2(3000, 0);
        guideArrow.GetComponent<Animator>().SetTrigger("Idle");

        // 내부조합버튼 아이콘 클릭 대기
        mixReIcon.onClick.AddListener(OnClickTarget);
        guideArrow.SetTarget(mixReIcon.transform, parents[3]);
        while (!isNext)
            yield return null;
        isNext = false;
        mixReIcon.onClick.RemoveListener(OnClickTarget);

        // X 클릭 대기
        closeIcon.onClick.AddListener(OnClickTarget);
        guideArrow.SetTarget(closeIcon.transform, parents[4]);
        while (!isNext)
            yield return null;
        isNext = false;
        closeIcon.onClick.RemoveListener(OnClickTarget);

        guideArrow.EndGuide();

        Destroy(myCorotine);
    }

    void OnClickTarget()
    {
        isNext = true;
    }
}