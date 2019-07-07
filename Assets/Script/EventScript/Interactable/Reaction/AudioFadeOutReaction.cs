using UnityEngine;
using System.Collections;

public class AudioFadeOutReaction : DelayedReaction
{
	public AudioSource audioSource;
	public AudioClip audioClip;
	public float fadeTime;
    public float beforeDelay;
	GameObject go;

	protected override void ImmediateReaction()
	{
        if (audioSource.volume == 0)
            return;

		if (audioClip != null)
			audioSource.clip = audioClip;
		
		audioSource.volume = StaticInfoForSound.BGMSound;
		//audioSource.PlayDelayed(delay);
		go = CoroutineHandler.Start_Coroutine(FadeOutAudio()).gameObject;
	}

	IEnumerator FadeOutAudio()
	{
        float temp = 0;
        while (temp < beforeDelay)
        {
            yield return null;
            temp += Time.deltaTime;
        }


        float conTime = 0;

		while (conTime < fadeTime)
		{
            audioSource.volume = StaticInfoForSound.BGMSound * (fadeTime - conTime) / fadeTime;
            conTime += Time.deltaTime;
            yield return null;
		}
        audioSource.volume = 0;
        audioSource.clip = null;

        //audioSource.Stop();


        Destroy(go);
	}
}