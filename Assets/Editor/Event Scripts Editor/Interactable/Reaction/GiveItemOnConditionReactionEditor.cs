#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(GiveItemOnConditionReaction))]
public class GiveItemOnConditionReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Give Item On Condition Reaction";
    }
}
