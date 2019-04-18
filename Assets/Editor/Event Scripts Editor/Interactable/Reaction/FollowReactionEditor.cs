#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(FollowReaction))]
public class FollowReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Follow Reaction";
    }
}
