using System.Collections;
#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(VibrateReaction))]
public class VibrateReactionEditor : ReactionEditor
{
	protected override string GetFoldoutLabel()
	{
		return "Vibrate Reaction";
	}
}

