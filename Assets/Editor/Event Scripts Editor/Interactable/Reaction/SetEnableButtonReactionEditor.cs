using System.Collections;
#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(SetButtonEnableReaction))]
public class SetEnableButtonReactionEditor : ReactionEditor
{
	protected override string GetFoldoutLabel()
	{
		return "Set Button Enable Reaction";
	}
}

