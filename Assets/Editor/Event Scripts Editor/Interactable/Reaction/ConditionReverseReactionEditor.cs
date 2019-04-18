#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(ConditionReverseReaction))]
public class ConditionReverseReactionEditor : ReactionEditor
{

    protected override string GetFoldoutLabel()
    {
        return "Condition Reverse Reaction";
    }
}
