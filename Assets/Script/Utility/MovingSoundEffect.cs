using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MovingSoundEffect : MonoBehaviour {

    private AudioSource soundEffect;
    private NewCharacter character;
    
    // Use this for initialization
    void Start () {
        soundEffect = GetComponentInChildren<AudioSource>();
        character = GetComponent<NewCharacter>();
        StartCoroutine(MovingSound());
    }

    IEnumerator MovingSound()
    {
        while (true)
        {
            if (character.getIsMoving())
            {
                float temp = soundEffect.volume + Time.deltaTime * 4;

                if (temp > StaticInfoForSound.EffectSound)
                {
                    temp = StaticInfoForSound.EffectSound;
                }

                soundEffect.volume = temp;
            }
            else
            {
                soundEffect.volume -= Time.deltaTime * 4;
            }
            yield return null;
        }
    }
}
