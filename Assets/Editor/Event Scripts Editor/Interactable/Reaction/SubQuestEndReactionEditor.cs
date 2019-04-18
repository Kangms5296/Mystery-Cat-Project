#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(SubQuestEndReaction))]
public class SubQuestEndReactionEditor : ReactionEditor
{
	protected override string GetFoldoutLabel()
	{
		return "=== SubQuest End Reaction ===";
	}
}