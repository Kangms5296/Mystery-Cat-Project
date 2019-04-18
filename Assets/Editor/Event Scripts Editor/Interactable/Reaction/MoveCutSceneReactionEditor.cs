#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(MoveCutSceneReaction))]
public class MoveCutSceneReactionEditor : ReactionEditor
{
	protected override string GetFoldoutLabel()
	{
		return "Move CutScene Reaction";
	}
}