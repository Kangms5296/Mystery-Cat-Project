#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(DebugReaction))]
public class DebugReactionEditor : ReactionEditor
{
	


	protected override void Init ()
	{
		
	}


	protected override void DrawReaction ()
	{
		
	}


	protected override string GetFoldoutLabel ()
	{
		return "Debug Reaction";
	}
}
