using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoundScript : MonoBehaviour {

    // 환경설정 canvas
    public GameObject settingDisplayer;

    // 사운드 canvas
    public GameObject soundDisplayer;


    // Background Speaker 옆 빼뺴로들
    public Image bSound1, bSound2, bSound3;
    // Background Slider
    public Slider bSlider;
    // Background 소리 정보
    private AudioSource bSource;

    // Effect Speaker 옆 뺴뺴로들
    public Image eSound1, eSound2, eSound3;
    // Effect Slider
    public Slider eSlider;
    // Effect 소리 정보
    private AudioSource eSource;
    private AudioSource eSource_Foot;

    bool isFirst = true;


    private void Start()
    {
        bSource = GameObject.Find("BGMSound_Main").GetComponent<AudioSource>();
        StaticInfoForSound.con_BGM_Audio = bSource;
        bSlider.value = StaticInfoForSound.BGMSound;

        eSource = GameObject.Find("EffectSound").GetComponent<AudioSource>();
        eSlider.value = StaticInfoForSound.EffectSound;
    }

    // Background 소리 Slider를 이동 시 호출
    public void OnbSoundChanged(Slider slider)
    {
        float temp = 0;

        if (isFirst)
        {
            temp = StaticInfoForSound.BGMSound;

            isFirst = false;
        }
        else
        {
            temp = slider.value;
        }
        StaticInfoForSound.con_BGM_Audio.volume = temp;
        StaticInfoForSound.BGMSound = temp;

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
        float temp = 0;

        if (isFirst)
        {
            temp = StaticInfoForSound.EffectSound;

            isFirst = false;
        }
        else
        {
            temp = slider.value;
        }
        eSource.volume = temp;
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

    // back 버튼 클릭
    public void OnClickBack()
    {
        soundDisplayer.gameObject.SetActive(false);

        settingDisplayer.gameObject.SetActive(true);
    }
}
