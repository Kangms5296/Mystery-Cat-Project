#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(TextShowReaction))]
public class TextShowReactionEditor : ReactionEditor
{
	protected override string GetFoldoutLabel()
	{
		return "=== Text Show Reaction ===";
	}
}