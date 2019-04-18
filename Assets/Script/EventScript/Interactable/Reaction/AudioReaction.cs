using UnityEngine;

public class AudioReaction : DelayedReaction
{
    public AudioSource audioSource;
    public AudioClip audioClip;

    public bool isBGM = false;

    protected override void ImmediateReaction()
    {
        // BGM의 Audio를 실행하는 중이면
        if (isBGM)
            audioSource.volume = StaticInfoForSound.BGMSound;
        else
            audioSource.volume = StaticInfoForSound.EffectSound;

        audioSource.clip = audioClip;
        audioSource.Play();
    }
}