#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(TextHideReaction))]
public class TextHideReactionEditor : ReactionEditor
{
	protected override string GetFoldoutLabel()
	{
		return "=== Text Hide Reaction ===";
	}
}