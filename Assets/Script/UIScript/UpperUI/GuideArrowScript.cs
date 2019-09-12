using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideArrowScript : MonoBehaviour {


    private Transform trans;

    private Transform target;
    private bool isTarget;

    [HideInInspector]
    public int index;

    [HideInInspector]
    public GameObject guideCoroutine;


    // Use this for initialization
    void Awake () {
        trans = GetComponent<Transform>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isTarget)
        {
            trans.position = target.position;
        }
    }

    public void StartGuide(GameObject coroutine)
    {
        guideCoroutine = coroutine;

        index = 0;

        isTarget = false;
        target = null;

        Debug.Log("!!!!!!!!!!!!!");
        gameObject.SetActive(true);
    }

    public void SetTarget(Transform newTarget, Transform parentTrans)
    {
        target = newTarget;
        trans.position = target.position;

        trans.SetParent(parentTrans);

        isTarget = true;
    }

    public void EndGuide()
    {
        trans.position = new Vector2(5000, 0);
        gameObject.SetActive(false);
    }

    public void AbortGuide()
    {
        if (guideCoroutine != null)
            Destroy(guideCoroutine);
        
        EndGuide();
    }
}
