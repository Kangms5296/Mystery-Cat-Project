using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NewPlayer : NewCharacter
{

    // 이동할 Vector
    private Vector2 moveVector;

    // 코루틴 목록 - 솔직히 지금은 이동밖에 코루틴을 사용하지 않아서 굳이 이래야 하나 싶기도 하다..
    // 만약 특정 이벤트에서 이 객체에 코루틴을 작용하면, 기존에 작동되는 코루틴과 충돌이 일어날 수 있으므로,
    // 기존에 작동되는(평상시에 작동되는) 모든 코루틴을 관리할 수 있어야 한다.
    private List<IEnumerator> coroutineList;

    // =========================================== 공개 메서드 ============================================

    // Use this for initialization
    // Start() 함수에 작성할 내용을 이 함수에 작성할 것!!
    // Start() 함수 작성 금지!!
    protected override void TemplateStart()
    {
        isMoving = false;
        moveVector = Vector2.zero;

        coroutineList = new List<IEnumerator>();

        // 앞으로 추가되는 코루틴을 여기서 추가

        // 코루틴 시작
        CoroutineListStart();
    }


    // 외부에서 (아마 대부분 조이스틱 스크립트) 플레이어로 전달하는 이동 정보에 대한 함수
    public void Move(Vector3 moveVector)
    {
        // 이동 방향으로의 벡터 지정
        this.moveVector = moveVector;

        // 이동 시작
        isMoving = true;

        // 방향에 따른 애니메이션 변환
        ChangeAnimation(moveVector);
    }

	public void MoveReverse(Vector2 touchPoint)
	{
		// 이동 목표 좌표를 터치 좌표로 지정
		moveVector = touchPoint;
		// 이동 목표 좌표까지 이동할 속도 지정
		// 이동 시작
		isMoving = true;

		// 이동 애니메이션 수행
		ReverseChangeAnimation();
	}

    // 외부에서 (아마 대부분 조이스틱 스크립트) 플레이어로 전달하는 정지 정보에 대한 함수
    public void Stop()
    {
        // 이동 정지
        isMoving = false;

        // 정지 벡터(0, 0, 0)를 캐틱터로 전송
        moveVector = Vector3.zero;
        MoveToVector(moveVector);

        // 애니메이션 정지
        ChangeAnimation(new Vector2(0, 0));
    }


    // =========================================== 비공개 메서드 ============================================
   
    // 기존에 작동하는 모든 코루틴을 다시 start
    protected override void CoroutineListStart()
    {
        foreach (IEnumerator e in coroutineList)
        {
            StartCoroutine(e);

        }
    }


    // 기존에 작동하는 모든 코루틴을 stop
    protected override void CoroutineListStop()
    {
        foreach (IEnumerator e in coroutineList)
        {
            StopCoroutine(e);
        }
    }


    // 이동 코루틴
    private IEnumerator Moving()
    {
        while (true)
        {
            // 만약 이동 중이면..
            if (isMoving)
            {
                // 방향으로 이동
                MoveToVector(moveVector);
            }
            yield return null;
        }
    }


    // 이전에 작용되고 있던 애니메이션을 표시 - 같은 애니메이션이 계속 호출 되는것을 방지, 4방향의 Idle 중 어느 방향을 호출할 지 선택
    private enum BEHAVIOR_MODE { IDLE, MOVE_TOPLEFT, MOVE_TOPRIGHT, MOVE_BOTLEFT, MOVE_BOTRIGHT };
    private BEHAVIOR_MODE beforeDir = BEHAVIOR_MODE.IDLE;

    // 현재 상황에 맞는 애니메이션을 재생합니다.
    protected override void ChangeAnimation(Vector2 moveVector)
    {
        // 이동 애니메이션
        if (isMoving)
        {
            // 오른쪽
            if (moveVector.x > 0)
            {
                // 위
                if (moveVector.y > 0)
                {
                    if (beforeDir != BEHAVIOR_MODE.MOVE_TOPRIGHT)
                    {
                        // 이전에 발동된 Trigger을 해제
                        anim.ResetTrigger("TopLeft");
                        anim.ResetTrigger("BotLeft");
                        anim.ResetTrigger("BotRight");

                        // 새로운 이동 Trigger 지정
                        anim.SetTrigger("TopRight");

                        beforeDir = BEHAVIOR_MODE.MOVE_TOPRIGHT;
                    }
                }
                // 아래
                else
                {
                    if (beforeDir != BEHAVIOR_MODE.MOVE_BOTRIGHT)
                    {
                        // 이전에 발동된 Trigger을 해제
                        anim.ResetTrigger("TopLeft");
                        anim.ResetTrigger("BotLeft");
                        anim.ResetTrigger("TopRight");

                        // 새로운 이동 Trigger 지정
                        anim.SetTrigger("BotRight");

                        beforeDir = BEHAVIOR_MODE.MOVE_BOTRIGHT;
                    }
                }
            }
            // 왼쪽
            else
            {
                // 위
                if (moveVector.y > 0)
                {
                    if (beforeDir != BEHAVIOR_MODE.MOVE_TOPLEFT)
                    {
                        // 이전에 발동된 Trigger을 해제
                        anim.ResetTrigger("TopRight");
                        anim.ResetTrigger("BotLeft");
                        anim.ResetTrigger("BotRight");

                        // 새로운 이동 Trigger 지정
                        anim.SetTrigger("TopLeft");

                        beforeDir = BEHAVIOR_MODE.MOVE_TOPLEFT;
                    }
                }
                // 아래
                else
                {
                    if (beforeDir != BEHAVIOR_MODE.MOVE_BOTLEFT)
                    {
                        // 이전에 발동된 Trigger을 해제
                        anim.ResetTrigger("TopLeft");
                        anim.ResetTrigger("BotRight");
                        anim.ResetTrigger("BotRight");

                        // 새로운 이동 Trigger 지정
                        anim.SetTrigger("BotLeft");

                        beforeDir = BEHAVIOR_MODE.MOVE_BOTLEFT;
                    }
                }
            }
        }
        // 정지 애니메이션
        else
        {
            switch (beforeDir)
            {
                case BEHAVIOR_MODE.MOVE_BOTLEFT:
                    anim.SetTrigger("Idle_BotLeft");
                    break;
                case BEHAVIOR_MODE.MOVE_BOTRIGHT:
                    anim.SetTrigger("Idle_BotRight");
                    break;
                case BEHAVIOR_MODE.MOVE_TOPLEFT:
                    anim.SetTrigger("Idle_TopLeft");
                    break;
                case BEHAVIOR_MODE.MOVE_TOPRIGHT:
                    anim.SetTrigger("Idle_TopRight");
                    break;
            }
            beforeDir = BEHAVIOR_MODE.IDLE;
        }
    }

	private void ReverseChangeAnimation()
	{
		// 터치 후 이동중이라면..
		if (isMoving)
		{
			// 클릭 좌표가 플레이어보다 왼쪽에 있다.
			if(trans.position.x > moveVector.x)
			{
				// 클릭 좌표가 플레이어보다 아래에 있다.
				if (trans.position.y > moveVector.y && !(beforeDir == BEHAVIOR_MODE.MOVE_BOTLEFT))
				{
					anim.SetTrigger("TopRight");

					beforeDir = BEHAVIOR_MODE.MOVE_TOPRIGHT;
				}
				// 클릭 좌표가 플레이어보다 위에 있다.
				else if(trans.position.y <= moveVector.y && !(beforeDir == BEHAVIOR_MODE.MOVE_TOPLEFT))
				{
					anim.SetTrigger("BotRight");

					beforeDir = BEHAVIOR_MODE.MOVE_BOTRIGHT;
				}
			}
			// 클릭 좌표가 플레이어보다 오른쪽에 있다.
			else
			{
				// 클릭 좌표가 플레이어보다 아래에 있다.
				if (trans.position.y > moveVector.y && !(beforeDir == BEHAVIOR_MODE.MOVE_BOTRIGHT))
				{
					anim.SetTrigger("TopLeft");

					beforeDir = BEHAVIOR_MODE.MOVE_TOPLEFT;
				}
				// 클릭 좌표가 플레이어보다 위에 있다.
				else if(trans.position.y <= moveVector.y && !(beforeDir == BEHAVIOR_MODE.MOVE_TOPRIGHT))
				{
					anim.SetTrigger("BotLeft");

					beforeDir = BEHAVIOR_MODE.MOVE_BOTLEFT;
				}
			}
		}
		// 정지한 상태이면..
		else
		{
			// 이동하던 방향에 따라 캐릭터기 Idle 하며 바라보는 애니메이션 지정
			switch(beforeDir)
			{
			case BEHAVIOR_MODE.MOVE_BOTLEFT:
				anim.SetTrigger("Idle_BotLeft");
				break;
			case BEHAVIOR_MODE.MOVE_BOTRIGHT:
				anim.SetTrigger("Idle_BotRight");
				break;
			case BEHAVIOR_MODE.MOVE_TOPLEFT:
				anim.SetTrigger("Idle_TopLeft");
				break;
			case BEHAVIOR_MODE.MOVE_TOPRIGHT:
				anim.SetTrigger("Idle_TopRight");
				break;
			}
			beforeDir = BEHAVIOR_MODE.IDLE;
		}
	}
}
