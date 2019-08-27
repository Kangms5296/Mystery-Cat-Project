using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoundScript : MonoBehaviour {

    public GameObject reactionButton;

    // 환경설정 canvas
    public GameObject mainDisplayer;

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
    private List<AudioSource> eSources;

    bool isFirst = true;


    private void Start()
    {
        bSlider.value = StaticInfoForSound.BGMSound;

        AudioSource[] effectList = FindObjectsOfType<AudioSource>();
        eSources = new List<AudioSource>();
        foreach (AudioSource tempEffect in effectList)
        {
            if (tempEffect.name == "BGMSound_Main" || tempEffect.name == "BGMSound_Sub" || tempEffect.name == "PlayerSound")
                continue;

            eSources.Add(tempEffect);
        }
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
        foreach(AudioSource tempSource in eSources)
            tempSource.volume = temp;
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
        soundDisplayer.SetActive(false);

        reactionButton.gameObject.SetActive(false);
    }

    // back 버튼 클릭
    public void OnClickBack()
    {
        soundDisplayer.gameObject.SetActive(false);

        mainDisplayer.gameObject.SetActive(true);
    }
}
