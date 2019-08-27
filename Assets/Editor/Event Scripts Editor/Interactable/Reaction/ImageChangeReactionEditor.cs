#if UNITY_EDITOR 
using UnityEditor;
#endif 
using UnityEngine;

[CustomEditor(typeof(ImageChangeReaction))]
public class ImageChangeReactionEditor : ReactionEditor
{

    protected override string GetFoldoutLabel()
    {
        return "Image Change Reaction";
    }
}
