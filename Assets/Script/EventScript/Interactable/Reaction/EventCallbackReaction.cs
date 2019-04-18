using UnityEngine;

public class EventCallbackReaction : DelayedReaction
{
    public ReactionCollection newReactionCollection;
    protected override void ImmediateReaction()
    {
		//TutorialController.Instance.reactionButton.onClick.RemoveAllListeners();
		//TutorialController.Instance.reactionButton.onClick.AddListener(delegate { newReactionCollection.React(); });
        newReactionCollection.InitAndReact();
    }

	public void Skip()
	{
		newReactionCollection.Skip ();
	}
}