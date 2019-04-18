#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(StartMoveReaction))]
public class StartMoveReactionEditor : ReactionEditor
{
	protected override string GetFoldoutLabel()
	{
		return "=== Start Move Reaction ===";
	}
}