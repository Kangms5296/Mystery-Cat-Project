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
        // 카메라가 유저를 따라다니는 행동을 중단
        camera.GetComponent<MainCamera>().enabled = false;
        // 카메라 초기 위치 캐싱
        Vector3 cameraFirst = camera.transform.position;

        // 지.변.
        bool isSizeOK = false;
        bool isPosOK = false;


        while (isSizeOK == false || isPosOK == false)
        {
            if (camera.orthographicSize < 5)
                camera.orthographicSize += Time.deltaTime * speed;
            else
            {
                isSizeOK = true;
                camera.orthographicSize = 5;
            }

            if (camera.transform.position.x == returnObject.transform.position.x && camera.transform.position.y == returnObject.transform.position.y)
                isPosOK = true;
            else
            {
                //camera.transform.position = Vector3.MoveTowards(camera.transform.position, new Vector3(foucsObject.position.x, foucsObject.position.y, -60), 5 * Time.deltaTime);
                camera.transform.position = Vector3.Lerp(cameraFirst, returnObject.transform.position, (camera.orthographicSize - 3.5f) / 1.5f);
                camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, -60);
            }

            yield return null;
        }

        // 카메라가 유저를 따라다니는 행동을 다시 시작
        camera.GetComponent<MainCamera>().enabled = true;

        FSLocator.textDisplayer.reactionButton.enabled = true;
        FSLocator.textDisplayer.reactionButton.onClick.Invoke();

        Destroy(myCorotine);
    }
}
