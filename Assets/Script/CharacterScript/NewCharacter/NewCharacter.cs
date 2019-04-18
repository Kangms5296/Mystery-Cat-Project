using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NewCharacter : MonoBehaviour {

    // 현재 객체의 Transform 콤포넌트
    protected Transform trans;

    // 현재 객체의 Animator 콤포넌트
    protected Animator anim;

    // 현재 객체의 Rigidbody2D 콤포넌트
    protected Rigidbody2D rig2D;

    // 현재 객체가 이동중인가?
    protected bool isMoving = false;

    // 현재 객체츼 이동 속도
    public float speed = 2.5f;

	[SerializeField]
	public float ownSpeed;

	public enum BEHAVIOR_MODE { IDLE, MOVE_TOPLEFT, MOVE_TOPRIGHT, MOVE_BOTLEFT, MOVE_BOTRIGHT };
	public BEHAVIOR_MODE startDir;

    // Use this for initialization
    void Start()
    {
        // 현재 객체의 Transform 콤포넌트 캐싱
        trans = GetComponent<Transform>();
        // 현재 객체의 Animator 콤포넌트 캐싱
        anim = GetComponent<Animator>();
        // 현재 객체의 Rigidbody2D 콤포넌트 캐싱
        rig2D = GetComponent<Rigidbody2D>();

        // CharacterSript를 상속하는 클래스에서 Start() 메서드를 사용하면 위의 콤포넌트 캐싱이 가려진다.
        // CharacterSript를 상속하는 클래스의 Start() 메서드에서 사용할 내용들을 템플릿 메서드 패턴으로 대체한다.
        TemplateStart();

		switch (startDir) {
		case BEHAVIOR_MODE.MOVE_BOTLEFT:
			anim.SetTrigger ("Idle_BotLeft");
			break;
		case BEHAVIOR_MODE.MOVE_BOTRIGHT:
			anim.SetTrigger ("Idle_BotRight");
			break;
		case BEHAVIOR_MODE.MOVE_TOPLEFT:
			anim.SetTrigger ("Idle_TopLeft");
			break;
		case BEHAVIOR_MODE.MOVE_TOPRIGHT:
			anim.SetTrigger ("Idle_TopRight");
			break;
		}
    }


    // =================================================== Public Function ==========================================================

    // CharacterSript를 상속하는 클래스의 Start() 메서드에서 재정의할 내용들을 여기에 작성합니다.
    // Start 메서드는 캐릭터 객체의 Information을 캐싱하므로 재정의하면 안됩니다.
    protected abstract void TemplateStart();

    // 이동 메서드 등 코루틴으로 작성되는 함수들을 start 하는 함수
    protected abstract void CoroutineListStart();

    // 이동 메서드 등 코루틴으로 작성되는 함수들을 stop 하는 함수
    protected abstract void CoroutineListStop();

    // 현재 상황에 맞는 애니메이션을 재생합니다.
    protected abstract void ChangeAnimation(Vector2 moveVector);


    // 현재 객체의 위치를 전달받은 좌표로 즉시 이동
    // 플레이어 or NPC의 캐릭터가 특정 위치로 이동되는 이벤트를 위해 구현
    // 애니메이션 변화 X
    public void MoveToPositionImmediately(Vector3 des)
    {
        trans.position = des;
    }


    // 현재 객체의 위치를 전달받은 좌표의 방향을 바라보며 이동
    // 플레이어 or NPC의 캐릭터가 특정 위치로 걸어서 이동하는 이벤트를 위해 구현
    // 애니메이션 변화 1회
    public void MoveToPositionSlowlyByLookingAt(Vector3 des)
    {
        // 만약 여러 Character 들이 동시에 이동할 수 있으므로 코루틴을 이용, 동시적인 이동을 구현
        StartCoroutine(MoveToDestination(des));

        // 이동 방향으로 방향 전환
        ChangeAnimation(new Vector2(des.x - trans.position.x, des.y - trans.position.y));
    }

    // 현재 객체의 위치를 현재 방향을 바라보며 이동(뒷걸음질 등)
    // 플레이어 or NPC의 캐릭터가 특정 위치로 걸어서 이동하는 이벤트를 위해 구현
    // 애니메이션 변화 X
    public void MoveToPositionSlowlyNotLookingAt(Vector3 pushedVector, float distance)
    {
        // 인자를 바탕으로 이동할 위치 계산
        Vector3 des = trans.position + pushedVector.normalized * distance;
        des = new Vector3(des.x, des.y, des.y);

        // 만약 여러 Character 들이 동시에 이동할 수 있으므로 코루틴을 이용, 동시적인 이동을 구현
        StartCoroutine(MoveToDestination(des));

        // 이동 방향의 반대 방향으로 방향 전환
        ChangeAnimation(new Vector2(trans.position.x - des.x, trans.position.y - des.y));
    }


    // 현재 플레이어의 이동은 조이스틱을 이용한 이동이다.
    // 따라서 좌표가 아닌, 전달받은 벡터 방향으로 이동하는 메서드도 필요하다.
    // 매 프레임마다 애니메이션 변화
    public void MoveToVector(Vector3 vector)
    {
        isMoving = true;

        // 이동 방향으로 방향 전환
        ChangeAnimation(vector);

        //trans.Translate(vector * *Time.deltaTime  speed);
        rig2D.velocity = vector * speed;
        trans.position = new Vector3(trans.position.x, trans.position.y, trans.position.y);

        //isMoving = false;
    }

	public void MakeSpeedBack()
	{
		speed = ownSpeed;
	}

    // 캐릭터 정지
    // 애니메이션은 현재 방향을 본다.
    public void Stop()
    {
        // 캐릭터 이동을 멈춘다.
        isMoving = false;
        rig2D.velocity = new Vector2(0, 0) * 0;

        // 현재 방향으로의 정지 애니메이션 실행
        ChangeAnimation(new Vector2(0, 0));
    }


    // ====================================================== Private Function ===============================================================


    private IEnumerator MoveToDestination(Vector2 movePoint)
    {
        // 기존에 작동되고 있는 다른 코루틴을 모두 stop 한다.
        CoroutineListStop();

        isMoving = true;

        while (!(trans.position.x == movePoint.x || trans.position.y == movePoint.y))
        {
            trans.position = Vector3.MoveTowards(trans.position, new Vector3(movePoint.x, movePoint.y, movePoint.y), speed * Time.deltaTime);
            trans.position = new Vector3(trans.position.x, trans.position.y, trans.position.y);
            yield return null;
        }

        isMoving = false;

        // stop한 기존의 코루틴을 다시 start 한다.
        CoroutineListStart();
    }

    // ================================================================== Get Set Function ===============================================================


    public void SetSpeed(float tempSpeed)
    {
        speed = tempSpeed;
    }


    public float GetSpeed()
    {
        return speed;
    }

    public bool getIsMoving()
    {
        return isMoving;
    }
}
