using UnityEngine;
#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(Interactable))]
public class InteractableEditor : EditorWithSubEditors<ConditionCollectionEditor, ConditionCollection>
{
    private Interactable interactable;
    private SerializedProperty interactionLocationProperty;
    private SerializedProperty collectionsProperty;
    private SerializedProperty defaultReactionCollectionProperty;
	private SerializedProperty isSkipProperty;

    private const float collectionButtonWidth = 125f;
    private const string interactablePropInteractionLocationName = "interactionLocation";
    private const string interactablePropConditionCollectionsName = "conditionCollections";
    private const string interactablePropDefaultReactionCollectionName = "defaultReactionCollection";
	private const string isSkipName = "isSkip";

    private string newCollecterInputText;

    private void OnEnable ()
    {
        interactable = (Interactable)target;

        collectionsProperty = serializedObject.FindProperty(interactablePropConditionCollectionsName);
        interactionLocationProperty = serializedObject.FindProperty(interactablePropInteractionLocationName);
        defaultReactionCollectionProperty = serializedObject.FindProperty(interactablePropDefaultReactionCollectionName);
		isSkipProperty = serializedObject.FindProperty (isSkipName);
        
        CheckAndCreateSubEditors(interactable.conditionCollections);
    }


    private void OnDisable ()
    {
        CleanupEditors ();
    }


    protected override void SubEditorSetup(ConditionCollectionEditor editor)
    {
        editor.collectionsProperty = collectionsProperty;
    }


    public override void OnInspectorGUI ()
    {
        serializedObject.Update ();
        
        CheckAndCreateSubEditors(interactable.conditionCollections);
        
        EditorGUILayout.PropertyField (interactionLocationProperty);

        for (int i = 0; i < subEditors.Length; i++)
        {
            subEditors[i].OnInspectorGUI ();
            EditorGUILayout.Space ();
        }

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace ();

        newCollecterInputText = GUILayout.TextField(newCollecterInputText, GUILayout.Width(collectionButtonWidth));

        if (GUILayout.Button("Add Collection", GUILayout.Width(collectionButtonWidth)))
        {
            ConditionCollection newCollection = ConditionCollectionEditor.CreateConditionCollection();

            //collectionsProperty.AddToObjectArray (newCollection);
            if (newCollecterInputText == "" || newCollecterInputText == null)
                newCollecterInputText = collectionsProperty.arraySize.ToString();
            collectionsProperty.InsertToObjectArray(newCollection, int.Parse(newCollecterInputText));
            newCollecterInputText = "";
        }


        EditorGUILayout.EndHorizontal ();

        EditorGUILayout.Space ();

        EditorGUILayout.PropertyField (defaultReactionCollectionProperty);

		EditorGUILayout.PropertyField (isSkipProperty);

        serializedObject.ApplyModifiedProperties ();
    }
}
