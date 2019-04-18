using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoticonDisplayer : MonoBehaviour {

    private float dissolveTime = 0.0f;
    private float emoticonMaintainTime = 0.0f;
    private SpriteRenderer emoticonSprite;
    private Color emoticonColor;

    private void Awake()
    {
        
    }
    public void ShowEmoticon()
    {

        //ImediateStopTransition();
        StartCoroutine("Show");
    }

    public void MaintainEmoticon()
    {
        StartCoroutine("Maintain");
    }

    public void HideEmoticon()
    {
        
        //ImediateStopTransition();
        StartCoroutine("Hide");
    }


    public void DrawEmoticon(GameObject target, Sprite sprite, float dissolve, float maintain)
    {
        dissolveTime = dissolve;
        emoticonMaintainTime = maintain;
        emoticonSprite = target.GetComponent<SpriteRenderer>();
        emoticonColor = emoticonSprite.color;
        emoticonSprite.sprite = sprite;
        ImmediateStopTransition();
        if (sprite)
        {
            ShowEmoticon();
        }
        else
        {
            emoticonColor.a = 0.0f;
            emoticonSprite.color = emoticonColor;
        }
            
    }



    private void ImmediateStopTransition()
    {
        StopCoroutine("Show");
        StopCoroutine("Maintain");
        StopCoroutine("Hide");
    }

    private IEnumerator Show()
    {
        emoticonColor.a = 0.0f;
        emoticonSprite.color = emoticonColor;

        float startTime = Time.time;

        while (Time.time <= startTime + dissolveTime)
        {
            emoticonColor.a += (Time.deltaTime / dissolveTime);
            emoticonSprite.color = emoticonColor;
            yield return null;
        }

        emoticonColor.a = 1.0f;
        emoticonSprite.color = emoticonColor;
        MaintainEmoticon();
    }

    private IEnumerator Maintain()
    {
        yield return new WaitForSeconds(emoticonMaintainTime);
        HideEmoticon();
    }

    private IEnumerator Hide()
    {
        //characterCanvasGroup.alpha = 1.0f;
        float startTime = Time.time;

        while (Time.time <= startTime + dissolveTime)
        {
            emoticonColor.a -= (Time.deltaTime / dissolveTime);
            emoticonSprite.color = emoticonColor;
            yield return null;
        }

        emoticonColor.a = 0.0f;
        emoticonSprite.color = emoticonColor;
    }
}
