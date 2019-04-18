#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(GetItemReaction))]
public class GetItemReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Get Item Reaction";
    }
}
