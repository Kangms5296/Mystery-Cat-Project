using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemlistButton : MonoBehaviour {

    public Button up;
    public Button down;

    public RectTransform content;



    // list를 한칸 올린다.
    public void OnClickUp()
    {
        if (content.localPosition.y <= 0)
            return;

        content.localPosition = new Vector2(content.localPosition.x, content.localPosition.y - 180);
    }

    // list를 한칸 내린다.
    public void OnClickDown()
    {
        if (content.localPosition.y >= 720)
            return;

        content.localPosition = new Vector2(content.localPosition.x, content.localPosition.y + 180);
    }
}
