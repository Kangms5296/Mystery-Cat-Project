#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(MoveDistanceCheckReaction))]
public class MoveDistanceCheckReactionEditor : ReactionEditor
{

    protected override string GetFoldoutLabel()
    {
        return "Move Distance Check Reaction";
    }
}
