#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(StopMoveReaction))]
public class StopMoveReactionEditor : ReactionEditor
{
	protected override string GetFoldoutLabel()
	{
		return "=== Stop Move Reaction ===";
	}
}