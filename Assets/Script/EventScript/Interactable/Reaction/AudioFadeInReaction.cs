using UnityEngine;
using System.Collections;

public class AudioFadeInReaction : DelayedReaction
{
	public AudioSource audioSource;
	public AudioClip audioClip;
	public float fadeTime;
    public float beforeDelay;

    GameObject go;



	protected override void ImmediateReaction()
	{
        go = CoroutineHandler.Start_Coroutine(FadeInAudio()).gameObject;
	}

	IEnumerator FadeInAudio()
	{
        float temp = 0;
        while (temp < beforeDelay)
        {
            yield return null;
            temp += Time.deltaTime;
        }

        if (audioClip != null)
            audioSource.clip = audioClip;
        audioSource.Stop();
        audioSource.Play();

        // 현재 메인 배경음 담당 source를 교체
        StaticInfoForSound.con_BGM_Audio = audioSource;

        float conTime = 0;
        while (conTime < fadeTime)
        {
            audioSource.volume = StaticInfoForSound.BGMSound * (conTime) / fadeTime;
            conTime += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = StaticInfoForSound.BGMSound;

        Destroy(go);
	}
}