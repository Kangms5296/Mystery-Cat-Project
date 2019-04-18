using System.Collections;
#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(DialogueWithoutFirecat))]
public class DialogueWithoutFirecatEditor : ReactionEditor
{
	protected override string GetFoldoutLabel()
	{
		return "Change Dialogue Reaction";
	}
}

