using System.Collections;
using UnityEngine;

public class ObservationReaction : DelayedReaction
{
    // 현재 대화중인 NPC 이름
    public string NpcName = "";

    // 관찰 기능 중 사용을 막을 기능들을 표시(특수한 경우)
    public bool disabledPull;
    public bool disabledLook;
    public bool disabledPincer;


    // 현재 관찰 UI를 실행 중인지 확인할 코루틴
    private GameObject myCorotine;

    // 관찰 UI
    private ObservationManager observationManager;



    protected override void SpecificInit()
    {
        observationManager = FindObjectOfType<ObservationManager>();
    }


    protected override void ImmediateReaction()
    {
        // 관찰 간 조작 금지 설정
        FSLocator.textDisplayer.reactionButton.gameObject.SetActive(false);

        // 관찰 루틴 실행
        myCorotine = CoroutineHandler.Start_Coroutine(CheckForDoingObservation()).gameObject;
    }

    IEnumerator CheckForDoingObservation()
    {
        // UI 초기 설정 및 화면 표시
        observationManager.Init(NpcName, disabledPull, disabledLook, disabledPincer);

        // 사용자가 UI를 종료하기까지 대기
        while (observationManager.Displayer.activeSelf == true)
            yield return null;

        // 관찰 간 조작 금지 해제
        FSLocator.textDisplayer.reactionButton.gameObject.SetActive(true);
        FSLocator.textDisplayer.reactionButton.onClick.Invoke();
        Destroy(myCorotine);
    }
}
