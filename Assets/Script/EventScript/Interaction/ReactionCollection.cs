using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ReactionCollection : MonoBehaviour
{
    public Reaction[] reactions = new Reaction[0];

    private int startIndex = 0;
    [HideInInspector]
    public float delaytime;

    // 전에 진행한 reaction이 Text Reaction이면 클릭 sound를 on
    private bool beforeTextReaction = false;
    private AudioSource audioSourceForTalk;
    private AudioClip audioClipForTalk;

    private void Start()
    {
        audioSourceForTalk = GameObject.Find("EffectSound_For_Click").GetComponent<AudioSource>();
        audioClipForTalk = Resources.Load<AudioClip>("AudioResource/EffectSound/S_Click");

        for (int i = 0; i < reactions.Length; i++)
        {
            DelayedReaction delayedReaction = reactions[i] as DelayedReaction;

            if (delayedReaction)
                delayedReaction.Init();
            else
                reactions[i].Init();
        }
    }

	public void InitIndex()
	{
		startIndex = 0;

		for (int i = 0; i < reactions.Length; i++)
		{
			DelayedReaction delayedReaction = reactions[i] as DelayedReaction;

			if (delayedReaction)
				delayedReaction.Init();
			else
				reactions[i].Init();
		}

        if(FSLocator.textDisplayer != null)
		    FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
	}


    public void React()
    {

		if (reactions.Length == 0) {

			return;
		}
		
		if (startIndex == 0) {
            if(FSLocator.textDisplayer != null)
			    FSLocator.textDisplayer.StopAllCoroutines ();
		}
        else if (FSLocator.textDisplayer.isTyping)
        {
            FSLocator.textDisplayer.SkipTypingLetter();
			FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
			FSLocator.textDisplayer.reactionButton.onClick.AddListener(delegate { this.React(); });
            return;
        }

        string reactionName = "";
        for (int i = startIndex; i < reactions.Length; i++)
        {
            DelayedReaction delayedReaction = reactions[i] as DelayedReaction;

            if (delayedReaction)
            {
                if (beforeTextReaction == true)
                {
                    audioSourceForTalk.clip = audioClipForTalk;
                    audioSourceForTalk.Play();
                    beforeTextReaction = false;
                }

                reactionName = reactions[i].GetType().Name;
                if (reactionName == "TextReaction")
                {
                    beforeTextReaction = true;
                    startIndex = i + 1;
                    delayedReaction.React(this);
                    FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
                    FSLocator.textDisplayer.reactionButton.onClick.AddListener(delegate { this.React(); });
                    return;
                }
                else if (reactionName == "DelayReaction")
                {
                    startIndex = i + 1;
                    FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
                    FSLocator.textDisplayer.reactionButton.onClick.AddListener(delegate { this.React(); });
                    delayedReaction.React(this);
                    return;
                }
                else if (reactionName == "CutSceneStartReaction")
                {
                    startIndex = i + 1;
                    FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
                    FSLocator.textDisplayer.reactionButton.onClick.AddListener(delegate { this.React(); });
                    delayedReaction.React(this);
                    return;
                }
                else if (reactionName == "CutSceneEndReaction")
                {
                    startIndex = i + 1;
                    FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
                    FSLocator.textDisplayer.reactionButton.onClick.AddListener(delegate { this.React(); });
                    delayedReaction.React(this);
                    return;
                }
                else if (reactionName == "CharacterMoveReaction")
                {
                    startIndex = i + 1;
                    FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
                    FSLocator.textDisplayer.reactionButton.onClick.AddListener(delegate { this.React(); });
                    delayedReaction.React(this);
                    return;
                }
                else if (reactionName == "CameraMoveReaction")
                {
                    startIndex = i + 1;
                    FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
                    FSLocator.textDisplayer.reactionButton.onClick.AddListener(delegate { this.React(); });
                    delayedReaction.React(this);
                    return;
                }
                else if (reactionName == "CameraZoomInReaction")
                {
                    startIndex = i + 1;
                    FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
                    FSLocator.textDisplayer.reactionButton.onClick.AddListener(delegate { this.React(); });
                    delayedReaction.React(this);
                    return;
                }
                else if (reactionName == "CameraZoomOutReaction")
                {
                    startIndex = i + 1;
                    FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
                    FSLocator.textDisplayer.reactionButton.onClick.AddListener(delegate { this.React(); });
                    delayedReaction.React(this);
                    return;
                }
                else if (reactionName == "PushBackReaction")
                {
                    startIndex = i + 1;
                    FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
                    FSLocator.textDisplayer.reactionButton.onClick.AddListener(delegate { this.React(); });
                    delayedReaction.React(this);
                    return;
                }
                else if (reactionName == "GameOverReaction")
                {
                    startIndex = i + 1;
                    FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
                    FSLocator.textDisplayer.reactionButton.onClick.AddListener(delegate { this.React(); });
                    delayedReaction.React(this);
                    return;
                }
                else if (reactionName == "ObservationReaction")
                {
                    startIndex = i + 1;
                    FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
                    FSLocator.textDisplayer.reactionButton.onClick.AddListener(delegate { this.React(); });
                    delayedReaction.React(this);
                    return;
                }
                else if (reactionName == "MoveDistanceCheckReaction")
                {
                    startIndex = i + 1;
                    FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
                    FSLocator.textDisplayer.reactionButton.onClick.AddListener(delegate { this.React(); });
                    delayedReaction.React(this);
                    return;
                }
                else if (reactionName == "WaitingUntilClickMissionReaction")
                {
                    startIndex = i + 1;
                    FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
                    FSLocator.textDisplayer.reactionButton.onClick.AddListener(delegate { this.React(); });
                    delayedReaction.React(this);
                    return;
                }
                else if (reactionName == "WaitingUntilClickInventoryReaction")
                {
                    startIndex = i + 1;
                    FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
                    FSLocator.textDisplayer.reactionButton.onClick.AddListener(delegate { this.React(); });
                    delayedReaction.React(this);
                    return;
                }
                else if (reactionName == "WaitingUntilClickDocuInfoReaction")
                {
                    startIndex = i + 1;
                    FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
                    FSLocator.textDisplayer.reactionButton.onClick.AddListener(delegate { this.React(); });
                    delayedReaction.React(this);
                    return;
                }
                else if (reactionName == "WaitingUntilClickDocuCharReaction")
                {
                    startIndex = i + 1;
                    FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
                    FSLocator.textDisplayer.reactionButton.onClick.AddListener(delegate { this.React(); });
                    delayedReaction.React(this);
                    return;
                }
                else if (reactionName == "WaitingUntilMixReaction")
                {
                    startIndex = i + 1;
                    FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
                    FSLocator.textDisplayer.reactionButton.onClick.AddListener(delegate { this.React(); });
                    delayedReaction.React(this);
                    return;
                }
                else if (reactionName == "WatingUntilClickSaveReaction")
                {
                    startIndex = i + 1;
                    FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
                    FSLocator.textDisplayer.reactionButton.onClick.AddListener(delegate { this.React(); });
                    delayedReaction.React(this);
                    return;
                }
                else if (reactionName == "EventCallbackReaction" ||
                    reactionName == "EventCallbackConditionReaction" ||
                    reactionName == "EventConditionReaction")
                {
                    startIndex = 0;
                    delayedReaction.React(this);
                    return;
                }
                else
                {
                    delayedReaction.React(this);
                }
            }
        }
    }

	public void Skip()
	{
		for (int i = startIndex; i < reactions.Length; i++)
		{

        }

	}

	public void InitAndReact(){
		InitIndex ();
		React ();
	}

	public void MoveAround()
	{
		reactions[0].React(this);
	}
  
}
