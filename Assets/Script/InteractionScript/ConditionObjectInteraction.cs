﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditionObjectInteraction : MonoBehaviour {


	// HUD UI를 표시하는 canvas
	protected Transform canvas;

	// UI를 표시하는 NPC 객체
	protected Transform parentObject;

	// NPC 객체가 가지는 UI 목록
	public Interactable interactable;

	[SerializeField]
	public Image button;

    public enum TargetType { Object, NPC, Wholf};
    public TargetType type;

    private Animator interactionBtn;


    protected void Start()
	{
		canvas = GameObject.Find("UI_Canvas").GetComponent<Transform>();

		parentObject = gameObject.transform.parent;
	}


    protected void OnTriggerEnter2D(Collider2D collision)
    {
        // 상호작용이 가능한 거리로 플레이어가 접근
        if (collision.tag == "Player")
        {
            if (interactionBtn == null)
                interactionBtn = GameObject.Find("InteractionIcon").GetComponent<Animator>();
            interactionBtn.SetTrigger("InArea");

            // 해당 UI 프리팹을 생성
            switch(type)
            {
                case TargetType.Object:
                    button = Instantiate(Resources.Load<Image>("UI_Observation"), transform.position, transform.rotation);
                    break;
                case TargetType.NPC:
                    button = Instantiate(Resources.Load<Image>("UI_Talk"), transform.position, transform.rotation);
                    break;
                case TargetType.Wholf:
                    button = Instantiate(Resources.Load<Image>(""), transform.position, transform.rotation);
                    break;
            }


            // 생성한 UI를 Canvas 하위로 이동
            button.transform.SetParent(canvas, false);
            button.transform.SetAsFirstSibling();

            // 생성한 UI 위치를 부모 객체 위로 이동

            FSLocator.uiContainer.InsertObservation(button.gameObject);

            Vector3 myVector = transform.parent.position;
            button.GetComponent<DynamicUI>().SetVector(myVector);

            // Interact 버튼 클릭 시 현재 오브젝트가 가지는 리액션이 호출되도록 지정
            interactionBtn.GetComponent<InteractScript>().interactable = interactable;
            interactionBtn.GetComponent<InteractScript>().interactObject = this.gameObject;

            /*
            if (subQuest != null) {
                button.onClick.AddListener (delegate {
                    subQuest.SubQuestReaction ();
                });
            } else {
                button.onClick.AddListener (delegate {
                    interactable.Interact ();
                });
            }
            */
        }
    }

	protected void OnTriggerExit2D(Collider2D collision)
	{
		// 상호작용이 불가능한 거리로 플레이어가 이동
		if (collision.tag == "Player")
		{
            // 현재 표시된 UI를 제거
            Destroy(button.gameObject);

            interactionBtn.SetTrigger("OutArea");

            // 만약 현재 Interact 버튼에 등록된 이벤트가 이 객체의 이벤트이면..
            if (interactionBtn.GetComponent<InteractScript>().interactObject == this.gameObject)
                // 등록 해제
                interactionBtn.GetComponent<InteractScript>().interactable = null;
        }


	}
}
