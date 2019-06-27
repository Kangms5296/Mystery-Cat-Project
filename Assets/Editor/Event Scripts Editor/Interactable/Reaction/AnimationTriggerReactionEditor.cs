#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(AnimationTriggerReaction))]
public class AnimationTriggerReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Animation Trigger Reaction";
    }
}
