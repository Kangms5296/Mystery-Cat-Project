#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(DestinationReaction))]
public class DestinationReactionEditor : ReactionEditor
{
	protected override string GetFoldoutLabel ()
	{
		return "Desination Reaction";
	}
}
