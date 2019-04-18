#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(SubQuestReaction))]
public class SubQuestReactionEditor : ReactionEditor
{
	protected override string GetFoldoutLabel()
	{
		return "=== SubQuest Reaction ===";
	}
}