#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(UIHideReaction))]
public class UIHideReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "UI Hide Reaction";
    }
}
