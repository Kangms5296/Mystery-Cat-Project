using System.Collections;
#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(HideTextReaction))]
public class HideTextReactionEditor : ReactionEditor
{
	protected override string GetFoldoutLabel()
	{
		return "HideText Reaction";
	}
}

