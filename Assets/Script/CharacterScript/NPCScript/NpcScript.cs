using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcScript : CharacterScript {

    // NPC가 가지는 행동 패턴
    // IDLE : 현재 위치에서 대기한다.
    // IDLE_MOVE : 초기 위치에서 지정된 거리만큼 주위를 서성거린다.
    // EVENT_MOVE : 특정 위치로 이동하며, 이벤트가 끝나기 전에는 IDLE_MOVE가 작동되지 않는다. 
    private enum BEHAVIOR_MODE {IDLE, IDLE_MOVE, EVENT_MOVE};
    private BEHAVIOR_MODE mode;

    // Idle 중 가만히 있는 시간
    public float restTime = 0;


    // NPC의 초기 위치
    public Vector2 firstPosition;
    // Idle_Move 중 배회하는 거리(초기 위치를 기반으로 해당 좌표만큼 이동)
    public List<Vector2> moveDistance;
    // moveDistance List를 순회할 Index 변수
    private int moveDistanceIndex = 0;

    // 이동속도
   // private float speed = 1.5f;

    // ******************************* 메서드 정의 **********************************





    // Use this for initialization
    // CharacterSript를 상속하는 모든 클래스가 아닌, PlayerScript에서 필요한 Start 본문을 템플릿 메서드 패턴을 이용하여 작성합니다.
    public override void TemplateStart()
    {
        // 씬 시작에서의 객체 초기 위치 캐싱
        firstPosition = trans.position;

        // NPC Idle 코루틴 시작
        //StartCoroutine(Idle_Move());
    }

    // 해당 좌표로 이동하는 함수
    public override void Move(Vector2 movePoint, float speed)
    {
		Debug.Log ("NPC MOVE1");
		this.speed = speed;
        // 해당 좌표로 해당 속도를 가지고 이동
		isMoving = true;
        //trans.position = Vector3.MoveTowards(trans.position, new Vector3(movePoint.x, movePoint.y, movePoint.y), speed* Time.deltaTime);
		StartCoroutine(MoveToDestination(movePoint));
    }



    IEnumerator Idle_Move()
    {
        // 현재 행동모드를 변경
        mode = BEHAVIOR_MODE.IDLE_MOVE;

        // 이번에 서성거리면서 이동할 죄표 계산
        Vector3 destination = new Vector3(firstPosition.x +
            moveDistance[moveDistanceIndex].x, firstPosition.y + moveDistance[moveDistanceIndex].y, firstPosition.y + moveDistance[moveDistanceIndex].y);

        // 목적지에 도착하지 않았다면 계속 이동
        while (!(trans.position.x == destination.x && trans.position.y == destination.y))
        {
            Move(destination, speed);
            yield return null;
        }
        moveDistanceIndex++;
        if (moveDistanceIndex == moveDistance.Count)
            moveDistanceIndex = 0;

        // 대기 코루틴 실행
        StartCoroutine(Idle());
    }

    IEnumerator Idle()
    {
        // 현재 행동모드를 변경
        mode = BEHAVIOR_MODE.IDLE;

        // Idle 애니메이션 실행

        // 애니메이션 상태에서 대기
        yield return new WaitForSeconds(restTime);

        // 다시 서성거려야지..
        StartCoroutine(Idle_Move());
    }

    // 플레이어가 NPC 인지 구역 안으로 진입하면, 해당 NPC는 플레이어를 쳐다봅니다.
    public void NearByPlayer()
    {
        // 만약 NPC가 어떤 이벤트 중간에 플리에어와 가까워진다면, 해당 이벤트를 유지해야 하므로 함수를 진행하지 않습니다.
        // ex) 플레이어와 NPC가 같이 어느 장소로 이동되는 스크립티를 수행중이면, 그 동안 NPC와 플레이어가 충분히 가까워져도 이 함수가 실행되면 안됩니다.
        if(!IsModeEvent())
        {
            // 현재 진행중인 코루틴을 멈춥니다.
            StopAllCoroutines();

            // 플레이어를 본다.
        }
    }

    // 플레이어가 NPC 인지 구역 밖으로 멀어지면, 해당 NPC는 다시 기존의 행동을 이어갑니다.
    public void GetAwayPlayer()
    {
        // 이벤트와 관련되지 않은.. 즉, 평범하게 행동하는 상황이면..
        if (!IsModeEvent())
        {
            // 하던 행동을 마저 수행
            StartCoroutineByMode();
        }
    }

    // 현재 NPC의 mode가 이벤트와 관련된 mode인지 확인
    // 이후 mode의 수가 많아질 수 있으므로 따로 check하는 함수를 구현
    private bool IsModeEvent()
    {
        switch(mode) {
            case BEHAVIOR_MODE.IDLE:
                return false;
            case BEHAVIOR_MODE.IDLE_MOVE:
                return false;
            case BEHAVIOR_MODE.EVENT_MOVE:
                return true;
        }
        // 예외를 던져야겠지..
        return true;
    }

    // 현재 NPC의 mode에 해당하는 코루틴 실행
    // 이후 mode의 수가 많아질 수 있으므로 따로 실행하는 함수를 구현
    private void StartCoroutineByMode()
    {
        switch (mode)
        {
            case BEHAVIOR_MODE.IDLE:
                StartCoroutine(Idle());
                break;
            case BEHAVIOR_MODE.IDLE_MOVE:
                StartCoroutine(Idle_Move());
                break;
            case BEHAVIOR_MODE.EVENT_MOVE:
                
                break;
        }
    }
}
