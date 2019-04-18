using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DefaultMode : TouchMode
{
    // 각 Mode는 여러 객체가 필요치 않으니, 싱글톤 패턴으로 구현한다.
    public static DefaultMode defaultMode = new DefaultMode();
    private DefaultMode() { }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnButtonDownNPC(TouchPointScript conTouch)
    {

    }

    public void OnButtonUpNPC()
    {

    }

    public void OnDragNPC()
    {

    }

    public void InTouchArea(TouchPointScript conTouch)
    {

    }

    public void OutTouchArea()
    {

    }
}
