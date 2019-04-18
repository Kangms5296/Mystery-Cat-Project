using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour {


    // 현재 상호작용 거리에 있는 오브젝트
    [HideInInspector]
    public GameObject interactObject;

    // 현재 상호작용 거리에 있는 오브젝트의 상호작용 내용.
    [HideInInspector]
    public Interactable interactable;


    public AudioSource source;
    public AudioClip clip;

    // Use this for initialization
    void Start () {
        //source = GetComponent<AudioSource>();
        source.clip = clip;
        source.volume = StaticInfoForSound.EffectSound;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    
    // ====================================================================== public function =========================================================================

     public void OnClickInteraction()
    {
        if (interactable != null)
        {
            interactable.Interact();
        }

        source.Play();
    }
}
