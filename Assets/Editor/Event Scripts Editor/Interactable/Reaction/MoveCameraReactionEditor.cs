#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(MoveCameraReaction))]
public class MoveCameraReactionEditor : ReactionEditor
{
	protected override string GetFoldoutLabel()
	{
		return "=== Move Camera Reaction ===";
	}
}