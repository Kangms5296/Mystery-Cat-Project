#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(ConditionChoiceReaction))]
public class ConditionChoiceReactionEditor : ReactionEditor
{
	protected override string GetFoldoutLabel()
	{
		return "Condition Choice Reaction";
	}
}