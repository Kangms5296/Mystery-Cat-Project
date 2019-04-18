using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingPanelScript : MonoBehaviour {

    public Transform lastPanel;
    public Transform goalPos;

    public float beforeDelay;
    public float startDelay;
    public float scrollTime;
    public float endDelay;

    public CanvasGroup toMainScene;
    public Text toMainSceneBtn;

	// Use this for initialization
	void Start () {
        StartCoroutine(EndingStart());

    }

    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator EndingStart()
    {
        float conTime = 0;
        while (conTime < beforeDelay)
        {
            conTime += Time.deltaTime;

            yield return null;
        }
        conTime = 0;

        while (conTime < startDelay)
        {
            conTime += Time.deltaTime;

            yield return null;
        }
        conTime = 0;

        Vector2 firstPos = lastPanel.position;

        while(conTime < scrollTime)
        {
            conTime += Time.deltaTime;

            lastPanel.position = Vector2.Lerp(firstPos, goalPos.position, conTime / scrollTime);
            float temp = (scrollTime - conTime) / scrollTime;
            lastPanel.GetComponent<Image>().color = new Color(temp, temp, temp);

            yield return null;
        }
        conTime = 0;

        while (conTime < endDelay)
        {
            conTime += Time.deltaTime;
            toMainScene.alpha = conTime/endDelay;
            yield return null;
        }
        toMainSceneBtn.raycastTarget = true;
    }
}
