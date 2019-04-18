using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimerScript : MonoBehaviour {

    public Text minute;
    public Text second;

    private int minuteFloat = 2;
    private int secondFloat = 0;

    private bool isFinished = false;

    public ReactionCollection failedReaction;

    private void OnEnable()
    {
        minuteFloat = 2;
        minute.text = string.Format("{0:00}", minuteFloat);
        secondFloat = 0;
        second.text = string.Format("{0:00}", secondFloat);

        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update () {
        // 0초..
		if(secondFloat == 0)
        {
            // 0분, 즉 시간이 모두 끝남
            if(minuteFloat == 0)
            {

            }

        }
	}

    public void Stop()
    {
        isFinished = true;
    }

    IEnumerator Timer()
    {
        while(isFinished == false)
        {
            // 0초..
            if (secondFloat == 0)
            {
                // 0분, 즉 시간이 모두 끝남
                if (minuteFloat == 0)
                {
                    isFinished = true;

                    failedReaction.InitAndReact();

                    // 종료
                    Debug.Log("Finish");
                }
                else
                {
                    // 1초 대기
                    yield return new WaitForSeconds(1);

                    // 1분 삭감
                    minuteFloat--;
                    // 59초로 다시 셋팅
                    secondFloat = 59;

                    // 출력
                    minute.text = string.Format("{0:00}", minuteFloat);
                    second.text = string.Format("{0:00}", secondFloat);

                }

            }
            else
            {
                // 1초 대기
                yield return new WaitForSeconds(1);

                // 1초 삭감
                secondFloat--;

                // 출력
                second.text = string.Format("{0:00}", secondFloat);
                
            }
            yield return null;
        }
    }
}
