#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(AudioFadeOutReaction))]
public class AudioFadeOutReactionEditor : ReactionEditor
{
	protected override string GetFoldoutLabel()
	{
		return "Audio Fade Out Reaction";
	}
}