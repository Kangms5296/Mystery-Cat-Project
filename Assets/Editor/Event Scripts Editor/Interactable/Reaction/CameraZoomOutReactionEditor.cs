#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(CameraZoomOutReaction))]
public class CameraZoomOutReactionEditor : ReactionEditor
{

    protected override string GetFoldoutLabel()
    {
        return "Zoom Out Reaction";
    }
}
