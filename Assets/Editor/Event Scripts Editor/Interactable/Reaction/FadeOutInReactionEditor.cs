#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(FadeOutInReaction))]
public class FadeOutInReactionEditor : ReactionEditor
{
	protected override string GetFoldoutLabel()
	{
		return "FadeOutIn Reaction";
	}
}
