using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundByDistance : MonoBehaviour {

    public float distance;
    public AudioSource audioSource;
    public AudioClip audioClip;

    public Transform target;

	// Use this for initialization
	void Start () {
        audioSource.clip = audioClip;

    }
	
	// Update is called once per frame
	void Update () {
        audioSource.volume = (distance - Vector2.Distance(transform.position, target.position)) * StaticInfoForSound.EffectSound;
	}


    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        audioSource.Play();
        inObject = collision.transform;
        inArea = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inArea = false;
        inObject = null;
        audioSource.Stop();
        audioSource.volume = 0;
    }
    */
}
