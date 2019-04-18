#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;
[CustomEditor(typeof(TextConditionReaction))]
public class TextConditionReactionEditor : ReactionEditor
{
    protected ConditionCollectionTextEditor[] subEditors;
    public TextConditionReaction interactableCallback;
    private SerializedProperty collectionsProperty;

    private const float collectionButtonWidth = 125f;
    private const string interactablePropConditionCollectionsName = "conditionCollectionsText";

    void CheckAndCreateSubEditors(ConditionCollectionText[] subEditorTargets)
    {
        if (subEditors != null && subEditors.Length == subEditorTargets.Length)
            return;

        CleanupEditors();

        subEditors = new ConditionCollectionTextEditor[subEditorTargets.Length];

        for (int i = 0; i < subEditors.Length; i++)
        {
            subEditors[i] = CreateEditor(subEditorTargets[i]) as ConditionCollectionTextEditor;
            SubEditorSetup(subEditors[i]);
        }
    }


    void CleanupEditors()
    {
        if (subEditors == null)
            return;

        for (int i = 0; i < subEditors.Length; i++)
        {
            DestroyImmediate(subEditors[i]);
        }

        subEditors = null;
    }


    void SubEditorSetup(ConditionCollectionTextEditor editor)
    {
        editor.collectionsProperty = collectionsProperty;
    }


    protected override void Init()
    {
        interactableCallback = (TextConditionReaction)target;
        collectionsProperty = serializedObject.FindProperty(interactablePropConditionCollectionsName);

        CheckAndCreateSubEditors(interactableCallback.conditionCollectionsText);
    }

    private void OnDisable()
    {
        CleanupEditors();
    }


    protected override void DrawReaction()
    {
        serializedObject.Update();

        CheckAndCreateSubEditors(interactableCallback.conditionCollectionsText);

        for (int i = 0; i < subEditors.Length; i++)
        {
            subEditors[i].OnInspectorGUI();
            EditorGUILayout.Space();
        }

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Add Collection", GUILayout.Width(collectionButtonWidth)))
        {
            ConditionCollectionText newCollection = ConditionCollectionTextEditor.CreateConditionCollection();
            collectionsProperty.AddToObjectArray(newCollection);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();



        serializedObject.ApplyModifiedProperties();
    }

    protected override string GetFoldoutLabel()
    {
        return "Text Condition Reaction";
    }
}
