#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(AudioFadeInReaction))]
public class AudioFadeInReactionEditor : ReactionEditor
{
	protected override string GetFoldoutLabel()
	{
		return "Audio Fade In Reaction";
	}
}