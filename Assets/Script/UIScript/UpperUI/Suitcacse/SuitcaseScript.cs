using UnityEngine;
using UnityEngine.EventSystems;

public class SuitcaseScript : UIScript {

    private Animation anim;

    private bool isClick = false;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animation>();
    }

    public override void OnClickUI()
    {
        // 첫번째 클릭 - 촤라락 하고 나옴
        if (!isClick)
        {
            isClick = true;

            // 촤라락
            anim["SuitcaseClick"].speed = 1.0f;
            anim.Play("SuitcaseClick");
        }
        // 두번째 클릭 - 스르륵 하고 들어감
        else
        {
            isClick = false;

            // 촤라락
            if (anim["SuitcaseClick"].normalizedTime == 0)
                anim["SuitcaseClick"].normalizedTime = 1.0f;
            anim["SuitcaseClick"].speed = -1.0f;
            anim.Play("SuitcaseClick");
        }
    }
}
