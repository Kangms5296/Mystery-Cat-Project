#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(ShowObjectReaction))]
public class ShowObjectReactionEditor : ReactionEditor
{

    protected override string GetFoldoutLabel()
    {
        return "Object Show Reaction";
    }
}
