using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class TutorialGuideStartInObservationReaction : DelayedReaction
{
    private GameObject myCorotine;
    private bool isNext = false;

    public enum ObservationType { Pull, Look, Pincer }
    public ObservationType type;

    public Button pullButton;
    public Button lookButton;
    public Button pincerButton;
    public Button closeButton;

    public ObservationManager observationManager;

    // 순서대로 Guide UI의 부모가 된다.
    public Transform parent;

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

        switch(type)
        {
            case ObservationType.Pull:
                // Pull 버튼 클릭
                pullButton.onClick.AddListener(OnClickTarget);
                guideArrow.SetTarget(pullButton.transform, parent);
                while (!isNext)
                    yield return null;
                isNext = false;
                pullButton.onClick.RemoveListener(OnClickTarget);

                // Pull 대기
                guideArrow.SetTarget(observationManager.transform, parent);
                guideArrow.GetComponent<Animator>().SetTrigger("Pull");
                while (observationManager.chatText.text == "")
                    yield return null;
                guideArrow.GetComponent<RectTransform>().anchoredPosition = new Vector2(3000, 0);
                guideArrow.GetComponent<Animator>().SetTrigger("Idle");
                break;

            case ObservationType.Look:
                // Look 버튼 클릭
                lookButton.onClick.AddListener(OnClickTarget);
                guideArrow.SetTarget(lookButton.transform, parent);
                while (!isNext)
                    yield return null;
                isNext = false;
                lookButton.onClick.RemoveListener(OnClickTarget);

                // Look 대기
                guideArrow.SetTarget(observationManager.transform, parent);
                guideArrow.GetComponent<Animator>().SetTrigger("Look");
                while (observationManager.chatText.text == "")
                    yield return null;
                guideArrow.GetComponent<RectTransform>().anchoredPosition = new Vector2(3000, 0);
                guideArrow.GetComponent<Animator>().SetTrigger("Idle");
                break;

            case ObservationType.Pincer:
                // Pincer 버튼 클릭
                pincerButton.onClick.AddListener(OnClickTarget);
                guideArrow.SetTarget(pincerButton.transform, parent);
                while (!isNext)
                    yield return null;
                isNext = false;
                pincerButton.onClick.RemoveListener(OnClickTarget);

                // Pincer 대기
                guideArrow.SetTarget(observationManager.transform, parent);
                guideArrow.GetComponent<Animator>().SetTrigger("Pincer");
                while (observationManager.chatText.text == "")
                    yield return null;
                guideArrow.GetComponent<RectTransform>().anchoredPosition = new Vector2(3000, 0);
                guideArrow.GetComponent<Animator>().SetTrigger("Idle");
                break;
        }
        // close 클릭
        closeButton.onClick.AddListener(OnClickTarget);
        guideArrow.SetTarget(closeButton.transform, parent);
        while (!isNext)
            yield return null;
        isNext = false;
        closeButton.onClick.RemoveListener(OnClickTarget);

        guideArrow.EndGuide();

        Destroy(myCorotine);
    }

    void OnClickTarget()
    {
        Debug.Log("Test닷!");
        isNext = true;
    }
}
