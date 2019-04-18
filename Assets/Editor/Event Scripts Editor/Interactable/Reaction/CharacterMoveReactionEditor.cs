#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(CharacterMoveReaction))]
public class CharacterMoveReactionEditor : ReactionEditor
{
	protected override string GetFoldoutLabel()
	{
		return "Character Move Reaction";
	}
}