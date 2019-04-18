#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(ChangeSceneReaction))]
public class ChangeSceneReactionEditor : ReactionEditor
{
	protected override string GetFoldoutLabel()
	{
		return "Change Scene Reaction";
	}
}