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
        audioSourceForTalk = GameObject.Find("EffectSound").GetComponent<AudioSource>();
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
                if (reactions[i].GetType().Name == "TextReaction")
                {
					if (startIndex == reactions.Length - 1) {
						break;
					}
                    else
                    {
                        beforeTextReaction = true;
                        startIndex = i + 1;
                        delayedReaction.React(this);
						FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
						FSLocator.textDisplayer.reactionButton.onClick.AddListener(delegate { this.React(); });
                        return;
                    }
                }
                if (reactions [i].GetType ().Name == "ChoiceTextReaction") {
					
						startIndex = i + 1;
						delayedReaction.React(this);
						FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
						FSLocator.textDisplayer.reactionButton.enabled = true;
						return;

				}
                else if (reactions[i].GetType().Name == "DelayReaction")
                {
                    if (startIndex == reactions.Length - 1)
                        break;
                    else
                    {
                        startIndex = i + 1;
                        if (FSLocator.textDisplayer != null) { 
                            FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
                            FSLocator.textDisplayer.reactionButton.onClick.AddListener(delegate { this.React(); });
                        }
						delayedReaction.React(this);
                        return;
                    }
                }
                else if (reactions[i].GetType().Name == "AnimationReaction")
                {
                    if (startIndex == reactions.Length - 1)
                        break;
                    else
                    {
                        startIndex = i + 1;
						FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
						FSLocator.textDisplayer.reactionButton.onClick.AddListener(delegate { this.React(); });
                        delayedReaction.React(this);
                        return;
                    }
                }
				else if (reactions[i].GetType().Name == "CharacterMoveReaction")
                {
                    if (startIndex == reactions.Length - 1)
                        break;
                    else
                    {
                        startIndex = i + 1;
						FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
						FSLocator.textDisplayer.reactionButton.onClick.AddListener(delegate { this.React(); });
                        delayedReaction.React(this);
                        return;
                    }
                }
                else if (reactions[i].GetType().Name == "CameraMoveReaction")
                {
                    if (startIndex == reactions.Length - 1)
                        break;
                    else
                    {
                        startIndex = i + 1;
                        FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
                        FSLocator.textDisplayer.reactionButton.onClick.AddListener(delegate { this.React(); });
                        delayedReaction.React(this);
                        return;
                    }
                }
                else if (reactions[i].GetType().Name == "CameraZoomInReaction")
                {
                    if (startIndex == reactions.Length - 1)
                        break;
                    else
                    {
                        startIndex = i + 1;
                        FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
                        FSLocator.textDisplayer.reactionButton.onClick.AddListener(delegate { this.React(); });
                        delayedReaction.React(this);
                        return;
                    }
                }
                else if (reactions[i].GetType().Name == "CameraZoomOutReaction")
                {
                    if (startIndex == reactions.Length - 1)
                        break;
                    else
                    {
                        startIndex = i + 1;
                        FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
                        FSLocator.textDisplayer.reactionButton.onClick.AddListener(delegate { this.React(); });
                        delayedReaction.React(this);
                        return;
                    }
                }
                else if (reactions[i].GetType().Name == "PushBackReaction")
				{
					if (startIndex == reactions.Length - 1)
						break;
					else
					{
						startIndex = i + 1;
						FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
						FSLocator.textDisplayer.reactionButton.onClick.AddListener(delegate { this.React(); });
						delayedReaction.React(this);
						return;
					}
				}
                else if (reactions[i].GetType().Name == "GameOverReaction")
                {
                    startIndex = i + 1;
                    FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
                    FSLocator.textDisplayer.reactionButton.onClick.AddListener(delegate { this.React(); });
                    delayedReaction.React(this);
                    return;
                }
                else if (reactions[i].GetType().Name == "ObservationReaction")
                {
                    startIndex = i + 1;
                    FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
                    FSLocator.textDisplayer.reactionButton.onClick.AddListener(delegate { this.React(); });
                    delayedReaction.React(this);
                    return;
                }
                else if (reactions[i].GetType().Name == "EventCallbackReaction" || reactions[i].GetType().Name == "EventCallbackConditionReaction")
                {
                    startIndex = 0;
                    delayedReaction.React(this);
                    return;
                }
				else if (reactions[i].GetType().Name == "EventConditionReaction")
				{
					startIndex = 0;
					delayedReaction.React(this);
					return;
				}

                else if (reactions[i].GetType().Name == "SkipReaction")
                {
                    Skip();
                    //FSLocator.controlManager.m_Button.onClick.RemoveAllListeners();
                    //FSLocator.controlManager.m_Button.onClick.AddListener(delegate { this.React(); });
                    //return;
                }
                else
                {
                    delayedReaction.React(this);
                }
            }
            else
            {
                
            }
        }
        //startIndex = 0;

    }

	public void Skip()
	{
		for (int i = startIndex; i < reactions.Length; i++)
		{
			if (reactions[i].GetType().Name == "AnimationReaction")
			{
				//AnimationReaction animationReaction = reactions[i] as AnimationReaction;
				//animationReaction.Skip ();
			}

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
