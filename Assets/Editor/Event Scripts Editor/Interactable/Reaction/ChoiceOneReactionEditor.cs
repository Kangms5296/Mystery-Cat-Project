#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;


[CustomEditor(typeof(ChoiceOneReaction))]
public class ChoiceOneReactionEditor : ReactionEditor
{
    private SerializedProperty firstChoiceProperty;
    private SerializedProperty firstReactionCollectionProperty;

    private const string firstChoiceName = "firstChoice";
    private const string firstReactionCollectionName = "firstReactionCollection";


    protected override void Init()
    {
        firstChoiceProperty = serializedObject.FindProperty(firstChoiceName);
        firstReactionCollectionProperty = serializedObject.FindProperty(firstReactionCollectionName);
    }


    protected override void DrawReaction()
    {
        EditorGUILayout.PropertyField(firstChoiceProperty);
        EditorGUILayout.PropertyField(firstReactionCollectionProperty);
    }


    protected override string GetFoldoutLabel()
    {
        return "Choice One Reaction";
    }


}
