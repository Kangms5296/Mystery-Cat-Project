#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(ChoiceTextReaction))]
public class ChoiceTextReactionEditor : ReactionEditor
{

	private SerializedProperty firstChoiceProperty;
	private SerializedProperty firstReactionCollectionProperty;

	private SerializedProperty secondChoiceProperty;
	private SerializedProperty secondReactionCollectionProperty;

	private const string firstChoiceName = "firstChoice";
	private const string firstReactionCollectionName = "firstReactionCollection";

	private const string secondChoiceName = "secondChoice";
	private const string secondReactionCollectionName = "secondReactionCollection";


	protected override void Init ()
	{
		firstChoiceProperty = serializedObject.FindProperty (firstChoiceName);
		firstReactionCollectionProperty = serializedObject.FindProperty (firstReactionCollectionName);

		secondChoiceProperty = serializedObject.FindProperty (secondChoiceName);
		secondReactionCollectionProperty = serializedObject.FindProperty (secondReactionCollectionName);
	}


	protected override void DrawReaction ()
	{
		EditorGUILayout.PropertyField (firstChoiceProperty);
		EditorGUILayout.PropertyField (firstReactionCollectionProperty);

		EditorGUILayout.PropertyField (secondChoiceProperty);
		EditorGUILayout.PropertyField (secondReactionCollectionProperty);
	}


	protected override string GetFoldoutLabel()
	{
		return "Choice Text Reaction";
	}
}