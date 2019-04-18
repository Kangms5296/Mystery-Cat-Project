#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(GiveItemReaction))]
public class GiveItemReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Give Item Reaction";
    }
}
