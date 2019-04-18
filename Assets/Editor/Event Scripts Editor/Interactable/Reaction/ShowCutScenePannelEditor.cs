using System.Collections;
#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(ShowCutScenePannel))]
public class ShowCutScenePannelEditor : ReactionEditor
{
	protected override string GetFoldoutLabel()
	{
		return "Show CutScene Pannel Reaction";
	}
}

