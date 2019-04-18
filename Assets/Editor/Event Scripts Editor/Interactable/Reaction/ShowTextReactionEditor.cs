using System.Collections;
#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(ShowTextReaction))]
public class ShowTextReactionEditor : ReactionEditor
{
	protected override string GetFoldoutLabel()
	{
		return "ShowText Reaction";
	}
}

