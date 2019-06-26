using UnityEngine;

public class EventStartReaction : DelayedReaction
{
	//CharacterManager characterManag
	private UICaching uiCaching;
	private JoystickScript joyStick;
	private NewPlayer newPlayer;

	protected override void SpecificInit()
	{
		joyStick = FindObjectOfType<JoystickScript> ();
		newPlayer = FindObjectOfType<NewPlayer>();
	}


    protected override void ImmediateReaction()
    {
		//characterManager.PlayerController.keyState = (TVNTPlayerController.JoystickKey.NONE);
		FSLocator.textDisplayer.reactionButton.gameObject.SetActive (true);

		FSLocator.textDisplayer.ShowDialogueHolder ();
		FSLocator.uiContainer.HideObservationList ();

		newPlayer.GetComponent<BoxCollider2D> ().enabled = false;

        uiCaching = FindObjectOfType<UICaching>();
        foreach (var ui in uiCaching.GetUI())
            ui.gameObject.SetActive(false);

		if(joyStick != null)
			joyStick.OnJoystickUp();
    }
}