using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleScript : MonoBehaviour
{
    // Audio
    private AudioSource paperClickAudioSource;
    private AudioClip paperClickAudioClip;

    // 어떤 단서를 클릭하였는지 저장
    private int clickIndex;

    // 시계바늘 Transform (현재 시계바늘이 가리키는 방향을 확인하기 위해 저장)
    public Transform hourRotate;
    public Transform minuteRotate;

    public ReactionCollection clearReaction;
    public ReactionCollection firstFailedReaction;
    public ReactionCollection secondFailedReaction;


    // 각 단서들이 의미하는 시간 및 선작업 Reaction
    [System.Serializable]
    public class paper
    {
        public int hour;
        public int minute;

        public ReactionCollection beforeReaction;
    }
    public List<paper> papers;

    // 실패 횟수
    private int failedCount = 0;

    private void Start()
    {
        paperClickAudioSource = GameObject.Find("EffectSound_For_Click").GetComponent<AudioSource>();
        paperClickAudioClip = Resources.Load<AudioClip>("AudioResource/EffectSound/S_Click2");

        clickIndex = -1;
    }


    // ============================================================================== public functuin ===============================================================================

    public void OnClickPaper(int index)
    {
        clickIndex = index;

        paperClickAudioSource.clip = paperClickAudioClip;
        paperClickAudioSource.Play();

        papers[clickIndex].beforeReaction.InitAndReact();
    }

    public void RotateClock()
    {
        StartCoroutine(RotatingClock());
    }

    public void ClearPuzzle()
    {
        clearReaction.InitAndReact();
    }

    public void FailedPuzzle()
    {
        failedCount++;

        switch(failedCount)
        {
            case 1:
                firstFailedReaction.InitAndReact();
                break;
            case 2:
                failedCount = 0;
                secondFailedReaction.InitAndReact();
                break;
        }
     }


    // ============================================================================== private functuin ===============================================================================


    private IEnumerator RotatingClock()
    {
        // 현재 시간 시계가 가리키는 시간 확인
        int conHour;
        int conMinute;
        conHour = (int)((hourRotate.rotation.eulerAngles.z % 360) / 30);
        conMinute = (int)((minuteRotate.rotation.eulerAngles.z % 360) / 6);

        // 시계를 돌릴 방향 지장
        float rotateDir;
        if (papers[clickIndex].hour > conHour)
            rotateDir = 5;
        else if (papers[clickIndex].hour < conHour)
            rotateDir = -5;
        else
        {
            if (papers[clickIndex].minute > conMinute)
                rotateDir = 5;
            else
                rotateDir = -5;
        }

        // 시계를 돌림
        while (true)
        {
            conHour = (int)((hourRotate.rotation.eulerAngles.z % 360) / 30);
            conMinute = (int)((minuteRotate.rotation.eulerAngles.z % 360) / 6);
            if (conHour == papers[clickIndex].hour && conMinute == papers[clickIndex].minute)
            {
                break;
            }
            else
            {
                minuteRotate.Rotate(0, 0, rotateDir);
                hourRotate.Rotate(0, 0, rotateDir / 12);
            }
            yield return null;
        }

        // 시간이 맞춰졌으므로 잠시 대기
        yield return new WaitForSeconds(0.5f);

        // 후 작업 진행
        switch(clickIndex)
        {
            case 0:
                FailedPuzzle();
                break;
            case 1:
                FailedPuzzle();
                break;
            case 2:
                ClearPuzzle();
                break;
            case 3:
                FailedPuzzle();
                break;
        }
    }


}