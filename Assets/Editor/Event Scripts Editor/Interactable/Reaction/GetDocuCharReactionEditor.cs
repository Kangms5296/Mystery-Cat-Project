#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(GetDocuCharReaction))]
public class GetDocuCharReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Get Character Reaction";
    }
}
