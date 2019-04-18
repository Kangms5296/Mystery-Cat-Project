#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(HideObjectReaction))]
public class HideObjectReactionEditor : ReactionEditor
{

    protected override string GetFoldoutLabel()
    {
        return "Object Hide Reaction";
    }
}
