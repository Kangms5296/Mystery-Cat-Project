#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(AnimatorTriggerReaction))]
public class AnimatorTriggerReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Animator Trigger Reaction";
    }
}
