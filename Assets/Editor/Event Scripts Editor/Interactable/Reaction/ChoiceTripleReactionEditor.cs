#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(ChoiceTripleReaction))]
public class ChoiceTripleReactionEditor : ReactionEditor
{

	private SerializedProperty firstChoiceProperty;
	private SerializedProperty firstReactionCollectionProperty; 

	private SerializedProperty secondChoiceProperty;
	private SerializedProperty secondReactionCollectionProperty;

	private SerializedProperty thirdChoiceProperty;
	private SerializedProperty thirdReactionCollectionProperty;

	private const string firstChoiceName = "firstChoice";
	private const string firstReactionCollectionName = "firstReactionCollection";

	private const string secondChoiceName = "secondChoice";
	private const string secondReactionCollectionName = "secondReactionCollection";

	private const string thirdChoiceName = "thirdChoice";
	private const string thirdReactionCollectionName = "thirdReactionCollection";


	protected override void Init ()
	{

		firstChoiceProperty = serializedObject.FindProperty (firstChoiceName);
		firstReactionCollectionProperty = serializedObject.FindProperty (firstReactionCollectionName);

		secondChoiceProperty = serializedObject.FindProperty (secondChoiceName);
		secondReactionCollectionProperty = serializedObject.FindProperty (secondReactionCollectionName);

		thirdChoiceProperty = serializedObject.FindProperty (thirdChoiceName);
		thirdReactionCollectionProperty = serializedObject.FindProperty (thirdReactionCollectionName);
	}


	protected override void DrawReaction ()
	{

		EditorGUILayout.PropertyField (firstChoiceProperty);
		EditorGUILayout.PropertyField (firstReactionCollectionProperty);

		EditorGUILayout.PropertyField (secondChoiceProperty);
		EditorGUILayout.PropertyField (secondReactionCollectionProperty);

		EditorGUILayout.PropertyField (thirdChoiceProperty);
		EditorGUILayout.PropertyField (thirdReactionCollectionProperty);
	}


	protected override string GetFoldoutLabel()
	{
		return "Choice Triple Text Reaction";
	}
}