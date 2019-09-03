using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomInReaction : DelayedReaction
{
    // 줌인으로 보여줄 오브젝트
    public Transform focusObject;

    // 줌인 속도
    public float speed = 1;

    private GameObject myCorotine;

    protected override void ImmediateReaction()
    {
        FSLocator.textDisplayer.reactionButton.enabled = false;

        myCorotine = CoroutineHandler.Start_Coroutine(Zoom_In()).gameObject;
    }

    IEnumerator Zoom_In()
    {
        // 카메라 조절을 위해 캐싱
        Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        // 카메라가 유저를 따라다니는 행동을 중단
        camera.GetComponent<MainCamera>().enabled = false;
        
        // 카메라 초기 위치 캐싱
        Vector3 cameraFirst = camera.transform.position;
        Vector2 temp;

        float conTime = 0;
        while (conTime < 1)
        {
            // 카메라가 점점 확대
            camera.orthographicSize = 5 - conTime * 1.5f;

            // 카메라가 점점 지정된 위치로 이동
            temp = Vector2.Lerp(cameraFirst, focusObject.position, conTime);
            camera.transform.position = new Vector3(temp.x, temp.y, -100);

            conTime += Time.deltaTime * speed;
            yield return null;
        }
        camera.orthographicSize = 3.5f;
        camera.transform.position = new Vector3(focusObject.position.x, focusObject.position.y, -100);
        
        FSLocator.textDisplayer.reactionButton.enabled = true;
        FSLocator.textDisplayer.reactionButton.onClick.Invoke();

        Destroy(myCorotine);
    }
}
