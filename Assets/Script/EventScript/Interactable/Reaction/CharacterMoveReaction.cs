using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterMoveReaction : DelayedReaction
{
    [System.Serializable]
    public struct Move_Info
    {
        public NewCharacter character;
        public Transform destination;
        public float speed;
    }
    public List<Move_Info> move_Info;

	private GameObject myCorotine;


	protected override void ImmediateReaction()
    {
        FSLocator.textDisplayer.reactionButton.enabled = false;

        myCorotine = CoroutineHandler.Start_Coroutine(CheckAllMoving()).gameObject;
    }

    IEnumerator CheckAllMoving()
    {
        bool isAllMoved = false;

        // 각 캐릭터들을 이동을 시작한다.
        for (int i = 0; i < move_Info.Count; i++)
        {
            move_Info[i].character.SetSpeed(move_Info[i].speed);
            move_Info[i].character.MoveToPositionSlowlyByLookingAt(move_Info[i].destination.position);
        }
           

        while (!isAllMoved)
        {
            // 모든 캐릭터가 이동이 끝났으면 종료를 하되..
            isAllMoved = true;

            // 각 캐릭터들의 이동이 끝났는지 확인해서..
            for (int i = 0; i < move_Info.Count; i++)
            {
                // 한 캐릭터라도 이동이 끝나지 않았으면..
                if (move_Info[i].character.getIsMoving() == true)
                    // 계속 무한루프를 돌도록 처리
                    isAllMoved = false;
                else
                    // 먼저 이동이 끝난놈은 Stop
                    move_Info[i].character.Stop();

                yield return null;
            }
        }

        // 각 캐릭터들을 정지시킨다.
        for (int i = 0; i < move_Info.Count; i++)
            move_Info[i].character.MakeSpeedBack();

        FSLocator.textDisplayer.reactionButton.enabled = true;
        FSLocator.textDisplayer.reactionButton.onClick.Invoke();

        Destroy(myCorotine);
    }
}