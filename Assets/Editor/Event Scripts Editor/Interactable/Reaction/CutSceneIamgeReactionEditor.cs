#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(CutSceneImageReaction))]
public class CutSceneImageReactionEditor : ReactionEditor
{
	private SerializedProperty imageProperty;
	private SerializedProperty delayProperty;


	private const string cutSceneReactionPropImage = "sprite";
	private const string textReactionPropDelayName = "delay";


	protected override void Init ()
	{
		imageProperty = serializedObject.FindProperty (cutSceneReactionPropImage);
		delayProperty = serializedObject.FindProperty (textReactionPropDelayName);
	}


	protected override void DrawReaction ()
	{
		EditorGUILayout.PropertyField (imageProperty);
		EditorGUILayout.PropertyField (delayProperty);
	}


	protected override string GetFoldoutLabel ()
	{
		return "CutSceneImage Reaction";
	}
}
