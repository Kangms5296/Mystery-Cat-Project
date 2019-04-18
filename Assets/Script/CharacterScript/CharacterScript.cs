using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterScript : MonoBehaviour {

    // 현재 객체의 Transform 콤포넌트
    protected Transform trans;

    // 현재 객체의 Animator 콤포넌트
    protected Animator anim;

    // 현재 객체의 Rigidbody2D 콤포넌트
    protected Rigidbody2D rig2D;

	public enum BEHAVIOR_MODE { IDLE, MOVE_TOPLEFT, MOVE_TOPRIGHT, MOVE_BOTLEFT, MOVE_BOTRIGHT };
	public BEHAVIOR_MODE startDir;

	public bool isMoving = false;



	protected float speed = 1.5f;

	// Use this for initialization
	void Start () {
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

    // CharacterSript를 상속하는 클래스의 Start() 메서드에서 재정의할 내용들을 여기에 작성합니다.
    // Start 메서드는 캐릭터 객체의 Information을 캐싱하므로 재정의하면 안됩니다.
    public abstract void TemplateStart();

    // 현재 객체의 이동 함수
    public abstract void Move(Vector2 movePoint, float speed);

	public bool getIsMoving(){
		return isMoving;
	}


	protected IEnumerator MoveToDestination(Vector2 movePoint){

		while(!(trans.position.x == movePoint.x || trans.position.y == movePoint.y)){
			Debug.Log ("NPC MOVE");
			trans.position = Vector3.MoveTowards(trans.position, new Vector3(movePoint.x, movePoint.y, movePoint.y), speed* Time.deltaTime);
			trans.position = new Vector3(trans.position.x, trans.position.y, trans.position.y);
			yield return null;
		}
		isMoving = false;

	}
}
