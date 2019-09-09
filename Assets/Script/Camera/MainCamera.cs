using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    public Transform player;
    public Transform target;
    private Vector2 targetPos;

    public bool isTracing = false;

    private Vector2 tempVector;

    private void Start()
    {
        targetPos = target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // 카메라가 표적을 점점 따라간다.
        if (isTracing)
        {
            tempVector = Vector2.MoveTowards(transform.position, targetPos, 7 * Time.deltaTime);
            transform.position = new Vector3(tempVector.x, tempVector.y, -100);
        }
        // 카메라가 바로 표적을 가리킨다.
        else
        {
            transform.position = new Vector3(target.position.x, target.position.y, -100);
        }
    }

    public void SetNewTracingTarget(Transform newTarget)
    {
        target = newTarget;
        targetPos = target.position;

        isTracing = true;
    }

    public void SetNewTarget(Transform newTarget)
    {
        target = newTarget;

        isTracing = false;
    }
}


