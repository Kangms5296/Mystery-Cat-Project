using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomOutReaction : DelayedReaction
{
    // 줌아웃으로 돌아갈 오브젝트
    public Transform returnObject;

    // 줌아웃 속도
    public float speed = 1;

    private GameObject myCorotine;

    protected override void ImmediateReaction()
    {
        FSLocator.textDisplayer.reactionButton.enabled = false;

        myCorotine = CoroutineHandler.Start_Coroutine(Zoom_Out()).gameObject;
    }

    IEnumerator Zoom_Out()
    {
        // 카메라 조절을 위해 캐싱
        Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();

        // 카메라 초기 위치 캐싱
        Vector3 cameraFirst = camera.transform.position;
        Vector2 temp;

        float conTime = 0;
        while (conTime < 1)
        {
            // 카메라가 점점 축소
            camera.orthographicSize = 3.5f + conTime * 1.5f;

            // 카메라가 점점 지정된 위치로 이동
            temp = Vector2.Lerp(cameraFirst, returnObject.position, conTime);
            camera.transform.position = new Vector3(temp.x, temp.y, -100);

            conTime += Time.deltaTime * speed;
            yield return null;
        }
        camera.orthographicSize = 5f;
        camera.transform.position = new Vector3(returnObject.position.x, returnObject.position.y, -100);

        // 카메라가 유저를 따라다니는 행동을 다시 시작
        camera.GetComponent<MainCamera>().enabled = true;

        FSLocator.textDisplayer.reactionButton.enabled = true;
        FSLocator.textDisplayer.reactionButton.onClick.Invoke();

        Destroy(myCorotine);
    }
}
