using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickScript : MonoBehaviour
{
    // 플레이어 객체
    private NewCharacter player;

    // ClickArea RectTransform 콤포넌트 캐싱
    private RectTransform rectTrans;

    // ClickArea 초기 위치
    private Vector2 idlePos;
    // ClickArea 최대 이동 길이
    private float maxMove;
    // ClickArea 이동 벡터
    private Vector2 moveVector;

    // 가장 기본이 되는 해상도
    private const float screenWidth = 620;
    private const float screenHeight = 349;

    // 현재 조이스틱이 이동중인지 확인
    public bool isClicked = false;

    // Use this for initialization
    private void Start()
    {
        Input.multiTouchEnabled = false;

        // 플레이어 객체 캐싱
        player = GameObject.Find("Player").GetComponent<NewCharacter>();

        // Rect Component 캐싱
        rectTrans = GetComponent<RectTransform>();


        // 최대 ClickArea 이동 거리 지정
        maxMove = 25.0f;
    }

    private void FixedUpdate()
    {
        Vector3 clickPos;

        // 조이스틱을 클릭 중이면..
        if (isClicked)
        {
            // 기준점
            idlePos = transform.parent.position;
            float tempDistance = maxMove * Screen.width / screenWidth;
            
            // Touch 좌표
            clickPos = Input.mousePosition;

            // 2. 클릭 방향으로 조이스틱을 이동(단, 최대 거리만큼만 이동)
            // 드래그한 위치가 maxMove보다 작다면 ClickArea 객체는 해당 위치에 있는다.
            if (Vector2.Distance(clickPos, idlePos) < tempDistance)
            {
                transform.position = clickPos;
            }
            // 드래그한 위치가 maxMove보다 크다면 ClickArea 객체는 maxMove 범위 내에 해당 방향에 위치한다.
            else
            {
                // 정규화
                Vector2 normalized = new Vector2(clickPos.x - idlePos.x, clickPos.y - idlePos.y);
                normalized.Normalize();

                // 크기가 1로 정규화된 normalized를 최대maxMove 의 크기만큼 변환 
                transform.position = idlePos + normalized * tempDistance;
            }

            // 3. 이동시킨 조이스틱 방향으로 플레이어를 이동
            // 정규화한 벡터의 방향으로 플레이어를 이동
            moveVector = new Vector2(clickPos.x - idlePos.x, clickPos.y - idlePos.y).normalized;
            player.MoveToVector(moveVector);
        }
    }

    // 조이스틱 클릭다운
    public void OnJoystickDown()
    {
        // 이동 시작
        isClicked = true;
    }

    // 조이스틱 클릭업
    public void OnJoystickUp()
    {
        // 이동 중지
        isClicked = false;

        // 플레이어를 정지
        player.Stop();

        // ClickArea의 위치를 다시 중앙으로 이동
        // idlePos는 Transform 위치를 나타내기때문에 RectTransform인 rectTrans에 대입x
        rectTrans.position = transform.parent.position;

        // 현재 이동 방향이 없으므로 moveVector도 0으로 초기화
        moveVector = Vector2.zero;
    }
}
