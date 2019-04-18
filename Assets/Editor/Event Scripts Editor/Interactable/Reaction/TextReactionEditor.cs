#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(TextReaction))]
public class TextReactionEditor : ReactionEditor
{
    private SerializedProperty dialogueProperty;
    private SerializedProperty characterNameProperty;
    private SerializedProperty characterImageProperty;
    private SerializedProperty delayProperty;


    private const float messageGUILines = 3f;
    private const float areaWidthOffset = 19f;
    private const string textReactionPropDialogueName = "dialogue";
    private const string textReactionPropCharacterName = "characterName";
    private const string textReactionPropCharacterImage = "characterImage";
    private const string textReactionPropDelayName = "delay";


    protected override void Init ()
    {
        dialogueProperty = serializedObject.FindProperty (textReactionPropDialogueName);
        characterNameProperty = serializedObject.FindProperty(textReactionPropCharacterName);
        characterImageProperty = serializedObject.FindProperty (textReactionPropCharacterImage);
        delayProperty = serializedObject.FindProperty (textReactionPropDelayName);
    }


    protected override void DrawReaction ()
    {
        EditorGUILayout.BeginHorizontal ();
        EditorGUILayout.LabelField ("Dialogue", GUILayout.Width (EditorGUIUtility.labelWidth - areaWidthOffset));
        dialogueProperty.stringValue = EditorGUILayout.TextArea (dialogueProperty.stringValue, GUILayout.Height (EditorGUIUtility.singleLineHeight * messageGUILines));
        EditorGUILayout.EndHorizontal ();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Name", GUILayout.Width(EditorGUIUtility.labelWidth - areaWidthOffset));
        characterNameProperty.stringValue = EditorGUILayout.TextArea(characterNameProperty.stringValue, GUILayout.Height(EditorGUIUtility.singleLineHeight));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.PropertyField (characterImageProperty);
        EditorGUILayout.PropertyField (delayProperty);
    }


    protected override string GetFoldoutLabel ()
    {
        return "Text Reaction";
    }
}
