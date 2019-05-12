using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDistanceCheckReaction : DelayedReaction
{

    private GameObject myCorotine;

    // 이동 거리(조이스틱 클릭 시간으로 판별)
    public float distance;

    // 이동 후 취할 리액션
    public ReactionCollection afterReaction;

    protected override void ImmediateReaction()
    {
        myCorotine = CoroutineHandler.Start_Coroutine(CheckingDistance()).gameObject;



    }

    IEnumerator CheckingDistance()
    {
        // 조이스틱 스크립트(조이스틱 사용 유무 판별)
        JoystickScript joystick = GameObject.FindObjectOfType<JoystickScript>();

        // 현재까지 조이스틱 사용 시간
        float conDistance = 0;

        while (true)
        {
            if(joystick.isClicked)
            {
                conDistance += Time.deltaTime;
                if (conDistance > distance)
                    break;
            }
            yield return null;
        }

        afterReaction.InitAndReact();

        // 이 코루틴 삭제
        Destroy(myCorotine);
    }
}
