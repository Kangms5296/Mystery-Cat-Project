#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(CameraZoomInReaction))]
public class CameraZoomInReactionEditor : ReactionEditor
{

    protected override string GetFoldoutLabel()
    {
        return "Zoom In Reaction";
    }
}
