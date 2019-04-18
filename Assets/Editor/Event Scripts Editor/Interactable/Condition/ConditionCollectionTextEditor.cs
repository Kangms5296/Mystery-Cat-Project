using UnityEngine;
#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(ConditionCollectionText))]
public class ConditionCollectionTextEditor : EditorWithSubEditors<ConditionEditor, Condition>
{
    public SerializedProperty collectionsProperty;


    private ConditionCollectionText conditionCollection;
    private SerializedProperty descriptionProperty;
    private SerializedProperty conditionsProperty;

    private SerializedProperty dialogueProperty;
    private SerializedProperty characterNameProperty;
    private SerializedProperty characterImageProperty;

    private const float conditionButtonWidth = 30f;
    private const float collectionButtonWidth = 125f;
    private const float messageGUILines = 3f;
    private const float areaWidthOffset = 19f;
    private const string conditionCollectionPropDescriptionName = "description";
    private const string conditionCollectionPropRequiredConditionsName = "requiredConditions";
    private const string textReactionPropDialogueName = "dialogue";
    private const string textReactionPropCharacterName = "characterName";
    private const string textReactionPropCharacterImage = "characterImage";

    private void OnEnable()
    {
        conditionCollection = (ConditionCollectionText)target;

        if (target == null)
        {
            DestroyImmediate(this);
            return;
        }

        descriptionProperty = serializedObject.FindProperty(conditionCollectionPropDescriptionName);
        conditionsProperty = serializedObject.FindProperty(conditionCollectionPropRequiredConditionsName);
        dialogueProperty = serializedObject.FindProperty(textReactionPropDialogueName);
        characterNameProperty = serializedObject.FindProperty(textReactionPropCharacterName);
        characterImageProperty = serializedObject.FindProperty(textReactionPropCharacterImage);

        CheckAndCreateSubEditors(conditionCollection.requiredConditions);
    }


    private void OnDisable()
    {
        CleanupEditors();
    }


    protected override void SubEditorSetup(ConditionEditor editor)
    {
        editor.editorType = ConditionEditor.EditorType.ConditionCollection;
        editor.conditionsProperty = conditionsProperty;
    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        CheckAndCreateSubEditors(conditionCollection.requiredConditions);

        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;

        EditorGUILayout.BeginHorizontal();

        descriptionProperty.isExpanded = EditorGUILayout.Foldout(descriptionProperty.isExpanded, descriptionProperty.stringValue);

        if (GUILayout.Button("Remove Collection", GUILayout.Width(collectionButtonWidth)))
        {
            collectionsProperty.RemoveFromObjectArray(conditionCollection);
        }

        EditorGUILayout.EndHorizontal();

        if (descriptionProperty.isExpanded)
        {
            ExpandedGUI();
        }

        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }


    private void ExpandedGUI()
    {
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(descriptionProperty);

        EditorGUILayout.Space();

        float space = EditorGUIUtility.currentViewWidth / 3f;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Condition", GUILayout.Width(space));
        EditorGUILayout.LabelField("Satisfied?", GUILayout.Width(space));
        EditorGUILayout.LabelField("Add/Remove", GUILayout.Width(space));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical(GUI.skin.box);
        for (int i = 0; i < subEditors.Length; i++)
        {
            subEditors[i].OnInspectorGUI();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("+", GUILayout.Width(conditionButtonWidth)))
        {
            Condition newCondition = ConditionEditor.CreateCondition();
            conditionsProperty.AddToObjectArray(newCondition);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Dialogue", GUILayout.Width(EditorGUIUtility.labelWidth - areaWidthOffset));
        dialogueProperty.stringValue = EditorGUILayout.TextArea(dialogueProperty.stringValue, GUILayout.Height(EditorGUIUtility.singleLineHeight * messageGUILines));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Name", GUILayout.Width(EditorGUIUtility.labelWidth - areaWidthOffset));
        characterNameProperty.stringValue = EditorGUILayout.TextArea(characterNameProperty.stringValue, GUILayout.Height(EditorGUIUtility.singleLineHeight));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.PropertyField(characterImageProperty);
    }


    public static ConditionCollectionText CreateConditionCollection()
    {
        ConditionCollectionText newConditionCollection = CreateInstance<ConditionCollectionText>();
        newConditionCollection.description = "New condition collection";
        newConditionCollection.requiredConditions = new Condition[1];
        newConditionCollection.requiredConditions[0] = ConditionEditor.CreateCondition();
        return newConditionCollection;
    }
}
