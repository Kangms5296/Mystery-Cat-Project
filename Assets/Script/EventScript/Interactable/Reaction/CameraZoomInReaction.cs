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

        // 지.변.
        bool isSizeOK = false;
        bool isPosOK = false;


        while (isSizeOK == false || isPosOK == false )
        {
            if (camera.orthographicSize > 3.5f)
                camera.orthographicSize -= Time.deltaTime * speed;
            else
            {
                isSizeOK = true;
                camera.orthographicSize = 3.5f;
            }

            if (camera.transform.position.x == focusObject.transform.position.x && camera.transform.position.y == focusObject.transform.position.y)
                isPosOK = true;
            else
            {
                //camera.transform.position = Vector3.MoveTowards(camera.transform.position, new Vector3(foucsObject.position.x, foucsObject.position.y, -60), 5 * Time.deltaTime);
                camera.transform.position = Vector3.Lerp(cameraFirst, focusObject.transform.position, (5 - camera.orthographicSize) / 1.5f);
                camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, -60);
            }

            yield return null;
        }


        FSLocator.textDisplayer.reactionButton.enabled = true;
        FSLocator.textDisplayer.reactionButton.onClick.Invoke();

        Destroy(myCorotine);
    }
}
