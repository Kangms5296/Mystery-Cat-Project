using System.Collections;
#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(PushBackReaction))]
public class PushBackReactionEditor : ReactionEditor
{
	protected override string GetFoldoutLabel()
	{
		return "Push Back Reaction Reaction";
	}
}

