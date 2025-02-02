﻿using System.Collections.Generic;
using UnityEngine;

public class UICaching : MonoBehaviour {

    // 상단에 존재하는 모든 UI들(tag가 UI로 되어있다.)
    public List<GameObject> UI;

    // Displayer 정보 확인
    public List<GameObject> displayers;

    public AudioSource source;
    public AudioClip clip1;
    public AudioClip clip2;



    public void ClearUI()
    {
        UI.Clear();
    }

    public void AddUI(GameObject ui)
    {
        UI.Add(ui);
    }

    public List<GameObject> GetUI()
    {
        return UI;
    }

    public List<GameObject> GetDisplayers()
    {
        return displayers;
    }

    public void OnClickUIClip1()
    {
        source.clip = clip1;
        source.Play();
    }

    public void OnClickUIClip2()
    {
        source.clip = clip2;
        source.Play();
    }
}
