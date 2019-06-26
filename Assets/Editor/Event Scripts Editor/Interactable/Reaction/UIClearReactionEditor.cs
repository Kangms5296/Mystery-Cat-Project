using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR 
using UnityEditor;
#endif 

[CustomEditor(typeof(UIClearReaction))]
public class UIClearReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "UI Clear Reaction";
    }
}
