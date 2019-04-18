using System.Collections;
#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(HideCutScenePannel))]
public class HideCutScenePannelEditor : ReactionEditor
{
	protected override string GetFoldoutLabel()
	{
		return "Hide CutScene Pannel Reaction";
	}
}

