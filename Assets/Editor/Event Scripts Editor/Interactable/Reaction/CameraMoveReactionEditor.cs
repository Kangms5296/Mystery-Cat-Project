#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(CameraMoveReaction))]
public class CameraMoveReactionEditor : ReactionEditor
{

    protected override string GetFoldoutLabel()
    {
        return "Camera Move Reaction";
    }
}
