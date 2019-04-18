using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SountScriptInMainScene : MonoBehaviour {
    
    // 사운드 canvas
    public GameObject soundDisplayer;


    // Background Speaker 옆 빼뺴로들
    public Image bSound1, bSound2, bSound3;
    // Background Slider
    public Slider bSlider;

    // Effect Speaker 옆 뺴뺴로들
    public Image eSound1, eSound2, eSound3;
    // Effect Slider
    public Slider eSlider;

    // 배경 사운드
    private AudioSource bSource;


    private void Awake()
    {
        // 최초 0.6으로 소리 지정
        bSound1.enabled = true;
        bSound2.enabled = true;
        bSound3.enabled = false;

        bSource = GameObject.Find("BGM").GetComponent<AudioSource>();
        bSlider.value = 0.6f;
        StaticInfoForSound.BGMSound = bSlider.value;
        bSource.volume = StaticInfoForSound.BGMSound;

        // 최초 0.6으로 소리 지정
        eSound1.enabled = true;
        eSound2.enabled = true;
        eSound3.enabled = false;

        eSlider.value = 0.6f;
        StaticInfoForSound.EffectSound = eSlider.value;
    }

    public void OnClickOption()
    {
        soundDisplayer.gameObject.SetActive(true);
    }

    // Background 소리 Slider를 이동 시 호출
    public void OnbSoundChanged(Slider slider)
    {
        float temp = slider.value;

        // 소리를 키우거나 줄이고
        StaticInfoForSound.BGMSound = temp;
        bSource.volume = StaticInfoForSound.BGMSound;

        // speaker 이미지 옆의 빼빼로들 수정해야징
        if (temp == 0)
        {
            bSound1.enabled = false;
            bSound2.enabled = false;
            bSound3.enabled = false;
        }
        else if (temp < 0.3f)
        {
            bSound1.enabled = true;
            bSound2.enabled = false;
            bSound3.enabled = false;
        }
        else if (temp < 0.6f)
        {
            bSound1.enabled = true;
            bSound2.enabled = true;
            bSound3.enabled = false;
        }
        else
        {
            bSound1.enabled = true;
            bSound2.enabled = true;
            bSound3.enabled = true;
        }

    }

    // Effect 소리 Slider를 이동 시 호출
    public void OneSoundChanged(Slider slider)
    {
        float temp = slider.value;

        // 소리를 키우거나 줄이고
        StaticInfoForSound.EffectSound = temp;

        // speaker 이미지 옆의 빼빼로들 수정해야징
        if (temp == 0)
        {
            eSound1.enabled = false;
            eSound2.enabled = false;
            eSound3.enabled = false;
        }
        else if (temp < 0.3f)
        {
            eSound1.enabled = true;
            eSound2.enabled = false;
            eSound3.enabled = false;
        }
        else if (temp < 0.6f)
        {
            eSound1.enabled = true;
            eSound2.enabled = true;
            eSound3.enabled = false;
        }
        else
        {
            eSound1.enabled = true;
            eSound2.enabled = true;
            eSound3.enabled = true;
        }
    }

    // close 버튼 클릭
    public void OnClickClose()
    {
        soundDisplayer.gameObject.SetActive(false);
    }
}
