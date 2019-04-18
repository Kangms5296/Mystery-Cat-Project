#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(SetActiveFalseAgentReaction))]
public class SetActiveFalseAgentReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "No Follow Reaction";
    }
}
