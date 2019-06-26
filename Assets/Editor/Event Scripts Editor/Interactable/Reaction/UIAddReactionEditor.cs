using UnityEngine;
#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(UIAddReaction))]
public class UIAddReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "UI Add Reaction";
    }
}
