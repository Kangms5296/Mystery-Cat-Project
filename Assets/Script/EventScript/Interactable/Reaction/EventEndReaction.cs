using UnityEngine;
using System.Collections;

public class EventEndReaction : DelayedReaction
{
	//raycast m_raycast;
	private UICaching uiCaching;
	private NewPlayer newPlayer;

	protected override void SpecificInit()
	{
		newPlayer = FindObjectOfType<NewPlayer> ();
	}

    protected override void ImmediateReaction()
    {

        //대화 관련된 것들 전부 숨기기
		FSLocator.textDisplayer.reactionButton.onClick.RemoveAllListeners();
		FSLocator.textDisplayer.reactionButton.gameObject.SetActive (false);
        FSLocator.textDisplayer.HideDialogueHolder();
        FSLocator.characterDisplayer.HideImage();

		newPlayer.GetComponent<BoxCollider2D> ().enabled = true;

		FSLocator.uiContainer.ShowObservationList ();

        uiCaching = FindObjectOfType<UICaching>();
        foreach (var ui in uiCaching.GetUI())
			ui.gameObject.SetActive(true);
    }
}