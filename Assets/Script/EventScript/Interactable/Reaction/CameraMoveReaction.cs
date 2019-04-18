using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveReaction : DelayedReaction
{
    // true 면 카메라가 특정 오브젝트를 가리키고 계속 유지.
    // false 면 카메라가 특정 오브젝트를 가리키고 다시 player를 따라다닌다.
    public bool isStay;
    // 카메라가 따라갈 target
    public Transform target;

    private GameObject myCorotine;

    protected override void ImmediateReaction()
    {
        FSLocator.textDisplayer.reactionButton.enabled = false;

        myCorotine = CoroutineHandler.Start_Coroutine(CheckForMoving()).gameObject;
    }

    IEnumerator CheckForMoving()
    {
        // 기존에 카메라가 플레이어를 계속 따라다니는 우선 행동을 멈춘다.
        MainCamera camera = GameObject.Find("Main Camera").GetComponent<MainCamera>();
        camera.enabled = false;

        // 카메라가 target을 가리키도록 한다.
        while(true)
        {
            if (camera.transform.position.x == target.transform.position.x && camera.transform.position.y == target.transform.position.y)
                break;

            camera.transform.position = Vector3.MoveTowards(camera.transform.position, new Vector3(target.position.x, target.position.y, -60), 5 * Time.deltaTime);
            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, -60);
            yield return null;
        }

        // 이후 처리
        if(isStay == false)
            camera.enabled = true;

        FSLocator.textDisplayer.reactionButton.enabled = true;
        FSLocator.textDisplayer.reactionButton.onClick.Invoke();

        Destroy(myCorotine);
    }
}