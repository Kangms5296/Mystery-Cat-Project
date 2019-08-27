#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(CameraResetReaction))]
public class CameraResetReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Camera Reset Reaction";
    }
}
