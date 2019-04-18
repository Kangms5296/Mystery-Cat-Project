#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(LifeGainReaction))]
public class LifeGainReactionEditor : ReactionEditor
{
	protected override string GetFoldoutLabel()
	{
		return "Life Gain Reaction";
	}
}