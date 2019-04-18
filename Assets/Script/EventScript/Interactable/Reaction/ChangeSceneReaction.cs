using UnityEngine;
using System.Collections;
public class ChangeSceneReaction : DelayedReaction
{
	public string sceneName;

	protected override void SpecificInit ()
	{
		
	}

	protected override void ImmediateReaction()
	{
		FSLocator.changeSceneManager.ChangeScene (sceneName);
	}
}