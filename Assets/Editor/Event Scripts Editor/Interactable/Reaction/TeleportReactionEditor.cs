#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(TeleportReaction))]
public class TeleportReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Teleport Reaction";
    }
}
