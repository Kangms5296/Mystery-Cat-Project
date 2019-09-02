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

        // 원활한 진행을 위해 플레이어의 물리 법칙을 잠시 종료
		newPlayer.GetComponent<BoxCollider2D> ().enabled = false;

        uiCaching = FindObjectOfType<UICaching>();

        // 상단 UI 제거
        foreach (var ui in uiCaching.GetUI())
            ui.SetActive(false);

        // Displayer 제거
        foreach (var ui in uiCaching.GetDisplayers())
            ui.SetActive(false);

		if(joyStick != null)
			joyStick.OnJoystickUp();
    }
}