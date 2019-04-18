#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(GameObjectReaction))]
public class GameObjectReactionEditor : ReactionEditor
{
	protected override string GetFoldoutLabel()
	{
		return "Game Object Reaction";
	}
}
