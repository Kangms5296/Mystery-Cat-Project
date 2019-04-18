#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(GetDocuInfoReaction))]
public class GetDocuInfoReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Get Information Reaction";
    }
}
