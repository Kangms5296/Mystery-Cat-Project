using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoachMarkManager : MonoBehaviour {

    // 2번 터치하면 UI 종료, 1회 터치와 2회 터치가 일정 시간 내에 이루어 진 경우만 UI를 종료함.
    public float timeBetweenTouch;
    private float beforeTouchTime = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickExit()
    {
        // 최초 touch는 시간만 저장한 뒤 종료
        if(beforeTouchTime == -1)
        {
            beforeTouchTime = Time.time;
            return;
        }
        // 이후부턴, 이전의 touch와 현 touch 사이 시간을 비교하여 UI를 종료할 지 결정
        else
        {
            float newTouchTime = Time.time;

            // 지정된 시간 안에 연속적인 touch를 수행하였으므로..
            if (newTouchTime < beforeTouchTime + timeBetweenTouch)
            {
                // UI 종료
                transform.GetChild(0).gameObject.SetActive(false);
            }
            
            // 이번에 터치한 시간을 기록
            beforeTouchTime = Time.time;
        }
    }
}
