#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(ConditionReaction))]
public class ConditionReactionEditor : ReactionEditor
{
	protected override string GetFoldoutLabel()
	{
		return "Condition Reaction";
	}
}