#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(StopCameraReaction))]
public class StopCameraReactionEditor : ReactionEditor
{
	protected override string GetFoldoutLabel()
	{
		return "=== Stop Camera Reaction ===";
	}
}