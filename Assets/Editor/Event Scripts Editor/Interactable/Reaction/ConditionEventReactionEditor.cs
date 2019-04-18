#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(ConditionEventReaction))]
public class ConditionEventReactionEditor : ReactionEditor
{
	protected override string GetFoldoutLabel()
	{
		return "Condition Event Reaction";
	}
}